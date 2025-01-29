using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cronos.Menu.Patches.VRRigPatches
{
    [HarmonyPatch(typeof(VRRig), "OnDisable")]
    public class OnDisablePatch : MonoBehaviour
    {
        private static bool Prefix(VRRig __instance)
        {
            return !(__instance == GorillaTagger.Instance.offlineVRRig);
        }
    }
}
