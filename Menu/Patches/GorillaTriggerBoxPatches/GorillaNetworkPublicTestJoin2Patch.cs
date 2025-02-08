using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cronos.Menu.Patches.GorillaTriggerBoxPatches
{
    [HarmonyPatch(typeof(GorillaNetworkPublicTestJoin2))]
    [HarmonyPatch("GracePeriod", MethodType.Normal)]
    public class GorillaNetworkPublicTestJoin2Patch : MonoBehaviour
    {
        private static bool Prefix()
        {
            return false;
        }
    }
}
