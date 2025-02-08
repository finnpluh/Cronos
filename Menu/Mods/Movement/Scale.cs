using Cronos.Menu.Management.Watch;
using Cronos.Menu.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Menu.Mods.Movement
{
    public class Scale
    {
        private static float amount;

        public static void Run()
        {
            Button speed = CronosButtonUtilities.GetButtonFromName("Scale");
            switch (speed.optionIndex)
            {
                case 0:
                    if (amount != 0.2f)
                        amount = 0.2f;
                    break;
                case 1:
                    if (amount != 0.4f)
                        amount = 0.4f;
                    break;
                case 2:
                    if (amount != 0.6f)
                        amount = 0.6f;
                    break;
                case 3:
                    if (amount != 0.8f)
                        amount = 0.8f;
                    break;
                case 4:
                    if (amount != 1.2f)
                        amount = 1.2f;
                    break;
                case 5:
                    if (amount != 1.4f)
                        amount = 1.4f;
                    break;
                case 6:
                    if (amount != 1.6f)
                        amount = 1.6f;
                    break;
                case 7:
                    if (amount != 1.8f)
                        amount = 1.8f;
                    break;
                case 8:
                    if (amount != 2f)
                        amount = 2f;
                    break;
                case 9:
                    if (amount != 2.2f)
                        amount = 2.2f;
                    break;
                case 10:
                    if (amount != 2.4f)
                        amount = 2.4f;
                    break;
                case 11:
                    if (amount != 2.6f)
                        amount = 2.6f;
                    break;
                case 12:
                    if (amount != 2.8f)
                        amount = 2.8f;
                    break;
                case 13:
                    if (amount != 3f)
                        amount = 3f;
                    break;
            }

            if (GorillaLocomotion.Player.Instance.scale != amount)
                GorillaLocomotion.Player.Instance.SetNativeScale(new NativeSizeChangerSettings { playerSizeScale = amount });
        }

        public static void Cleanup() => GorillaLocomotion.Player.Instance.SetNativeScale(new NativeSizeChangerSettings { playerSizeScale = 1f });
    }
}
