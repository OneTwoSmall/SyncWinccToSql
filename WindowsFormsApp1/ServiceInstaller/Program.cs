using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace ServiceInstaller
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //String path = AppDomain.CurrentDomain.BaseDirectory + "MAutoUpdate.exe";
            ////同时启动自动更新程序
            //if (File.Exists(path))
            //{
            //    ProcessStartInfo processStartInfo = new ProcessStartInfo()
            //    {
            //        FileName = "MAutoUpdate.exe",
            //        Arguments = " ServiceInstaller 0"//1表示静默更新 0表示弹窗提示更新
            //    };
            //    Process proc = Process.Start(processStartInfo);
            //    if (proc != null)
            //    {
            //        proc.WaitForExit();
                   
            //    }
            //}
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
