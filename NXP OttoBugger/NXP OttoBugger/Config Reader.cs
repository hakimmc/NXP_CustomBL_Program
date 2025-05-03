using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NXPBugger
{
    public partial class Config_Reader : Form
    {
        public Config_Reader()
        {
            InitializeComponent();
        }

        private void SelectCfgFromStorage_Click(object sender, EventArgs e)
        {
            ReadAndReplaceConfigFile();
        }

        public void ReadAndReplaceConfigFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select Config File For Show Configuration";
            ofd.Filter = "Config File |*.cfg";
            ofd.FilterIndex = 1;
            ofd.InitialDirectory = GeneralProgramClass.DefaultFileLocation;
            if (DialogResult.OK == ofd.ShowDialog())
            {
                byte[] filebyte = File.ReadAllBytes(ofd.FileName);
                UInt32 unixtime = 0;
                for(int i = 0; i < 4; i++)
                {
                    unixtime += (Convert.ToUInt32(filebyte[i]) << (i*8));
                }
                date.Text = DateTimeOffset.FromUnixTimeSeconds(unixtime).LocalDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                SYSTEMID.Value = filebyte[4];
                MAJORVERSION.Value = filebyte[5];
                MINORVERSION.Value = filebyte[6];
                BUGFIXVERSION.Value = filebyte[7];
                //filebyte[8] released!
                MAC_ADDR[0] = filebyte[9];
                MAC_ADDR[1] = filebyte[10];
                MAC_ADDR[2] = filebyte[11];
                MAC_ADDR[3] = filebyte[12];
                MAC_ADDR[4] = filebyte[13];
                MAC_ADDR[5] = filebyte[14];
                //filebyte[15] released!
                BootModeCB.SelectedIndex = filebyte[16] & 0x1;
                BOOTTIMEOUT.Value = filebyte[16] & 0xFE;
                CAN1_BAUD_CB.SelectedIndex = filebyte[17] & 0x3;
                CAN2_BAUD_CB.SelectedIndex = (filebyte[17] & (0x3 << 2)) >> 2;
                CAN3_BAUD_CB.SelectedIndex = (filebyte[17] & (0x3 << 4)) >> 4;
                CAN4_BAUD_CB.SelectedIndex = (filebyte[17] & (0x3 << 6)) >> 6;
                UARTBAUDCB.SelectedIndex = filebyte[18] & 0x1;
                PRINTFDEBUGCB.SelectedIndex = filebyte[18] & 0x2;
                companynum.Value = filebyte[18] & 0xFC;
                usernamenum.Value = filebyte[19];
                CELLCAPAH.Value = filebyte[20];
                SERIESCELLCOUNT.Value = filebyte[21];
                PARALLELCELLCOUNT.Value = filebyte[22];
                DAISYCHAINCOUNT.Value = filebyte[23];
                maxcellv.Text = (((uint)(filebyte[25]) << 8) + filebyte[24]).ToString();
                mincellv.Text = (((uint)(filebyte[27]) << 8) + filebyte[26]).ToString();
                tempsensorb.Text = (((uint)(filebyte[29]) << 8) + filebyte[28]).ToString();
                defaultsoc.Value = filebyte[30];
                defaultsoh.Value = filebyte[31];
                maxtemp.Value = filebyte[32];
                mintemp.Value = filebyte[33];
                tempsensorcount.Value = filebyte[34];
                //filebyte[35] released!
                //filebyte[36] '!'
                //filebyte[37] 'C'
                //filebyte[38] 'F'
                //filebyte[39] 'G'
                //filebyte[40] 'S'
                //filebyte[41] 'K'
                //filebyte[42] 'I'
                //filebyte[43] 'P'
                macaddresstext.Text = "MAC : " + BitConverter.ToString(MAC_ADDR).Replace("-", ":");
            }
            else
            {
                return;
            }
        }

        public byte[] MAC_ADDR = new byte[6];
        public byte[] CreateCfg()
        {
            int unixTimestamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            byte[] bytes = new byte[] { 
                //DATE
                (byte)(0xFF & (unixTimestamp >> (24 - (8 * 3)))),
                (byte)(0xFF & (unixTimestamp >> (24 - (8 * 2)))),
                (byte)(0xFF & (unixTimestamp >> (24 - (8 * 1)))),
                (byte)(0xFF & (unixTimestamp >> (24 - (8 * 0)))),//0x450000 - 0x450003 4BYTE
                //SYSTEM ID
                (byte)(SYSTEMID.Value), //0x450004 - 0x450005 1BYTE
                //SW VERSION
                (byte)(MAJORVERSION.Value), //0x450005 - 0x450006 1BYTE
                (byte)(MINORVERSION.Value), //0x450006 - 0x450007 1BYTE
                (byte)(BUGFIXVERSION.Value), //0x450007 - 0x450008 1BYTE
                //BOOT SETTINGS
                (byte)(BootModeCB.SelectedIndex + ((uint)(BOOTTIMEOUT.Value)<<1)),
                //CAN SETTINGS
                (byte)((CAN1_BAUD_CB.SelectedIndex)+(CAN2_BAUD_CB.SelectedIndex<<2) + (CAN3_BAUD_CB.SelectedIndex<<4) + (CAN4_BAUD_CB.SelectedIndex<<6)),
                //UART SETTINGS
                (byte)((UARTBAUDCB.SelectedIndex)+(PRINTFDEBUGCB.SelectedIndex<<1)+
                //COMPANY SETTING
                (uint)(companynum.Value)<<2),
                //USER SETTINGS
                (byte)(usernamenum.Value),
                //BMS SETTINGS
                (byte)(CELLCAPAH.Value),
                (byte)(SERIESCELLCOUNT.Value),
                (byte)(PARALLELCELLCOUNT.Value),
                (byte)(DAISYCHAINCOUNT.Value),
                //CELL SETTINGS
                (byte)((Convert.ToInt32(maxcellv.Text)) & 0xFF),
                (byte)((Convert.ToInt32(maxcellv.Text))>>8),
                (byte)((Convert.ToInt32(mincellv.Text)) & 0xFF),
                (byte)((Convert.ToInt32(mincellv.Text))>>8),
                (byte)((Convert.ToInt32(tempsensorb.Text)) & 0xFF),
                (byte)((Convert.ToInt32(tempsensorb.Text))>>8),
                (byte)(defaultsoc.Value),
                (byte)(defaultsoh.Value),
                (byte)(maxtemp.Value),
                (byte)(mintemp.Value),
                (byte)(tempsensorcount.Value),
                (byte)(0xFF), // released
                //MAC ADDRESS
                MAC_ADDR[0],
                MAC_ADDR[1],
                MAC_ADDR[2],
                MAC_ADDR[3],
                MAC_ADDR[4],
                MAC_ADDR[5],
                (byte)(0xFF),(byte)(0xFF),(byte)(0xFF),(byte)(0xFF),(byte)(0xFF),(byte)(0xFF), // padding bytes
                (byte)('!'),(byte)('C'),(byte)('F'),(byte)('G'),
                (byte)'E',(byte)'O',(byte)'C',(byte)';'
            };
            return bytes;
        }

        private void Config_Reader_Load(object sender, EventArgs e)
        {
            SYSTEMID.Value = SYSTEMID.Minimum;
            BOOTTIMEOUT.Value = BOOTTIMEOUT.Minimum;
            MAJORVERSION.Value = MAJORVERSION.Minimum;
            MINORVERSION.Value = MINORVERSION.Minimum;
            BUGFIXVERSION.Value = BUGFIXVERSION.Minimum;
            CELLCAPAH.Value = CELLCAPAH.Minimum;
            SERIESCELLCOUNT.Value = SERIESCELLCOUNT.Minimum;
            PARALLELCELLCOUNT.Value = PARALLELCELLCOUNT.Minimum;
            DAISYCHAINCOUNT.Value = DAISYCHAINCOUNT.Minimum;
            companynum.Value = companynum.Minimum;
            usernamenum.Value = usernamenum.Minimum;
            maxcellv.Text = "0";
            mincellv.Text = "0";
            tempsensorb.Text = "0";
            defaultsoc.Value = defaultsoc.Minimum;
            defaultsoh.Value = defaultsoh.Minimum;
            maxtemp.Value = maxtemp.Minimum;
            mintemp.Value = mintemp.Minimum;
            tempsensorcount.Value = tempsensorcount.Minimum;
        }

        private void Config_Reader_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralProgramClass.FormActive_CFG_Creator = false;
        }

        private void ReadCfgFromDevice_Click(object sender, EventArgs e)
        {

        }
        private void SaveOutputCfgFile_Click(object sender, EventArgs e)
        {
            byte[] cfg_bytes = CreateCfg();
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Config File|*.cfg";
            sfd.FilterIndex = 1;
            sfd.InitialDirectory = GeneralProgramClass.DefaultFileLocation;
            sfd.FileName = $"NXP_SW_VER_{MAJORVERSION.Value}_{MINORVERSION.Value}_{BUGFIXVERSION.Value}_COMPANY_{companynum.Value}_USER_{usernamenum.Value}_CFG_FILE";
            if (DialogResult.OK == sfd.ShowDialog())
            {
                GeneralProgramClass.DefaultFileLocation = sfd.FileName;
                File.WriteAllBytes(sfd.FileName, cfg_bytes);
                MessageBox.Show(sfd.FileName + " created!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                return;
            }
        }
    }
}
