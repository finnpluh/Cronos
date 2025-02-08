using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cronos.Menu.Patches.GorillaTriggerBoxPatches
{
    [HarmonyPatch(typeof(GorillaNetworkPublicTestsJoin))]
    [HarmonyPatch("GracePeriod", MethodType.Normal)]
    public class GorillaNetworkPublicTestsJoinPatch : MonoBehaviour
    {
        private static bool Prefix()
        {
            return false;
        }
    }
}
