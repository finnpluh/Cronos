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
    public class Fly
    {
        private static float speed;

        public static void Run()
        {
            if (!ControllerInput.leftStick())
            {
                if (ControllerInput.rightTrigger())
                {
                    Button button = CronosButtonUtilities.GetButtonFromName("Fly");
                    switch (button.optionIndex)
                    {
                        case 0:
                            if (speed != 0.1f)
                                speed = 0.1f;
                            break;
                        case 1:
                            if (speed != 0.15f)
                                speed = 0.15f;
                            break;
                        case 2:
                            if (speed != 0.25f)
                                speed = 0.25f;
                            break;
                        case 3:
                            if (speed != 0.3f)
                                speed = 0.3f;
                            break;
                        case 4:
                            if (speed != 0.35f)
                                speed = 0.35f;
                            break;
                    }

                    GorillaLocomotion.Player.Instance.transform.position += GorillaLocomotion.Player.Instance.headCollider.gameObject.transform.forward * speed;
                    GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.velocity = Vector3.zero;
                }
            }
        }
    }
}
