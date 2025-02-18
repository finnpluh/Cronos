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
        private static void Prefix(Player newPlayer)
        {
            Notifications.Send("JOIN", $"Name: {newPlayer.NickName}", Color.green);
        }
    }
}
