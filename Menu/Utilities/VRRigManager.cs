using HarmonyLib;
using Pathfinding.RVO;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cronos.Menu.Utilities
{
    public class VRRigManager
    {
        public static VRRig GetRandomVRRig()
        {
            return GorillaParent.instance.vrrigs[UnityEngine.Random.Range(0, GorillaParent.instance.vrrigs.Count)];
        }

        public static VRRig GetVRRigFromPlayer(Player player)
        {
            return GorillaGameManager.instance.FindPlayerVRRig(player);
        }

        public static VRRig GetVRRigFromNetPlayer(NetPlayer player)
        {
            return GorillaGameManager.instance.FindPlayerVRRig(player);
        }

        public static VRRig GetVRRigFromPlayerId(string id)
        {
            VRRig player = null;
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                if (player.Creator.UserId == id)
                    player = vrrig;
            return player;
        }

        public static VRRig GetVRRigFromNickName(string name)
        {
            VRRig player = null;
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                if (player.Creator.NickName == name)
                    player = vrrig;
            return player;
        }

        public static bool VRRigIsTagged(VRRig vrrig)
        {
            return vrrig.mainSkin.material.name.ToLower().Contains("fected") || vrrig.mainSkin.material.name.ToLower().Contains("it");
        }

        public static void FixVRRigColors(VRRig vrrig)
        {
            if (!CronosButtonUtilities.GetButtonFromName("Chams").toggled)
                if (vrrig.mainSkin.material.name.Contains("gorilla_body"))
                    if (vrrig.mainSkin.material.color != vrrig.playerColor)
                        vrrig.mainSkin.material.color = vrrig.playerColor;
        }
    }
}
