using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NXP_OttoBugger
{
    public static class UartClass
    {
        public static SerialPort SerialCom;
        public static string[] baudrates = { "9600", "115200"};
        public static bool UartConnect(SerialPort serial, String Com, int BaudRate)
        {
            try
            {
                serial.BaudRate = BaudRate;
                serial.PortName = Com;
                serial.Open();
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
                
        }

        public static bool UartDisconnect(SerialPort serial)
        {
            try
            {
                serial.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }
    }
}
