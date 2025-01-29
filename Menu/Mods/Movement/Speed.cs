using Cronos.Menu.Management.Watch;
using Cronos.Menu.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Menu.Mods.Movement
{
    public class Speed
    {
        private static float amount;

        public static void Run()
        {
            Button speed = CronosButtonUtilities.GetButtonFromName("Speed");
            switch (speed.optionIndex)
            {
                case 0:
                    if (amount != 6.6f)
                        amount = 6.6f;
                    break;
                case 1:
                    if (amount != 6.7f)
                        amount = 6.7f;
                    break;
                case 2:
                    if (amount != 6.8f)
                        amount = 6.8f;
                    break;
                case 3:
                    if (amount != 6.9f)
                        amount = 6.9f;
                    break;
                case 4:
                    if (amount != 7f)
                        amount = 7f;
                    break;
                case 5:
                    if (amount != 7.1f)
                        amount = 7.1f;
                    break;
                case 6:
                    if (amount != 7.2f)
                        amount = 7.2f;
                    break;
                case 7:
                    if (amount != 7.3f)
                        amount = 7.3f;
                    break;
                case 8:
                    if (amount != 7.4f)
                        amount = 7.4f;
                    break;
                case 9:
                    if (amount != 7.5f)
                        amount = 7.5f;
                    break;
                case 10:
                    if (amount != 7.6f)
                        amount = 7.6f;
                    break;
                case 11:
                    if (amount != 7.7f)
                        amount = 7.7f;
                    break;
                case 12:
                    if (amount != 7.8f)
                        amount = 7.8f;
                    break;
                case 13:
                    if (amount != 7.9f)
                        amount = 7.9f;
                    break;
                case 14:
                    if (amount != 8f)
                        amount = 8f;
                    break;
                case 15:
                    if (amount != 8.1f)
                        amount = 8.1f;
                    break;
                case 16:
                    if (amount != 8.2f)
                        amount = 8.2f;
                    break;
                case 17:
                    if (amount != 8.3f)
                        amount = 8.3f;
                    break;
                case 18:
                    if (amount != 8.4f)
                        amount = 8.4f;
                    break;
                case 19:
                    if (amount != 8.5f)
                        amount = 8.5f;
                    break;
                case 20:
                    if (amount != 8.6f)
                        amount = 8.6f;
                    break;
            }

            if (GorillaLocomotion.Player.Instance.maxJumpSpeed != amount)
                GorillaLocomotion.Player.Instance.maxJumpSpeed = amount;
        }

        public static void Cleanup() => GorillaLocomotion.Player.Instance.maxJumpSpeed = 6.5f;
    }
}
