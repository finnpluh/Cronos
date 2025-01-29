using Cronos.Menu.Libraries;
using Cronos.Menu.Utilities;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Menu.Mods.Modders
{
    public class ModChecker
    {
        public static void Run()
        {
            if (CronosRoomUtilities.InRoomWithOthers())
            {
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    if (!vrrig.isOfflineVRRig && !vrrig.isMyPlayer)
                    {
                        Photon.Realtime.Player player = vrrig.Creator.GetPlayerRef();
                        if (player.CustomProperties.ContainsKey("genesis"))
                            Notifications.Send("<color=purple>Mod Checker</color>", $"{player.NickName} is using Genesis");
                        else if (player.CustomProperties.ContainsKey("void"))
                            Notifications.Send("<color=purple>Mod Checker</color>", $"{player.NickName} is using Void Buddies");
                        else if (player.CustomProperties.ContainsKey("6XpyykmrCthKhFeUfkYGxv7xnXpoe2"))
                            Notifications.Send("<color=purple>Mod Checker</color>", $"{player.NickName} is using Colossal Cheat Menu");
                        else if (player.CustomProperties.ContainsKey("cronos"))
                            Notifications.Send("<color=purple>Mod Checker</color>", $"{player.NickName} is using Cronos");
                        else if (player.CustomProperties.ContainsKey("GrateVersion"))
                            Notifications.Send("<color=purple>Mod Checker</color>", $"{player.NickName} is using Grate");
                        else if (player.CustomProperties.ContainsKey("BananaOS"))
                            Notifications.Send("<color=purple>Mod Checker</color>", $"{player.NickName} is using BananaOS");
                        else if (player.CustomProperties.ContainsKey("GorillaShirts"))
                            Notifications.Send("<color=purple>Mod Checker</color>", $"{player.NickName} is using GorillaShirts");
                        else
                            Notifications.Send("<color=purple>Mod Checker</color>", $"Noone else in your room is using mods");
                    }
                }
            }
        }
    }
}
