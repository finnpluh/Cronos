using Cronos.Menu.Management.Watch;
using Cronos.Menu.Utilities;
using Fusion;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cronos.Menu.Mods.Player
{
    [HarmonyPatch(typeof(GorillaTagger), "sphereCastRadius", MethodType.Getter)]
    public class Reach : MonoBehaviour
    {
        private static void Postfix(GorillaTagger __instance, ref float __result)
        {
            Button button = CronosButtonUtilities.GetButtonFromName("Reach");
            if (button.toggled)
            {
                switch (button.optionIndex)
                {
                    case 0:
                        if (__result != 0.3f)
                            __result = 0.3f;
                        break;
                    case 1:
                        if (__result != 0.4f)
                            __result = 0.4f;
                        break;
                    case 2:
                        if (__result != 0.5f)
                            __result = 0.5f;
                        break;
                    case 3:
                        if (__result != 0.6f)
                            __result = 0.6f;
                        break;
                    case 4:
                        if (__result != 0.7f)
                            __result = 0.7f;
                        break;
                    case 5:
                        if (__result != 0.8f)
                            __result = 0.8f;
                        break;
                }
            }
        }
    }
}
