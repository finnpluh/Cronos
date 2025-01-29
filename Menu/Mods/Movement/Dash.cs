using Cronos.Menu.Management.Watch;
using Cronos.Menu.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cronos.Menu.Mods.Movement
{
    public class Dash
    {
        private static bool cooldown = false;

        public static void Run()
        {
            if (!ControllerInput.leftStick())
            {
                if (ControllerInput.rightPrimary())
                {
                    if (!cooldown)
                    {
                        float amount = 0f;
                        Button button = CronosButtonUtilities.GetButtonFromName("Dash");
                        switch (button.optionIndex)
                        {
                            case 0:
                                if (amount != 62.5f)
                                    amount = 62.5f;
                                break;
                            case 1:
                                if (amount != 125f)
                                    amount = 125f;
                                break;
                            case 2:
                                if (amount != 250f)
                                    amount = 250f;
                                break;
                            case 3:
                                if (amount != 500f)
                                    amount = 500f;
                                break;
                            case 4:
                                if (amount != 750f)
                                    amount = 750f;
                                break;
                        }

                        GorillaTagger.Instance.bodyCollider.attachedRigidbody.AddForce(GorillaTagger.Instance.headCollider.transform.forward * amount, ForceMode.Impulse);
                        cooldown = true;
                    }
                }
                else
                    if (cooldown)
                        cooldown = false;
            }
                    
        }
    }
}
