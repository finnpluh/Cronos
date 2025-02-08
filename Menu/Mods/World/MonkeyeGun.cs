using Cronos.Menu.Libraries;
using Cronos.Menu.Management.Watch;
using Cronos.Menu.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cronos.Menu.Mods.World
{
    public class MonkeyeGun
    {
        private static Guns gun = null;
        private static GameObject pointer = null;
        private static LineRenderer line = null;
        public static MonkeyeAI current = null;

        public static void Run()
        {
            if (gun == null)
                gun = new Guns();

            if (CronosRoomUtilities.InRoom())
            {
                if (CronosRoomUtilities.IsMasterClient(NetworkSystem.Instance.LocalPlayer))
                {
                    if (pointer != gun.pointer)
                        pointer = gun.pointer;

                    if (line != gun.line)
                        line = gun.line;

                    gun.Create(() =>
                    {
                        Button button = CronosButtonUtilities.GetButtonFromName("Monkeye Gun");
                        MonkeyeAI monkeye = gun.raycast.collider.GetComponentInParent<MonkeyeAI>();

                        if (monkeye != null)
                        {
                            if (current == null)
                                current = monkeye;

                            switch (button.optionIndex)
                            {
                                case 1:
                                    float random = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
                                    current.gameObject.transform.rotation *= Quaternion.Euler(random, random, random);
                                    break;
                                case 2:
                                    current.gameObject.transform.position = new Vector3(current.gameObject.transform.position.x, int.MinValue, current.gameObject.transform.position.z);
                                    break;
                            }
                        }

                        if (button.optionIndex == 0)
                        {
                            float distance = Vector3.Distance(current.gameObject.transform.position, GorillaTagger.Instance.headCollider.transform.position);
                            current.gameObject.transform.position = gun.pointer.transform.position + new Vector3(0f, Mathf.Floor(distance) / 5, 0f);
                        }

                    }, () => { current = null; }, false);
                }
                else
                    gun.Cleanup();
            }
            else
                gun.Cleanup();
        }

        public static void Cleanup() => gun.Cleanup();
    }
}
