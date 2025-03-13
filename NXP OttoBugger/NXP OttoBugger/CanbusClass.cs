using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Peak.Can.Basic;

namespace NXP_OttoBugger
{
    public static class CanbusClass
    {
        public static string[] baudrates = { "125K", "250K", "500K" };
        public static PcanStatus result;
        public static PcanChannel channel = PcanChannel.Usb01;
        public static PcanMessage CanTXMessage;
        public static bool CanConnect(string BaudRate)
        {
            try
            {
                if(BaudRate == "125K")
                {
                    result = Api.Initialize(channel, Bitrate.Pcan125);
                }
                else if (BaudRate == "250K")
                {
                    result = Api.Initialize(channel, Bitrate.Pcan250);
                }
                else if (BaudRate == "500K")
                {
                    result = Api.Initialize(channel, Bitrate.Pcan500);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }
        }
        public static bool CanDisconnect()
        {
            try
            {
                Api.Uninitialize(channel);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public static bool CanTransmit(uint ID, MessageType MSG_FRMT, uint DLC, byte[] data)
        {
            CanTXMessage.ID = ID;
            CanTXMessage.MsgType = MSG_FRMT;
            CanTXMessage.DLC = (byte)DLC;
            for(int i=0;i<DLC;i++)
            {
                CanTXMessage.Data[i] = data[i];
            }
            Api.Write(channel, CanTXMessage);
            return true;
        }
        public static bool CanReceive(int ID, int MSG_FRMT ,int DLC, int[] data)
        {
            return true;
        }
    }
}
