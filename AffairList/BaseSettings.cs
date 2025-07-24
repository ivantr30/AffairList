using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffairList
{
    public class BaseSettings
    {
        public readonly Color textColor = Color.MediumSpringGreen;
        public readonly Color bgtextColor = Color.Black;

        public readonly Keys closeKey = Keys.F7;
        public readonly Keys returnKey = Keys.F6;

        public readonly bool autostartState = true;
        public readonly bool askToDelete = true;

        public readonly int x = 0, y = 0;
    }
}
