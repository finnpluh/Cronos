using Cronos.Menu.Management.Watch;
using Cronos.Menu.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Menu.Mods.Movement
{
    public class SlipControl
    {
        private static float amount;

        public static void Run()
        {
            Button button = CronosButtonUtilities.GetButtonFromName("Slip Control");
            switch (button.optionIndex)
            {
                case 0:
                    if (amount != 0.005f)
                        amount += 0.005f;
                    break;
                case 1:
                    if (amount != 0.015f)
                        amount += 0.015f;
                    break;
                case 2:
                    if (amount != 0.03f)
                        amount += 0.03f;
                    break;
                case 3:
                    if (amount != 0.045f)
                        amount += 0.045f;
                    break;
                case 4:
                    if (amount != 0.6f)
                        amount += 0.6f;
                    break;
            }

            if (GorillaLocomotion.Player.Instance.slideControl != amount)
                GorillaLocomotion.Player.Instance.slideControl = amount;
        }

        public static void Cleanup()
        {
            if (GorillaLocomotion.Player.Instance.slideControl != 0.0035f)
                GorillaLocomotion.Player.Instance.slideControl = 0.0035f;
        }
    }
}
