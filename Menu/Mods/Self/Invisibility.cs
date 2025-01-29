using Cronos.Menu.Utilities;
using emotitron.Compression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Menu.Mods.Self
{
    public class Invisibility
    {
        private static bool cooldown = false;

        public static void Run()
        {
            if (ControllerInput.leftPrimary())
            {
                if (!cooldown)
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = !GorillaTagger.Instance.offlineVRRig.enabled;
                    if (!GorillaTagger.Instance.offlineVRRig.enabled)
                        GorillaTagger.Instance.offlineVRRig.transform.position = new UnityEngine.Vector3(GorillaTagger.Instance.offlineVRRig.transform.position.x, int.MinValue, GorillaTagger.Instance.offlineVRRig.transform.position.z);
                    cooldown = true;
                }
            }
            else
                if (cooldown)
                    cooldown = false;
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
