using Cronos.Menu.Libraries;
using Cronos.Menu.Utilities;
using GorillaLocomotion.Gameplay;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static g3.SetGroupBehavior;

namespace Cronos.Menu.Mods.World
{
    public class FlingRopeGun
    {
        private static Guns gun = null;
        private static GameObject pointer = null;
        private static LineRenderer line = null;

        public static void Run()
        {
            if (gun == null)
                gun = new Guns();

            if (CronosRoomUtilities.InRoom())
            {
                if (pointer != gun.pointer)
                    pointer = gun.pointer;

                if (line != gun.line)
                    line = gun.line;

                gun.Create(() =>
                {
                    GorillaRopeSwing rope = gun.raycast.collider.GetComponentInParent<GorillaRopeSwing>();
                    RopeSwingManager.instance.SendSetVelocity_RPC(rope.ropeId, 1, GorillaTagger.Instance.bodyCollider.gameObject.transform.position, true);
                });
            }
            else
                gun.Cleanup();
        }

        public static void Cleanup() => gun.Cleanup();
    }
}
