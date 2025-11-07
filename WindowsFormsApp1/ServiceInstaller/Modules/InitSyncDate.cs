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
            DateTime now = DateTime.Now;
            startDateT.Value = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0); ;
            endDataT.Value = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0); ;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            INIHelper.WriteString("Wincc归档设置", "manualStartTime", startDateT.Value.ToString("yyyy-MM-dd HH:mm:00"), _iniSettingFilePath);
            INIHelper.WriteString("Wincc归档设置", "manualEndTime", endDataT.Value.ToString("yyyy-MM-dd HH:mm:00"), _iniSettingFilePath);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            INIHelper.WriteString("Wincc归档设置", "manualStartTime", "", _iniSettingFilePath);
            INIHelper.WriteString("Wincc归档设置", "manualEndTime", "", _iniSettingFilePath);

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
