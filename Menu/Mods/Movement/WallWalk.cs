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
    public class WallWalk
    {
        private static RaycastHit left;
        private static RaycastHit right;
        private static RaycastHit closest;
        private static float strength;

        public static void Run()
        {
            if (ControllerInput.leftGrip() || ControllerInput.rightGrip())
            {
                Physics.Raycast(GorillaTagger.Instance.leftHandTransform.transform.position, GorillaTagger.Instance.leftHandTransform.transform.right, out left, 1f, int.MaxValue);
                Physics.Raycast(GorillaTagger.Instance.rightHandTransform.transform.position, -GorillaTagger.Instance.rightHandTransform.transform.right, out right, 1f, int.MaxValue);

                if (left.distance > right.distance)
                    closest = right;
                else
                    closest = left;

                Button button = CronosButtonUtilities.GetButtonFromName("Wall Walk");
                switch (button.optionIndex)
                {
                    case 0:
                        if (strength != 0.03f)
                            strength = 0.03f;
                        break;
                    case 1:
                        if (strength != 0.05f)
                            strength = 0.05f;
                        break;
                    case 2:
                        if (strength != 0.07f)
                            strength = 0.07f;
                        break;
                    case 3:
                        if (strength != 0.09f)
                            strength = 0.09f;
                        break;
                    case 4:
                        if (strength != 0.11f)
                            strength = 0.11f;
                        break;
                }

                if (closest.distance < 1f)
                    GorillaTagger.Instance.bodyCollider.attachedRigidbody.velocity -= closest.normal * strength;
            }
        }
    }
}
