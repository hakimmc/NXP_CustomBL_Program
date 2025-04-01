using Peak.Can.Basic;
using System.Collections;
using System.IO.Ports;
using System.Net.Sockets;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NXP_OttoBugger
{
    public static class CanbusClass
    {
        public static string[] baudrates = { "125K", "250K", "500K" };
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
        private static UInt16 BOOT_CCITT_KEY = 0xCEFA;
        private static byte[] BOOT_RECV_BYTE = new byte[8];

        public static readonly byte[] START_BL_TX = Encoding.ASCII.GetBytes("!BOOTSTT");
        public static readonly byte[] START_BL_RX = Encoding.ASCII.GetBytes("!BOOTSTD");
        private static readonly byte[] START_MSG_CFG = Encoding.ASCII.GetBytes("!CFGxxxx");
        private static readonly byte[] START_MSG_APP = Encoding.ASCII.GetBytes("!APPxxxx");
        private static readonly byte[] READY_MSG = Encoding.ASCII.GetBytes("!OTTOSTR");
        private static readonly byte[] NEXT_MSG = Encoding.ASCII.GetBytes("!OTTONXT");
        private static readonly byte[] END_MSG = Encoding.ASCII.GetBytes("!OTTOJMP");
        private static readonly byte[] SKIP_MSG = Encoding.ASCII.GetBytes("!SKIPJUMP");

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        public static bool CanReceive(PcanChannel PcanChannel, uint ID, MessageType MSG_FRMT, uint DLC, byte[] data)
        {
        spawn:
            Api.Read(PcanChannel, out CanRXMessage);
            if (CanRXMessage.ID != ID || CanRXMessage.MsgType != MSG_FRMT || CanRXMessage.DLC != DLC) goto spawn;
            for(int i = 0; i < 8; i++)
            {
                data[i] = CanRXMessage.Data[i];
            }
            return true;
        }
        public static bool CanBootloaderStart(PcanChannel PcanChannel, string filePath, ProgressBar pb, Button SW_UPD_BUTTON, Label TIME_LABEL, bool Kill_Thread_Status)
        {
            try
            {
                pb.Enabled = true;
                SW_UPD_BUTTON.Text = "Software Updating";
                SW_UPD_BUTTON.Enabled = false;
                switch (GeneralProgramClass.ModeForUpload)
                {
                    case GeneralProgramClass.UploadMode.CONFIG:
                    {
                        while (true)
                        {
                            int unixTimestamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                            for (int indx = 0; indx < 4; indx++)
                            {
                                START_MSG_CFG[indx + 4] = (byte)(0xFF & (unixTimestamp >> (24 - (8 * indx))));
                            }
                            CanTransmit(PcanChannel, BOOT_WAKE_ID, BOOT_MSGTYP, BOOT_DLC, START_MSG_CFG);
                            if (WaitForMessage(PcanChannel, READY_MSG))
                            {
                                break;
                            }
                            if (Kill_Thread_Status)
                            {
                                Kill_Thread_Status = false;
                                return false;
                            }
                        }
                        break;
                    }
                    case GeneralProgramClass.UploadMode.PROGRAM:
                    {
                        while (true)
                        {
                            int unixTimestamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                            for (int indx = 0; indx < 4; indx++)
                            {
                                START_MSG_APP[indx + 4] = (byte)(0xFF & (unixTimestamp >> (24 - (8 * indx))));
                            }
                            CanTransmit(PcanChannel, BOOT_WAKE_ID, BOOT_MSGTYP, BOOT_DLC, START_MSG_APP);
                            if (WaitForMessage(PcanChannel, READY_MSG))
                            {
                                break;
                            }
                            if (Kill_Thread_Status)
                            {
                                Kill_Thread_Status = false;
                                return false;
                            }
                        }
                        break;
                    }
                }
                

                byte[][] fileChunks = ReadBinFile(filePath);
                int totalChunks = fileChunks.Length;

                for (int i = 0; i < totalChunks; i++)
                {
                    byte[] packet = fileChunks[i];
                    while (true)
                    {
                        CanTransmit(PcanChannel, BOOT_ID, BOOT_MSGTYP, BOOT_DLC, packet);
                        if (WaitForMessage(PcanChannel, NEXT_MSG))
                        {
                            pb.Value += 4;
                        }
                        if (Kill_Thread_Status)
                        {
                            Kill_Thread_Status = false;
                            return false;
                        }
                        break;
                    }
                }
                CanTransmit(PcanChannel, BOOT_ID, BOOT_MSGTYP, BOOT_DLC, END_MSG);
                SW_UPD_BUTTON.Text = "Software Update Start";
                SW_UPD_BUTTON.Enabled = true;
                pb.Enabled = false;
                pb.Value = 0;
                if (Kill_Thread_Status)
                {
                    Kill_Thread_Status = false;
                    return false;
                }
                MessageBox.Show("Software Update Successfull!", "Software Update Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch
            {
                SW_UPD_BUTTON.Text = "Software Update Start";
                SW_UPD_BUTTON.Enabled = true;
                pb.Enabled = false;
                if (Kill_Thread_Status)
                {
                    Kill_Thread_Status = false;
                    return false;
                }
                MessageBox.Show("Software Update Error!", "Software Update Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            /*finally
            {
                SW_UPD_BUTTON.Text = "Software Update Start";
                SW_UPD_BUTTON.Enabled = true;
            }*/
        }
        public static bool WaitForMessage(PcanChannel PcanChannel, byte[] expectedMsg)
        {
            DateTime startTime = DateTime.Now;
            while ((DateTime.Now - startTime).TotalMilliseconds < 2000)
            {
                CanReceive(PcanChannel, BOOT_ID, BOOT_MSGTYP, BOOT_DLC, BOOT_RECV_BYTE);
                if (Compare(BOOT_RECV_BYTE, expectedMsg, 8))
                {
                    //MessageBox.Show($"ID : {CanRXMessage.ID}\nDATA0 : {CanRXMessage.Data[0]}\nDATA1 : {CanRXMessage.Data[1]}\nDATA2 : {CanRXMessage.Data[2]}\nDATA3 : {CanRXMessage.Data[3]}\nDATA4 : {CanRXMessage.Data[4]}\nDATA5 : {CanRXMessage.Data[5]}\nDATA6 : {CanRXMessage.Data[6]}\nDATA7 : {CanRXMessage.Data[7]}\n");
                    return true;
                }
            }
            return false;
        }
        private static byte[][] ReadBinFile(string filePath)
        {
            int DataIndexController = 0;
            byte[] fileData = File.ReadAllBytes(filePath);
            int chunkSize = 4;
            int totalChunks = (fileData.Length + chunkSize - 1) / chunkSize;

            byte[][] chunks = new byte[totalChunks][];

            for (int i = 0; i < totalChunks; i++)
            {
                byte[] chunk = fileData.Skip(i * chunkSize).Take(chunkSize).ToArray();

                if (chunk.Length < chunkSize)
                {
                    Array.Resize(ref chunk, chunkSize);
                }
                byte[] packet = new byte[chunkSize + 4];
                packet[0] = (byte)'~';
                packet[1] = (byte)DataIndexController++;
                DataIndexController = DataIndexController > 1 ? 0 : 1;
                Array.Copy(chunk, 0, packet, 2, chunkSize);
                byte[] crc = CalculateCRC(packet, 6);
                packet[chunkSize + 2] = crc[0];
                packet[chunkSize + 3] = crc[1];
                chunks[i] = packet;
            }
            return chunks;
        }
        private static byte[] CalculateCRC(byte[] data, uint Length)
        {
            const ushort poly = 4129;
            ushort[] table = new ushort[256];
            ushort initialValue = BOOT_CCITT_KEY;
            ushort temp, a;
            ushort crc = initialValue;
            for (int i = 0; i < table.Length; ++i)
            {
                temp = 0;
                a = (ushort)(i << 8);
                for (int j = 0; j < 8; ++j)
                {
                    if (((temp ^ a) & 0x8000) != 0)
                        temp = (ushort)((temp << 1) ^ poly);
                    else
                        temp <<= 1;
                    a <<= 1;
                }
                table[i] = temp;
            }
            for (int i = 0; i < Length; ++i)
            {
                crc = (ushort)((crc << 8) ^ table[((crc >> 8) ^ (0xFF & data[i]))]);
            }
            byte[] CRC_CCITT = new byte[2];
            CRC_CCITT[0] = (byte)(crc >> 8);
            CRC_CCITT[1] = (byte)(crc & 0xFF);
            return CRC_CCITT;
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
