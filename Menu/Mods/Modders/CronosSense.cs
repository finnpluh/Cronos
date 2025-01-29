using HarmonyLib;
using PlayFab.ClientModels;
using PlayFab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GorillaGameModes;
using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
using Cronos.Menu.Utilities;
using Cronos.Menu.Management.Watch;
using ExitGames.Client.Photon;

namespace Cronos.Menu.Mods.Modders
{
    public class CronosSense
    {
        private static bool properties = false;

        public static void Run()
        {
            if (CronosRoomUtilities.InRoomWithOthers())
            {
                if (!properties)
                {
                    Hashtable hashtable = new Hashtable();
                    hashtable.Add("cronos", PluginInfo.Version);
                    PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);
                    properties = true;
                }

                foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerListOthers)
                {
                    VRRig vrrig = VRRigManager.GetVRRigFromPlayer(player);
                    if (player.CustomProperties.ContainsKey("cronos"))
                    {
                        string nametag = $"<color={CronosColorUtilities.Color32ToHTML(Cronos.Menu.Management.Watch.Cronos.theme)}>[CRONOS]</color> {vrrig.Creator.NickName}";
                        string nametag_outline = $"<color=black>[CRONOS]</color> {vrrig.Creator.NickName}";

                        if (vrrig.playerText1.text != nametag)
                            vrrig.playerText1.text = nametag;

                        if (vrrig.playerText2.text != nametag_outline)
                            vrrig.playerText2.text = nametag_outline;

                        if (!vrrig.playerText1.richText)
                            vrrig.playerText1.richText = true;
                    }
                }
            }
        }

        public static void Cleanup()
        {
            if (CronosRoomUtilities.InRoomWithOthers())
            {
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    if (!vrrig.isOfflineVRRig && !vrrig.isMyPlayer)
                    {
                        if (vrrig.playerText1.text != vrrig.Creator.NickName)
                            vrrig.playerText1.text = vrrig.Creator.NickName;

                        if (vrrig.playerText2.text != vrrig.Creator.NickName)
                            vrrig.playerText2.text = vrrig.Creator.NickName;

                        if (vrrig.playerText1.richText)
                            vrrig.playerText1.richText = false;
                    }
                }
            }

            if (properties)
            {
                PhotonNetwork.LocalPlayer.CustomProperties.Remove("cronos");
                properties = false;
            }
        }
    }
}
