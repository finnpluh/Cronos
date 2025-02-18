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
    public class Reach
    {
        private static float amount;

        public static void Run()
        {
            Button button = CronosButtonUtilities.GetButtonFromName("Reach");
            switch (button.optionIndex)
            {
                case 0:
                    if (amount != 0.3f)
                        amount = 0.3f;
                    break;
                case 1:
                    if (amount != 0.4f)
                        amount = 0.4f;
                    break;
                case 2:
                    if (amount != 0.5f)
                        amount = 0.5f;
                    break;
                case 3:
                    if (amount != 0.6f)
                        amount = 0.6f;
                    break;
                case 4:
                    if (amount != 0.7f)
                        amount = 0.7f;
                    break;
                case 5:
                    if (amount != 0.8f)
                        amount = 0.8f;
                    break;
            }

            if (GorillaTagger.Instance.sphereCastRadius != amount)
                GorillaTagger.Instance.SetTagRadiusOverrideThisFrame(amount);
        }

        public static void Cleanup()
        {
            if (GorillaTagger.Instance.sphereCastRadius != 0.03f)
                GorillaTagger.Instance.SetTagRadiusOverrideThisFrame(0.03f);
        }
    }
}
