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
    public class LongArms
    {
        private static float length;

        public static void Run()
        {
            Button longarms = CronosButtonUtilities.GetButtonFromName("Long Arms");
            switch (longarms.optionIndex)
            {
                case 0:
                    if (length != 1.02f)
                        length = 1.02f;
                    break;
                case 1:
                    if (length != 1.04f)
                        length = 1.04f;
                    break;
                case 2:
                    if (length != 1.05f)
                        length = 1.05f;
                    break;
                case 3:
                    if (length != 1.1f)
                        length = 1.1f;
                    break;
                case 4:
                    if (length != 1.15f)
                        length = 1.15f;
                    break;
                case 5:
                    if (length != 1.2f)
                        length = 1.2f;
                    break;
                case 6:
                    if (length != 1.25f)
                        length = 1.25f;
                    break;
                case 7:
                    if (length != 1.3f)
                        length = 1.3f;
                    break;
                case 8:
                    if (length != 1.35f)
                        length = 1.35f;
                    break;
                case 9:
                    if (length != 1.4f)
                        length = 1.4f;
                    break;
                case 10:
                    if (length != 1.45f)
                        length = 1.45f;
                    break;
                case 11:
                    if (length != 1.5f)
                        length = 1.5f;
                    break;
            }

            if (GorillaLocomotion.Player.Instance.gameObject.transform.localScale != new Vector3(length, length, length))
                GorillaLocomotion.Player.Instance.gameObject.transform.localScale = new Vector3(length, length, length);
        }

        public static void Cleanup() => GorillaLocomotion.Player.Instance.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
