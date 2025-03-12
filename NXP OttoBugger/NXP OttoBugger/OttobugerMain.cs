using System.Diagnostics;
using System.IO;
using System.IO.Ports;

namespace NXP_OttoBugger
{
    public partial class OttobuggerV3 : Form
    {
        bool testwindow = false;
        public OttobuggerV3()
        {
            InitializeComponent();
        }

        public static string DefaultFileLocation;
        public bool flag = false;
        public int DoubleClickCounter = 0;
        void ReWriteDatas(string[] data)
        {

            if (data[0] == "UART")
            {
                CanRadio.Checked = false;
                UartRadio.Checked = true;
                BaudCombobox.Items.Clear();
                BaudCombobox.Items.AddRange(UartClass.baudrates);
                UART_COM_GB.Enabled = true;
            }
            else
            {
                UartRadio.Checked = false;
                CanRadio.Checked = true;
                BaudCombobox.Items.Clear();
                BaudCombobox.Items.AddRange(CanbusClass.baudrates);
                UART_COM_GB.Enabled = false;
            }
            foreach (string s in BaudCombobox.Items)
            {
                if (s == data[1])
                {
                    BaudCombobox.Text = data[1];
                    flag = true;
                    break;
                }
                continue;
            }
            if (!flag)
            {
                BaudCombobox.Text = Convert.ToString(BaudCombobox.Items[0]);
            }
            UartComportCombobox.Text = data[2];
            DefaultFileLocation = data[3];
            CanDatasTXID.Text = data[4];
            CanDatasTXDLC.Text = data[5];
            CanDatasTXD1.Text = data[6];
            CanDatasTXD2.Text = data[7];
            CanDatasTXD3.Text = data[8];
            CanDatasTXD4.Text = data[9];
            CanDatasTXD5.Text = data[10];
            CanDatasTXD6.Text = data[11];
            CanDatasTXD7.Text = data[12];
            CanDatasTXD8.Text = data[13];

            CanDatasRXID.Text = data[14];
            CanDatasRXDLC.Text = data[15];
            CanDatasRXD1.Text = data[16];
            CanDatasRXD2.Text = data[17];
            CanDatasRXD3.Text = data[18];
            CanDatasRXD4.Text = data[19];
            CanDatasRXD5.Text = data[20];
            CanDatasRXD6.Text = data[21];
            CanDatasRXD7.Text = data[22];
            CanDatasRXD8.Text = data[23];

            UartDatasTX.Text = data[24];
            UartDatasRX.Text = data[25];

        }
        string file = "set.csv";
        private void OttobuggerV3_Load(object sender, EventArgs e)
        {
            UartClass.SerialCom = new SerialPort();
            UartComportCombobox.Items.AddRange(SerialPort.GetPortNames());
            this.Size = new Size(286, 390);
            SW_UPD_GB.Enabled = false;
            TEST_GB.Enabled = false;
            using (StreamReader reader = new StreamReader(file))
            {
                string data = reader.ReadToEnd();
                ReWriteDatas(data.Split(','));
            }
        }
        private void OpenTest_Window_Button_Click(object sender, EventArgs e)
        {
            if (testwindow)
            {
                this.Size = new Size(286, 390);
                testwindow = false;
                OpenTest_Window_Button.Text = "Open Test Window";
            }
            else
            {
                this.Size = new Size(671, 390);
                testwindow = true;
                OpenTest_Window_Button.Text = "Close Test Window";
            }
        }
        private void OttobuggerV3_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (StreamWriter writer = new StreamWriter(file))
            {
                string commod = UartRadio.Checked == true ? "UART" : "CAN";
                writer.Write($"{commod}," +
                    $"{BaudCombobox.Text}," +
                    $"{UartComportCombobox.Text}," +
                    $"{DefaultFileLocation}," +
                    $"{CanDatasTXID.Text}," +
                    $"{CanDatasTXDLC.Text}," +
                    $"{CanDatasTXD1.Text}," +
                    $"{CanDatasTXD2.Text}," +
                    $"{CanDatasTXD3.Text}," +
                    $"{CanDatasTXD4.Text}," +
                    $"{CanDatasTXD5.Text}," +
                    $"{CanDatasTXD6.Text}," +
                    $"{CanDatasTXD7.Text}," +
                    $"{CanDatasTXD8.Text}," +
                    $"{CanDatasRXID.Text}," +
                    $"{CanDatasRXDLC.Text}," +
                    $"{CanDatasRXD1.Text}," +
                    $"{CanDatasRXD2.Text}," +
                    $"{CanDatasRXD3.Text}," +
                    $"{CanDatasRXD4.Text}," +
                    $"{CanDatasRXD5.Text}," +
                    $"{CanDatasRXD6.Text}," +
                    $"{CanDatasRXD7.Text}," +
                    $"{CanDatasRXD8.Text}," +
                    $"{UartDatasTX.Text}," +
                    $"{UartDatasRX.Text}");
            }
        }
        private void UartRadio_CheckedChanged(object sender, EventArgs e)
        {
            BaudCombobox.Items.Clear();
            BaudCombobox.Items.AddRange(UartRadio.Checked == true ? UartClass.baudrates : CanbusClass.baudrates);
            BaudCombobox.Text = Convert.ToString(BaudCombobox.Items[0]);
            UART_COM_GB.Enabled = UartRadio.Checked;

        }
        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if (ConnectButton.Text == "Connect to Device")
            {
                //uart
                if (UartRadio.Checked)
                {
                    if (UartClass.UartConnect(UartClass.SerialCom, UartComportCombobox.Text, Convert.ToInt32(BaudCombobox.Text)))
                    {
                        ConnectButton.Text = "Disconnect from Device";
                        COMM_MODE_GB.Enabled = false;
                        BAUD_GB.Enabled = false;
                        UART_COM_GB.Enabled = false;
                        SW_UPD_GB.Enabled = true;
                        TEST_GB.Enabled = true;
                        UART_TEST_GB.Enabled = true;
                        CAN_TEST_GB.Enabled = false;
                    }
                }
                //can
                else
                {
                    ConnectButton.Text = "Disconnect from Device";
                    COMM_MODE_GB.Enabled = false;
                    BAUD_GB.Enabled = false;
                    UART_COM_GB.Enabled = false;
                    SW_UPD_GB.Enabled = true;
                    SW_UPD_GB.Enabled = true;
                    TEST_GB.Enabled = true;
                    UART_TEST_GB.Enabled = false;
                    CAN_TEST_GB.Enabled = true;
                }
            }
            else
            {
                //uart disc
                if (UartRadio.Checked)
                {
                    if (UartClass.UartDisconnect(UartClass.SerialCom))
                    {
                        ConnectButton.Text = "Connect to Device";
                        COMM_MODE_GB.Enabled = true;
                        BAUD_GB.Enabled = true;
                        UART_COM_GB.Enabled = true;
                        SW_UPD_GB.Enabled = false;
                        TEST_GB.Enabled = false;
                        UART_TEST_GB.Enabled = false;
                        CAN_TEST_GB.Enabled = false;
                    }
                }
                //can disc
                else
                {
                    ConnectButton.Text = "Connect to Device";
                    COMM_MODE_GB.Enabled = true;
                    BAUD_GB.Enabled = true;
                    UART_COM_GB.Enabled = false;
                    SW_UPD_GB.Enabled = false;
                    TEST_GB.Enabled = false;
                    UART_TEST_GB.Enabled = false;
                    CAN_TEST_GB.Enabled = false;
                }
            }

        }
        private void OttobuggerV3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DoubleClickCounter++;
            if (DoubleClickCounter > 1)
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "https://www.linkedin.com/in/abdulhakim-calgin/",
                    UseShellExecute = true
                };
                Process.Start(psi);
                DoubleClickCounter = 0;
            }
        }
    }
}
