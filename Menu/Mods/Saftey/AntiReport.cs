using Cronos.Menu.Libraries;
using Cronos.Menu.Management.Watch;
using Cronos.Menu.Utilities;
using Pathfinding.RVO;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Viveport;

namespace Cronos.Menu.Mods.Saftey
{
    public class AntiReport
    {
        public static void Run()
        {
            if (CronosRoomUtilities.InRoomWithOthers())
            {
                foreach (GorillaPlayerScoreboardLine lines in GorillaScoreboardTotalUpdater.allScoreboardLines)
                {
                    if (lines.linePlayer == NetworkSystem.Instance.LocalPlayer)
                    {
                        foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                        {
                            if (!vrrig.isOfflineVRRig && !vrrig.isMyPlayer)
                            {
                                float distance = 0.5f + PhotonNetwork.GetPing() / 250;
                                if (Vector3.Distance(vrrig.rightHandTransform.transform.position, lines.reportButton.gameObject.transform.position) < distance || Vector3.Distance(vrrig.leftHandTransform.transform.position, lines.reportButton.gameObject.transform.position) < distance)
                                {
                                    Notifications.Send("CRONOS", $"Disconnected, {vrrig.Creator.NickName} tried to report you", Color.magenta);
                                    NetworkSystem.Instance.ReturnToSinglePlayer();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}