using Cronos.Menu.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Menu.Mods.Self
{
    public class HoldRig
    {
        public static void Run()
        {
            if (ControllerInput.leftGrip())
            {
                if (GorillaTagger.Instance.offlineVRRig.enabled)
                    GorillaTagger.Instance.offlineVRRig.enabled = false;
                else
                {
                    GorillaTagger.Instance.offlineVRRig.transform.position = GorillaLocomotion.Player.Instance.leftControllerTransform.transform.position;
                    GorillaTagger.Instance.offlineVRRig.transform.rotation = GorillaLocomotion.Player.Instance.leftControllerTransform.transform.rotation;
                }
            }
            else if (ControllerInput.rightGrip())
            {
                if (GorillaTagger.Instance.offlineVRRig.enabled)
                    GorillaTagger.Instance.offlineVRRig.enabled = false;
                else
                {
                    GorillaTagger.Instance.offlineVRRig.transform.position = GorillaLocomotion.Player.Instance.rightControllerTransform.transform.position;
                    GorillaTagger.Instance.offlineVRRig.transform.rotation = GorillaLocomotion.Player.Instance.rightControllerTransform.transform.rotation;
                }
            }
            else
                Cleanup();
        }

        public static void Cleanup()
        {
            if (!GorillaTagger.Instance.offlineVRRig.enabled)
                GorillaTagger.Instance.offlineVRRig.enabled = true;
        }
    }
}
