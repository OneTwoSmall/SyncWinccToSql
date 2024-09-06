using DBHelpClass.DBHelper;
using System;
using System.Windows.Forms;

namespace ServiceInstaller.Modules
{
    public partial class InitSyncDate : Form
    {

        private string _iniSettingFilePath = $"{Application.StartupPath}\\Setting.INI";
        public InitSyncDate()
        {
            InitializeComponent();
        }

        private void InitSyncDate_Load(object sender, EventArgs e)
        {
            startDateT.Value = DateTime.Now.AddDays(-1);
            endDataT.Value = DateTime.Now;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            INIHelper.WriteString("Wincc归档设置", "manualStartTime", startDateT.Value.ToString("yyyy-MM-dd"), _iniSettingFilePath);
            INIHelper.WriteString("Wincc归档设置", "manualEndTime", endDataT.Value.ToString("yyyy-MM-dd"), _iniSettingFilePath);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
