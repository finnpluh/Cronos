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
    public class AnimalGun
    {
        private static Guns gun = null;
        private static GameObject pointer = null;
        private static LineRenderer line = null;
        public static ThrowableBug current = null;

        public static void Run()
        {
            if (gun == null)
                gun = new Guns();

            if (pointer != gun.pointer)
                pointer = gun.pointer;

            if (line != gun.line)
                line = gun.line;

            gun.Create(() =>
            {
                Button button = CronosButtonUtilities.GetButtonFromName("Animal Gun");
                ThrowableBug animal = gun.raycast.collider.GetComponentInParent<ThrowableBug>();

                if (animal != null)
                {
                    if (current == null)
                        current = animal;

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

        public static void Cleanup() => gun.Cleanup();
    }
}
