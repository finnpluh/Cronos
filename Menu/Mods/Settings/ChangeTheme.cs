using Cronos.Menu.Management.Watch;
using Cronos.Menu.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cronos.Menu.Mods.Settings
{
    public class ChangeTheme
    {
        public static void Run()
        {
            float divisor = 3f;
            Button button = CronosButtonUtilities.GetButtonFromName("Change Theme");
            switch (button.optionIndex)
            {
                case 0:
                    Cronos.Menu.Management.Watch.Settings.animated = true;
                    Cronos.Menu.Management.Watch.Settings.theme[0] = CronosColorUtilities.HTMLToColor32("#354DE7");
                    Cronos.Menu.Management.Watch.Settings.theme[1] = CronosColorUtilities.HTMLToColor32("#745CEE");
                    break;
                case 1:
                    Cronos.Menu.Management.Watch.Settings.animated = true;
                    Cronos.Menu.Management.Watch.Settings.theme[0] = Color.red / divisor;
                    Cronos.Menu.Management.Watch.Settings.theme[1] = Color.red / divisor * 2;
                    break;
                case 2:
                    Cronos.Menu.Management.Watch.Settings.animated = true;
                    Cronos.Menu.Management.Watch.Settings.theme[0] = CronosColorUtilities.HTMLToColor32("#ff9700") / divisor;
                    Cronos.Menu.Management.Watch.Settings.theme[1] = CronosColorUtilities.HTMLToColor32("#ff9700") / divisor * 2;
                    break;
                case 3:
                    Cronos.Menu.Management.Watch.Settings.animated = true;
                    Cronos.Menu.Management.Watch.Settings.theme[0] = Color.yellow / divisor;
                    Cronos.Menu.Management.Watch.Settings.theme[1] = Color.yellow / divisor * 2;
                    break;
                case 4:
                    Cronos.Menu.Management.Watch.Settings.animated = true;
                    Cronos.Menu.Management.Watch.Settings.theme[0] = Color.green / divisor;
                    Cronos.Menu.Management.Watch.Settings.theme[1] = Color.green / divisor * 2;
                    break;
                case 5:
                    Cronos.Menu.Management.Watch.Settings.animated = true;
                    Cronos.Menu.Management.Watch.Settings.theme[0] = Color.cyan / divisor;
                    Cronos.Menu.Management.Watch.Settings.theme[1] = Color.cyan / divisor * 2;
                    break;
                case 6:
                    Cronos.Menu.Management.Watch.Settings.animated = true;
                    Cronos.Menu.Management.Watch.Settings.theme[0] = Color.blue / divisor;
                    Cronos.Menu.Management.Watch.Settings.theme[1] = Color.blue / divisor * 2;
                    break;
                case 7:
                    Cronos.Menu.Management.Watch.Settings.animated = true;
                    Cronos.Menu.Management.Watch.Settings.theme[0] = CronosColorUtilities.HTMLToColor32("#8300ff") / divisor;
                    Cronos.Menu.Management.Watch.Settings.theme[1] = CronosColorUtilities.HTMLToColor32("#8300ff") / divisor * 2;
                    break;
                case 8:
                    Cronos.Menu.Management.Watch.Settings.animated = true;
                    Cronos.Menu.Management.Watch.Settings.theme[0] = Color.magenta / divisor;
                    Cronos.Menu.Management.Watch.Settings.theme[1] = Color.magenta / divisor * 2;
                    break;
                case 9:
                    Cronos.Menu.Management.Watch.Settings.animated = false;
                    Cronos.Menu.Management.Watch.Settings.theme[0] = Color.grey / divisor / 2;
                    Cronos.Menu.Management.Watch.Settings.theme[1] = Color.grey / divisor / 2;
                    break;
            }
        }
    }
}
