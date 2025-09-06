namespace AffairList
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            
            ApplicationConfiguration.Initialize();

            using (Mutex mutex = new Mutex(true, name:"AffairList", out bool createdNew))
            {
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
}