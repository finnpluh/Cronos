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
    [HarmonyPatch(typeof(MonoBehaviourPunCallbacks), "OnPlayerEnteredRoom")]
    public class OnPlayerEnteredRoomPatch : MonoBehaviour
    {
        private static Player player;

        private static void Prefix(Player newPlayer)
        {
            if (newPlayer != PhotonNetwork.LocalPlayer)
            {
                if (newPlayer != player)
                {
                    Notifications.Send("<color=green>Room</color>", $"Join: {newPlayer.NickName}");
                    player = newPlayer;
                }
            }
        }
    }
}
