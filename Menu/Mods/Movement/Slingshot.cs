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
    public class Slingshot
    {
        private static float speed;

        public static void Run()
        {
            if (!ControllerInput.rightStick())
            {
                if (ControllerInput.leftTrigger())
                {
                    Button button = CronosButtonUtilities.GetButtonFromName("Slingshot");
                    switch (button.optionIndex)
                    {
                        case 0:
                            speed += 0.001f;
                            break;
                        case 1:
                            speed += 0.0015f;
                            break;
                        case 2:
                            speed += 0.0025f;
                            break;
                        case 3:
                            speed += 0.003f;
                            break;
                        case 4:
                            speed += 0.0035f;
                            break;
                    }

                    GorillaLocomotion.Player.Instance.transform.position += GorillaLocomotion.Player.Instance.headCollider.gameObject.transform.forward * speed;
                }
                else
                    if (speed != 0f)
                        speed = 0f;
            }
        }
    }
}
