using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NXPBugger
{
    
    public static class GeneralProgramClass
    {
        public enum UploadMode
        {
            NONE,
            CONFIG,
            PROGRAM
        }

        public enum CanMessageState
        {
            ERROR,
            OK,
            TIMEOUT
        }

        public enum UartMessageState
        {
            ERROR,
            OK,
            TIMEOUT
        }

        public static UploadMode ModeForUpload;
        public static bool READCFG = false;
        public static string DefaultFileLocation;
        public static bool FormActive_CFG_Creator = false;
        public static bool FormActive_CFG_Reader = false;
        public static bool VALID_FILE = false;
        private static UInt16 BOOT_CCITT_KEY = 0xCEFA;
        public static uint BOOT_START_TIMEOUT = 1000;

        private static void CheckBinFile_Is_Valid(byte[] fileData)
        {
            try
            {
                byte[] target = { (byte)('!'), (byte)('C'), (byte)('F'), (byte)('G'), (byte)'E', (byte)'O', (byte)'C', (byte)';' };
                for (int i = 0; i < 8; i++)
                {
                    if (fileData[40 + i] == target[i]) continue;
                    return;
                }
                VALID_FILE = true;
            }
            catch
            {
                MessageBox.Show("Please select valid upgrade file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                VALID_FILE = false;
            }
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

        public static byte[][] ReadBinFile(string filePath)
        {
            int DataIndexController = 0;
            byte[] fileData = File.ReadAllBytes(filePath);
            CheckBinFile_Is_Valid(fileData);
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
    }
}
