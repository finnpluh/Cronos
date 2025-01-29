using Cronos.Menu.Management.Watch;
using Cronos.Menu.Utilities;
using ExitGames.Client.Photon;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Menu.Mods.Saftey
{
    public class FullGhostMode
    {
        public static void Run()
        {
            if (!Cronos.Menu.Management.Watch.Settings.ghost_mode)
            {
                Cronos.Menu.Management.Watch.Settings.volume = 0f;
                Cronos.Menu.Management.Watch.Cronos.watch.layer = 19;
                Cronos.Menu.Management.Watch.Cronos.screen.layer = 19;
                Cronos.Menu.Management.Watch.Cronos.watch.transform.Find("HuntWatch_ScreenLocal").Find("Canvas").gameObject.layer = 19;
                Cronos.Menu.Management.Watch.Settings.ghost_mode = true;
            }

            foreach (Button module in Cronos.Menu.Management.Watch.Cronos.modules)
            {
                if (module.isToggleable)
                {
                    if (module.blatant)
                    {
                        if (module.toggled)
                        {
                            if (module.disableAction != null)
                                module.disableAction();
                            module.toggled = false;
                        }
                    }
                }
            }
        }

        public static void Cleanup()
        {
            if (Cronos.Menu.Management.Watch.Settings.ghost_mode)
            {
                Cronos.Menu.Management.Watch.Cronos.watch.layer = 0;
                Cronos.Menu.Management.Watch.Cronos.screen.layer = 0;
                Cronos.Menu.Management.Watch.Cronos.watch.transform.Find("HuntWatch_ScreenLocal").Find("Canvas").gameObject.layer = 0;
                Cronos.Menu.Management.Watch.Settings.ghost_mode = false;
            }
        }
    }
}
