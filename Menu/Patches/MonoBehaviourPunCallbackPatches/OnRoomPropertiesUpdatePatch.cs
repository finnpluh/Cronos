using ExitGames.Client.Photon;
using HarmonyLib;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cronos.Menu.Patches.MonoBehaviourPunCallbackPatches
{
    [HarmonyPatch(typeof(MonoBehaviourPunCallbacks), "OnRoomPropertiesUpdate")]
    public class OnRoomPropertiesUpdatePatch : MonoBehaviour
    {
        private static void Prefix(Hashtable propertiesThatChanged)
        {
            Notifications.Send("NOTICE", "Room properties changed", Color.blue);
        }
    }
}
