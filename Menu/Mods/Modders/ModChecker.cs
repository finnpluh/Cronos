using Cronos.Menu.Libraries;
using Cronos.Menu.Utilities;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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
                            Notifications.Send("CRONOS", $"{player.NickName} is using Genesis", Color.magenta);
                        else if (player.CustomProperties.ContainsKey("void"))
                            Notifications.Send("CRONOS", $"{player.NickName} is using Void Buddies", Color.magenta);
                        else if (player.CustomProperties.ContainsKey("6XpyykmrCthKhFeUfkYGxv7xnXpoe2"))
                            Notifications.Send("CRONOS", $"{player.NickName} is using Colossal Cheat Menu", Color.magenta);
                        else if (player.CustomProperties.ContainsKey("cronos"))
                            Notifications.Send("CRONOS", $"{player.NickName} is using Cronos", Color.magenta);
                        else if (player.CustomProperties.ContainsKey("GrateVersion"))
                            Notifications.Send("CRONOS", $"{player.NickName} is using Grate", Color.magenta);
                        else if (player.CustomProperties.ContainsKey("BananaOS"))
                            Notifications.Send("CRONOS", $"{player.NickName} is using BananaOS", Color.magenta);
                        else if (player.CustomProperties.ContainsKey("GorillaShirts"))
                            Notifications.Send("CRONOS", $"{player.NickName} is using GorillaShirts", Color.magenta);
                        else
                            Notifications.Send("CRONOS", $"Noone else in your room is using mods", Color.magenta);
                    }
                }
            }
        }
    }
}
