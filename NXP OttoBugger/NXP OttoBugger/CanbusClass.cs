using Peak.Can.Basic;
using Peak.Can.Basic.BackwardCompatibility;
using System.Collections;
using System.Diagnostics;
using System.IO.Ports;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using static NXPBugger.GeneralProgramClass;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NXPBugger
{
    public static class CanbusClass
    {
        public static string[] baudrates = { "125K", "250K", "500K", "1000K" };
        public static PcanStatus result;
        public static PcanChannel channel = PcanChannel.Usb01;
        public static PcanMessage CanTXMessage = new PcanMessage();
        public static PcanMessage CanRXMessage = new PcanMessage();
        public static uint BOOT_ID = 0x5166;
        public static uint BOOT_WAKE_ID = 0x5165;
        public static MessageType BOOT_MSGTYP = MessageType.Extended;
        public static uint BOOT_DLC = 8;
        //public static byte[] BOOT_START_DATA = {0x4F, 0x54, 0x54, 0x4F, 0x0, 0x0, 0x0, 0x0};
        public static bool IsCanOpen = false;
        private static byte[] BOOT_RECV_BYTE = new byte[8];

        public static readonly byte[] START_BL_TX = Encoding.ASCII.GetBytes("!BOOTSTT");
        public static readonly byte[] START_BL_RX = Encoding.ASCII.GetBytes("!BOOTSTD");
        private static readonly byte[] START_MSG_CFG = Encoding.ASCII.GetBytes("!CFGxxxx");
        private static readonly byte[] WOKE_UP_FROM_APP = Encoding.ASCII.GetBytes("!WAKEAPP");
        private static readonly byte[] START_MSG_APP = Encoding.ASCII.GetBytes("!APPxxxx");
        private static readonly byte[] APP_READY_MSG = Encoding.ASCII.GetBytes("!APPSTRT");
        private static readonly byte[] READY_MSG = Encoding.ASCII.GetBytes("!OTTOSTR");
        private static readonly byte[] NEXT_MSG = Encoding.ASCII.GetBytes("!OTTONXT");
        private static readonly byte[] END_MSG = Encoding.ASCII.GetBytes("!OTTOJMP");

        public static bool CanConnect(PcanChannel PcanChannel, string BaudRate)
        {
            try
            {
                if (BaudRate == "125K")
                {
                    result = Api.Initialize(PcanChannel, Bitrate.Pcan125);
                }
                else if (BaudRate == "250K")
                {
                    result = Api.Initialize(PcanChannel, Bitrate.Pcan250);
                }
                else if (BaudRate == "500K")
                {
                    result = Api.Initialize(PcanChannel, Bitrate.Pcan500);
                }
                return true;
            }
            catch (TypeInitializationException ex)
            {
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }
        }
        public static bool CanDisconnect(PcanChannel PcanChannel)
        {
            try
            {
                Api.Uninitialize(PcanChannel);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public static bool CanTransmit(PcanChannel PcanChannel, uint ID, MessageType MSG_FRMT, uint DLC, byte[] data)
        {
            CanTXMessage.ID = ID;
            CanTXMessage.MsgType = MSG_FRMT;
            CanTXMessage.DLC = (byte)DLC;
            for (int i = 0; i < DLC; i++)
            {
                CanTXMessage.Data[i] = data[i];
            }
            Api.Write(PcanChannel, CanTXMessage);
            return true;
        }

        public static bool CanReceive(PcanChannel PcanChannel, uint ID, MessageType MSG_FRMT, uint DLC, byte[] data, uint timeoutMs)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            while (stopwatch.ElapsedMilliseconds < timeoutMs)
            {
                if (Api.Read(PcanChannel, out CanRXMessage) == PcanStatus.OK)
                {
                    if (CanRXMessage.ID != ID || CanRXMessage.MsgType != MSG_FRMT || CanRXMessage.DLC != DLC)
                        continue;

                    int length = Math.Min(CanRXMessage.DLC, data.Length);
                    Array.Copy(CanRXMessage.Data, data, length);
                    return true;
                }
                //Thread.Sleep(1);
            }
            return false;
        }

        public static bool CanBootloaderStart(PcanChannel PcanChannel, string filePath, ProgressBar pb, Button SW_UPD_BUTTON, Label TIME_LABEL, ref bool Kill_Thread_Status)
        {
            try
            {
                byte[][] fileChunks = GeneralProgramClass.ReadBinFile(filePath);
                if (GeneralProgramClass.VALID_FILE == false)
                {
                    MessageBox.Show("Please select valid upgrade file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                pb.Enabled = true;
                SW_UPD_BUTTON.Text = "Software Update Started!";
                SW_UPD_BUTTON.Enabled = false;
                Thread.Sleep(100);
                CanTransmit(PcanChannel, BOOT_WAKE_ID, BOOT_MSGTYP, BOOT_DLC, WOKE_UP_FROM_APP);
                if (WaitForMessage(PcanChannel, APP_READY_MSG, GeneralProgramClass.BOOT_START_TIMEOUT) == CanMessageState.OK)
                {
                    SW_UPD_BUTTON.Text = "Woke Up From App";
                }
                else return false;
                Thread.Sleep(200);
                int unixTimestamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                for (int indx = 0; indx < 4; indx++)
                {
                    START_MSG_APP[indx + 4] = (byte)(0xFF & (unixTimestamp >> (24 - (8 * indx))));
                }
                switch (GeneralProgramClass.ModeForUpload)
                {
                    case GeneralProgramClass.UploadMode.CONFIG:
                        {
                            CanTransmit(PcanChannel, BOOT_WAKE_ID, BOOT_MSGTYP, BOOT_DLC, START_MSG_CFG);
                            if (WaitForMessage(PcanChannel, READY_MSG, GeneralProgramClass.BOOT_START_TIMEOUT) != CanMessageState.OK)
                            {
                                return false;
                            }
                            SW_UPD_BUTTON.Text = "Software Update Mode : Config";
                            break;
                        }
                    case GeneralProgramClass.UploadMode.PROGRAM:
                        {
                            CanTransmit(PcanChannel, BOOT_WAKE_ID, BOOT_MSGTYP, BOOT_DLC, START_MSG_APP);
                            if (WaitForMessage(PcanChannel, READY_MSG, GeneralProgramClass.BOOT_START_TIMEOUT) != CanMessageState.OK)
                            {
                                return false;
                            }
                            SW_UPD_BUTTON.Text = "Software Update Mode : Program";
                            break;
                        }
                }

                int totalChunks = fileChunks.Length;

                for (int i = 0; i < totalChunks; i++)
                {
                    if (Kill_Thread_Status)
                    {
                        Kill_Thread_Status = false;
                        return false;
                    }

                    byte[] packet = fileChunks[i];
                    CanTransmit(PcanChannel, BOOT_ID, BOOT_MSGTYP, BOOT_DLC, packet);

                    if (WaitForMessage(PcanChannel, NEXT_MSG, GeneralProgramClass.BOOT_START_TIMEOUT) != CanMessageState.OK)
                    {
                        return false;
                    }

                    pb.Value += 4;
                    //SW_UPD_BUTTON.Text = pb.Value/4 + " / " + pb.Maximum/4 + "flashed!";
                }
                SW_UPD_BUTTON.Text = pb.Maximum / 4 + "flashed!";
                Thread.Sleep(20);
                SW_UPD_BUTTON.Text = "Software Update Done";
                Thread.Sleep(20);
                CanTransmit(PcanChannel, BOOT_ID, BOOT_MSGTYP, BOOT_DLC, END_MSG);
                SW_UPD_BUTTON.Text = "Software Update Start";
                SW_UPD_BUTTON.Enabled = true;
                pb.Enabled = false;
                pb.Value = 0;
                MessageBox.Show("Software Update Successful!", "Software Update Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch
            {
                MessageBox.Show("Software Update Error!", "Software Update Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                pb.Value = 0;
                pb.Enabled = false;
                SW_UPD_BUTTON.Text = "Software Update Start";
                SW_UPD_BUTTON.Enabled = true;
                Kill_Thread_Status = false;
            }
        }
        public static CanMessageState WaitForMessage(PcanChannel PcanChannel, byte[] expectedMsg, uint Timeout)
        {
            if (!CanReceive(PcanChannel, BOOT_ID, BOOT_MSGTYP, BOOT_DLC, BOOT_RECV_BYTE, Timeout))
            {
                MessageBox.Show("Boot Timeout Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return CanMessageState.TIMEOUT;
            }
            if (Compare(BOOT_RECV_BYTE, expectedMsg, 8))
            {
                return CanMessageState.OK;
            }
            else
            {
                MessageBox.Show("Boot Unknown Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return CanMessageState.ERROR;
            }
        }
        
        private static bool Compare(byte[] Value_1, byte[] Value_2, uint Length)
        {
            for(int i=0;i<Length;i++)
            {
                if (Value_1[i] != Value_2[i]) return false;
            }
            return true;
        }

    }
}
