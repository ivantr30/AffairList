using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffairList
{
    public class Config
    {
        string listFileFullPath = Application.StartupPath + "\\list.txt";
        string settingsFileFullPath = Application.StartupPath + "\\settings.txt";
        public static Color textColor = Color.MediumSpringGreen;
        public static Color bgtextColor = Color.Black;
        bool isConfirmed = true;
        bool musicState = true;
        bool autostartState = true;
        bool askToDelete = true;
        int currentVolume = 0;
        int x, y;
        Point lastPoint;
    }
}
