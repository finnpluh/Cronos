using Cronos.Menu.Libraries;
using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cronos.Menu.Patches.MonoBehaviourPunCallbackPatches
{
    [HarmonyPatch(typeof(MonoBehaviourPunCallbacks), "OnPlayerLeftRoom")]
    public class OnPlayerLeftRoomPatch : MonoBehaviour
    {
        private static Player player;

        private static void Prefix(Player otherPlayer)
        {
            if (otherPlayer != PhotonNetwork.LocalPlayer)
            {
                if (otherPlayer != player)
                {
                    Notifications.Send("<color=red>Room</color>", $"Leave: {otherPlayer.NickName}");
                    player = otherPlayer;
                }
            }
        }
    }
}
