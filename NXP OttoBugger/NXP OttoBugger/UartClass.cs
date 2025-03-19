using System.IO.Ports;
using System.Text;

namespace NXP_OttoBugger
{
    public static class UartClass
    {
        public static SerialPort SerialCom;
        public static string[] baudrates = { "9600", "115200" };
        private static readonly byte[] START_MSG = Encoding.ASCII.GetBytes("!OTTOWAKE!");
        private static readonly byte[] READY_MSG = Encoding.ASCII.GetBytes("!STR");
        private static readonly byte[] NEXT_MSG = Encoding.ASCII.GetBytes("!NXT");
        private static readonly byte[] END_MSG = Encoding.ASCII.GetBytes("!OTTOJUMP!");
        private static readonly byte[] SKIP_MSG = Encoding.ASCII.GetBytes("!SKIPJUMP!");
        public static bool UartConnect(SerialPort serial, String Com, int BaudRate)
        {
            try
            {
                serial.BaudRate = BaudRate;
                serial.PortName = Com;
                serial.Open();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }
        public static int SendedDataCount;
        public static bool UartBootloaderStart(SerialPort serial, string filePath, ProgressBar pb, Button SW_UPD_BUTTON, Label TIME_LABEL)
        {
            if (!serial.IsOpen)
            {
                return false;
            }

            try
            {
                pb.Enabled = true;
                SW_UPD_BUTTON.Text = "Software Updating";
                SW_UPD_BUTTON.Enabled = false;
                while (true)
                {
                    serial.Write(START_MSG, 0, START_MSG.Length);
                    if (WaitForMessage(serial, READY_MSG))
                    {
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
                        serial.Write(packet, 0, packet.Length);
                        if (WaitForMessage(serial, NEXT_MSG))
                        {
                            //SendedDataCount += 8;
                            pb.Value += 8;
                            break;
                        }
                    }
                }

                serial.Write(END_MSG, 0, END_MSG.Length);
                SW_UPD_BUTTON.Text = "Software Update Start";
                SW_UPD_BUTTON.Enabled = true;
                pb.Enabled = false;
                pb.Value = 0;
                MessageBox.Show("Software Update Successfull!", "Software Update Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch
            {
                SW_UPD_BUTTON.Text = "Software Update Start";
                SW_UPD_BUTTON.Enabled = true;
                pb.Enabled = false;
                MessageBox.Show("Software Update Error!", "Software Update Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            /*finally
            {
                SW_UPD_BUTTON.Text = "Software Update Start";
                SW_UPD_BUTTON.Enabled = true;
            }*/
        }
        private static byte[][] ReadBinFile(string filePath)
        {
            byte[] fileData = File.ReadAllBytes(filePath);
            int chunkSize = 8;
            int totalChunks = (fileData.Length + chunkSize - 1) / chunkSize;

            byte[][] chunks = new byte[totalChunks][];

            for (int i = 0; i < totalChunks; i++)
            {
                byte[] chunk = fileData.Skip(i * chunkSize).Take(chunkSize).ToArray();

                if (chunk.Length < chunkSize)
                {
                    Array.Resize(ref chunk, chunkSize);
                }

                if (chunk.All(b => b == 0))
                {
                    chunks[i] = SKIP_MSG;
                }
                else
                {
                    byte crc = CalculateCRC(chunk);
                    byte[] packet = new byte[chunkSize + 2];
                    packet[0] = (byte)'!';
                    Array.Copy(chunk, 0, packet, 1, chunkSize);
                    packet[chunkSize + 1] = crc;
                    chunks[i] = packet;
                }
            }

            return chunks;
        }
        private static byte CalculateCRC(byte[] data)
        {
            int sum = '!';
            foreach (byte b in data)
            {
                sum += b;
            }
            return (byte)(sum % 255);
        }
        private static bool WaitForMessage(SerialPort serial, byte[] expectedMsg)
        {
            DateTime startTime = DateTime.Now;
            while ((DateTime.Now - startTime).TotalMilliseconds < 5000)
            {
                if (serial.BytesToRead >= expectedMsg.Length)
                {
                    byte[] response = new byte[expectedMsg.Length];
                    serial.Read(response, 0, expectedMsg.Length);

                    if (response.SequenceEqual(expectedMsg))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        
        public static bool Serial_Transmit(SerialPort serial, string Data)
        {
            try
            {
                if (!serial.IsOpen)
                {
                    return false;
                }
                serial.Write(Encoding.ASCII.GetBytes(Data), 0, Data.Length);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool Serial_Receive(SerialPort serial, string Data)
        {
            try
            {
                if (!serial.IsOpen)
                {
                    return false;
                }
                serial.Read(Encoding.ASCII.GetBytes(Data), 0, Data.Length);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
