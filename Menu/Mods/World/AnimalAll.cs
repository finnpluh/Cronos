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
    public class AnimalAll
    {
        public static void Run()
        {
            Button button = CronosButtonUtilities.GetButtonFromName("Animal All");
            foreach (ThrowableBug animal in GameObject.FindObjectsOfType<ThrowableBug>())
            {
                switch (button.optionIndex)
                {
                    case 0:
                        animal.gameObject.transform.position = GorillaTagger.Instance.bodyCollider.transform.position;
                        break;
                    case 1:
                        animal.gameObject.transform.position = new Vector3(animal.gameObject.transform.position.x, int.MinValue, animal.gameObject.transform.position.z);
                        break;
                }
            }
        }
    }
}
