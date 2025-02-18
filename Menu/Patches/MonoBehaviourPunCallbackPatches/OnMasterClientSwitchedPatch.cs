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
    [HarmonyPatch(typeof(MonoBehaviourPunCallbacks), "OnMasterClientSwitched")]
    public class OnMasterClientSwitchedPatch : MonoBehaviour
    {
        private static void Prefix(Player newMasterClient)
        {
            if (newMasterClient == PhotonNetwork.LocalPlayer)
                Notifications.Send("MASTER", "You are now master client", Color.yellow);
            else
                Notifications.Send("MASTER", $"{newMasterClient.NickName} is now master client", Color.yellow);
        }
    }
}
