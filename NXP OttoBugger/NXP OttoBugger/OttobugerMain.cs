using System.IO;

namespace NXP_OttoBugger
{
    public partial class OttobuggerV3 : Form
    {
        public OttobuggerV3()
        {
            InitializeComponent();
        }

        private void OttobuggerV3_Load(object sender, EventArgs e)
        {

            SW_UPD_GB.Enabled = false;
            TEST_GB.Enabled = false;
            string dosyaYolu = "set.csv";
            using (StreamReader reader = new StreamReader(dosyaYolu))
            {
                string data = reader.ReadToEnd();
                GeneralClass.ReWriteDatas(data.Split(','));
            }
        }

        private void OpenTest_Window_Button_Click(object sender, EventArgs e)
        {

        }
    }
}
