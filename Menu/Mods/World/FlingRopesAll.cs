using Cronos.Menu.Utilities;
using GorillaLocomotion.Gameplay;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cronos.Menu.Mods.World
{
    public class FlingRopesAll
    {
        public static void Run()
        {
            if (CronosRoomUtilities.InRoom())
                foreach (GorillaRopeSwing rope in GameObject.FindObjectsOfType<GorillaRopeSwing>())
                    RopeSwingManager.instance.SendSetVelocity_RPC(rope.ropeId, 1, GorillaTagger.Instance.bodyCollider.gameObject.transform.position, true);
        }
    }
}
