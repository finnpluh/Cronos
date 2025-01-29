using Cronos.Menu.Libraries;
using Cronos.Menu.Management.Watch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Menu.Utilities
{
    public class CronosButtonUtilities
    {
        public static Button GetButtonFromName(string name)
        {
            Button button = null;
            if (button == null)
                foreach (Button buttons in Cronos.Menu.Management.Watch.Cronos.modules)
                    if (buttons.title == name)
                        button = buttons;
            return button;
        }
    }
}
