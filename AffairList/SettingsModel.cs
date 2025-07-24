using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffairList
{
    public class SettingsModel
    {
        public string currentListFileFullPath;

        public Color textColor = Color.MediumSpringGreen;
        public Color bgtextColor = Color.Black;

        public Keys closeKey = Keys.F7;
        public Keys returnKey = Keys.F6;

        public bool autostartState = true;
        public bool askToDelete = true;

        public int x, y;
    }
}
