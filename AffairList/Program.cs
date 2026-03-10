namespace AffairList;

internal static class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        ApplicationConfiguration.Initialize();

        using Mutex mutex = new Mutex(true, name: "AffairList", out bool createdNew);
        if (createdNew)
        {
            Application.Run(new AffairList());
        }
        else
        {
            if (!args.Contains("--autostart"))
                MessageBox.Show("The program (AffairList) has been already started!\nIf you dont see it check tray or try to press close or back buttons", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}