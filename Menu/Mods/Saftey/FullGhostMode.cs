using Cronos.Menu.Management.Watch;
using Cronos.Menu.Utilities;
using ExitGames.Client.Photon;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Cronos.Menu.Management.Watch.Preferences;
using static Cronos.Menu.Management.Watch.Cronos;

namespace Cronos.Menu.Mods.Saftey
{
    public class FullGhostMode
    {
        public static void Run()
        {
            if (!preferences[1])
            {
                if (volume != 0f)
                    volume = 0f;

                if (watch.layer != 19)
                    watch.layer = 19;

                if (screen.layer != 19)
                    screen.layer = 19;

                if (watch.transform.Find("HuntWatch_ScreenLocal").Find("Canvas").gameObject.layer != 19)
                    watch.transform.Find("HuntWatch_ScreenLocal").Find("Canvas").gameObject.layer = 19;

                preferences[1] = true;
            }

            foreach (Button[] modules in pages)
            {
                foreach (Button module in modules)
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
        }

        public static void Cleanup()
        {
            if (preferences[1])
            {
                if (watch.layer != 0)
                    watch.layer = 0;

                if (screen.layer != 0)
                    screen.layer = 0;

                if (watch.transform.Find("HuntWatch_ScreenLocal").Find("Canvas").gameObject.layer != 0)
                    watch.transform.Find("HuntWatch_ScreenLocal").Find("Canvas").gameObject.layer = 0;

                preferences[1] = false;
            }
        }
    }
}
