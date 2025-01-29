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
    public class BalloonAll
    {
        public static void Run()
        {
            Button button = CronosButtonUtilities.GetButtonFromName("Balloon All");
            foreach (BalloonHoldable balloon in GameObject.FindObjectsOfType<BalloonHoldable>())
            {
                if (balloon.currentState == TransferrableObject.PositionState.Dropped)
                {
                    if (balloon.ownerRig == GorillaTagger.Instance.offlineVRRig)
                    {
                        switch (button.optionIndex)
                        {
                            case 0:
                                balloon.gameObject.transform.position = GorillaTagger.Instance.bodyCollider.transform.position;
                                break;
                            case 1:
                                balloon.gameObject.transform.position = new Vector3(balloon.gameObject.transform.position.x, int.MinValue, balloon.gameObject.transform.position.z);
                                break;
                        }
                    }
                    else
                        balloon.WorldShareableRequestOwnership();
                }
            }
        }
    }
}
