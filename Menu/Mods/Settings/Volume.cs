using Cronos.Menu.Management.Watch;
using Cronos.Menu.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Menu.Mods.Settings
{
    public class Volume
    {
        public static void Run()
        {
            Button volume = CronosButtonUtilities.GetButtonFromName("Volume");
            switch (volume.optionIndex)
            {
                case 0:
                    Cronos.Menu.Management.Watch.Preferences.volume = 0f;
                    break;
                case 1:
                    Cronos.Menu.Management.Watch.Preferences.volume = 0.1f;
                    break;
                case 2:
                    Cronos.Menu.Management.Watch.Preferences.volume = 0.2f;
                    break;
                case 3:
                    Cronos.Menu.Management.Watch.Preferences.volume = 0.3f;
                    break;
                case 4:
                    Cronos.Menu.Management.Watch.Preferences.volume = 0.4f;
                    break;
                case 5:
                    Cronos.Menu.Management.Watch.Preferences.volume = 0.5f;
                    break;
                case 6:
                    Cronos.Menu.Management.Watch.Preferences.volume = 0.6f;
                    break;
                case 7:
                    Cronos.Menu.Management.Watch.Preferences.volume = 0.7f;
                    break;
                case 8:
                    Cronos.Menu.Management.Watch.Preferences.volume = 0.8f;
                    break;
                case 9:
                    Cronos.Menu.Management.Watch.Preferences.volume = 0.9f;
                    break;
                case 10:
                    Cronos.Menu.Management.Watch.Preferences.volume = 1f;
                    break;
            }
        }
    }
}
