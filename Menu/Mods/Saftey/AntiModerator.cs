using Cronos.Menu.Libraries;
using Cronos.Menu.Utilities;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cronos.Menu.Mods.Saftey
{
    public class AntiModerator
    {
        public static void Run()
        {
            if (CronosRoomUtilities.InRoomWithOthers())
            {
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    if (!vrrig.isMyPlayer && !vrrig.isOfflineVRRig)
                    {
                        if (vrrig.concatStringOfCosmeticsAllowed.Contains("LBAAK"))
                        {
                            Notifications.Send("CRONOS", "Disconnected, Moderator was in lobby", Color.magenta);
                            NetworkSystem.Instance.ReturnToSinglePlayer();
                        }
                    }
                }
            }
        }
    }
}
