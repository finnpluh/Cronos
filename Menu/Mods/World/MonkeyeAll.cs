using Cronos.Menu.Management.Watch;
using Cronos.Menu.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Cronos.Menu.Mods.World
{
    public class MonkeyeAll
    {
        public static void Run()
        {
            if (CronosRoomUtilities.InRoom())
            {
                if (CronosRoomUtilities.IsMasterClient(NetworkSystem.Instance.LocalPlayer))
                {
                    Button button = CronosButtonUtilities.GetButtonFromName("Monkeye All");
                    foreach (MonkeyeAI monkeye in GameObject.FindObjectsOfType<MonkeyeAI>())
                    {
                        switch (button.optionIndex)
                        {
                            case 0:
                                monkeye.gameObject.transform.position = GorillaTagger.Instance.bodyCollider.transform.position;
                                break;
                            case 1:
                                monkeye.gameObject.transform.position = new Vector3(monkeye.gameObject.transform.position.x, int.MinValue, monkeye.gameObject.transform.position.z);
                                break;
                        }
                    }
                }
            }
        }
    }
}
