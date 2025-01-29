using Cronos.Menu.Libraries;
using Cronos.Menu.Utilities;
using g3;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cronos.Menu.Mods.Self
{
    public class RigGun
    {
        private static Guns gun = null;
        private static GameObject pointer = null;
        private static LineRenderer line = null;

        public static void Run()
        {
            if (gun == null)
                gun = new Guns();

            if (pointer != gun.pointer)
                pointer = gun.pointer;

            if (line != gun.line)
                line = gun.line;

            gun.Create(() =>
            {
                float distance = Vector3.Distance(GorillaLocomotion.Player.Instance.bodyCollider.transform.position, GorillaTagger.Instance.offlineVRRig.bodyTransform.transform.position);

                if (GorillaTagger.Instance.offlineVRRig.enabled)
                    GorillaTagger.Instance.offlineVRRig.enabled = false;

                GorillaTagger.Instance.offlineVRRig.transform.position = gun.pointer.transform.position + new Vector3(0f, 0.75f + Mathf.Floor(distance) / 20, 0f);
            }, () => { GorillaTagger.Instance.offlineVRRig.enabled = true; }, false);
        }

        public static void Cleanup()
        {
            if (!GorillaTagger.Instance.offlineVRRig.enabled)
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            gun.Cleanup();
        }
    }
}
