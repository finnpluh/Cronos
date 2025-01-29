using Cronos.Menu.Libraries;
using Cronos.Menu.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cronos.Menu.Mods.Movement
{
    public class TeleportGun
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
                GorillaLocomotion.Player.Instance.transform.position = pointer.transform.position;
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.velocity = Vector3.zero;
            });
        }

        public static void Cleanup() => gun.Cleanup();
    }
}
