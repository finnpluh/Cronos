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
        private static void Prefix(Player otherPlayer)
        {
            Notifications.Send("LEAVE", $"Name: {otherPlayer.NickName}", Color.red);
        }
    }
}
