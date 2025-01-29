using Cronos.Menu.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Menu.Mods.Self
{
    public class Ghost
    {
        private static bool cooldown = false;

        public static void Run()
        {
            if (!ControllerInput.leftStick())
            {
                if (ControllerInput.rightPrimary())
                {
                    if (!cooldown)
                    {
                        GorillaTagger.Instance.offlineVRRig.enabled = !GorillaTagger.Instance.offlineVRRig.enabled;
                        cooldown = true;
                    }
                }
                else
                    if (cooldown)
                        cooldown = false;
            }
        }

        public static void Cleanup()
        {
            if (!GorillaTagger.Instance.offlineVRRig.enabled)
                GorillaTagger.Instance.offlineVRRig.enabled = true;

            if (cooldown)
                cooldown = false;
        }
    }
}
