using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cronos.Menu.Patches.VRRigPatches
{
    [HarmonyPatch(typeof(VRRigJobManager), "DeregisterVRRig")]
    public class DeregisterVRRigPatch : MonoBehaviour
    {
        private static void Prefix(VRRigJobManager __instance, VRRig rig)
        {
            if (rig.isMyPlayer)
                __instance.RegisterVRRig(rig);
        }
    }
}
