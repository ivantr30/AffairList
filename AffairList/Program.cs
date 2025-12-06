using Microsoft.Win32;

namespace AffairList
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            ApplicationConfiguration.Initialize();
            if (args.Contains("--autostart"))
            {
                SystemEvents.SessionSwitch += SystemEvents_SessionSwitch;
            }
            else
            {
                using Mutex mutex = new Mutex(true, name: "AffairList", out bool createdNew);
                if (createdNew)
                {
                    Application.Run(new AffairList());
                }
                else
                {
                    MessageBox.Show("The program(AffairList) has been already started!" +
                        "\nif you dont see it check tray or try to press close or back buttons", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private static void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            using Mutex mutex = new Mutex(true, name: "AffairList", out bool createdNew);
            if (createdNew)
            {
                Application.Run(new AffairList());
            }
            else
            {
                MessageBox.Show("The program(AffairList) has been already started!" +
                    "\nif you dont see it check tray or try to press close or back buttons", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}