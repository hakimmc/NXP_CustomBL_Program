using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NXP_OttoBugger
{
    public static class GeneralClass
    {
        public static string DefaultFileLocation;
        public static void ReWriteDatas(string[] data)
        {

            if(data[0]=="UART")
            {
                OttobuggerV3.CanRadio.Checked = false;
                OttobuggerV3.UartRadio.Checked = true;
                OttobuggerV3.BaudCombobox.Items.Clear();
                OttobuggerV3.BaudCombobox.Items.AddRange(UartClass.baudrates);
            }
            else
            {
                OttobuggerV3.UartRadio.Checked = false;
                OttobuggerV3.CanRadio.Checked = true;
                OttobuggerV3.BaudCombobox.Items.Clear();
                OttobuggerV3.BaudCombobox.Items.AddRange(UartClass.baudrates);
            }
            OttobuggerV3.BaudCombobox.Text = data[1];
            OttobuggerV3.UartComportCombobox.Text = data[2]; 
            DefaultFileLocation = data[3];
            OttobuggerV3.CanDatasTXID.Text = data[4];
            OttobuggerV3.CanDatasTXDLC.Text = data[5];
            OttobuggerV3.CanDatasTXD1.Text = data[6];
            OttobuggerV3.CanDatasTXD2.Text = data[7];
            OttobuggerV3.CanDatasTXD3.Text = data[8];
            OttobuggerV3.CanDatasTXD4.Text = data[9];
            OttobuggerV3.CanDatasTXD5.Text = data[10];
            OttobuggerV3.CanDatasTXD6.Text = data[11];
            OttobuggerV3.CanDatasTXD7.Text = data[12];
            OttobuggerV3.CanDatasTXD8.Text = data[13];

            OttobuggerV3.CanDatasRXID.Text = data[14];
            OttobuggerV3.CanDatasRXDLC.Text = data[15];
            OttobuggerV3.CanDatasRXD1.Text = data[16];
            OttobuggerV3.CanDatasRXD2.Text = data[17];
            OttobuggerV3.CanDatasRXD3.Text = data[18];
            OttobuggerV3.CanDatasRXD4.Text = data[19];
            OttobuggerV3.CanDatasRXD5.Text = data[20];
            OttobuggerV3.CanDatasRXD6.Text = data[21];
            OttobuggerV3.CanDatasRXD7.Text = data[22];
            OttobuggerV3.CanDatasRXD8.Text = data[23];

            OttobuggerV3.UartDatasTX.Text = data[24];
            OttobuggerV3.UartDatasRX.Text = data[25];
        }
    }
}
