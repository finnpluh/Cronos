using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Menu.Mods.Settings
{
    public class GhostRig
    {
        public static void Update(bool toggled)
        {
            if (Cronos.Menu.Management.Watch.Preferences.preferences[2] != toggled)
                Cronos.Menu.Management.Watch.Preferences.preferences[2] = toggled;
        }
    }
}
