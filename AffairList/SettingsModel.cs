using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffairList
{
    public class SettingsModel
    {
        public string currentListFileFullPath { get; set; }

        public Color textColor { get; set; }
        public Color bgColor { get; set; }

        public Keys closeKey { get; set; }
        public Keys returnKey { get; set; }

        public bool autostartState { get; set; }
        public bool askToDelete { get; set; }

        public int x { get; set; }
        public int y { get; set; }
    }
}
