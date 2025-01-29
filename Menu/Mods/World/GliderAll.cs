using Cronos.Menu.Management.Watch;
using Cronos.Menu.Utilities;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cronos.Menu.Mods.World
{
    public class GliderAll
    {
        public static void Run()
        {
            Button button = CronosButtonUtilities.GetButtonFromName("Glider All");
            foreach (GliderHoldable glider in GameObject.FindObjectsOfType<GliderHoldable>())
            {
                if (glider.Owner == NetworkSystem.Instance.LocalPlayer)
                {
                    switch (button.optionIndex)
                    {
                        case 0:
                            glider.gameObject.transform.position = GorillaTagger.Instance.bodyCollider.transform.position;
                            break;
                        case 1:
                            glider.Respawn();
                            break;
                    }
                }
                else
                    glider.OnHover((InteractionPoint)null, (GameObject)null);
            }
        }
    }
}
