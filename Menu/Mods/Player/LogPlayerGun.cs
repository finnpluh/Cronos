using Cronos.Menu.Libraries;
using Cronos.Menu.Management.Watch;
using Cronos.Menu.Mods.Movement;
using Cronos.Menu.Utilities;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cronos.Menu.Mods.Player
{
    public class LogPlayerGun
    {
        private static Guns gun = null;
        private static GameObject pointer = null;
        private static LineRenderer line = null;

        public static void Run()
        {
            if (gun == null)
                gun = new Guns();

            if (CronosRoomUtilities.InRoomWithOthers())
            {
                if (pointer != gun.pointer)
                    pointer = gun.pointer;

                if (line != gun.line)
                    line = gun.line;

                gun.Create(() =>
                {
                    VRRig vrrig = gun.raycast.collider.GetComponentInParent<VRRig>();
                    if (!vrrig.isOfflineVRRig && !vrrig.isMyPlayer)
                    {
                        Button button = CronosButtonUtilities.GetButtonFromName("Log Player Gun");
                        switch (button.optionIndex)
                        {
                            case 0:
                                Notifications.Send("PLAYER", $"Player ID for {vrrig.Creator.NickName}: {vrrig.Creator.UserId}", Color.cyan);
                                break;
                            case 1:
                                Notifications.Send("PLAYER", $"Color for {vrrig.Creator.NickName}: {Mathf.Floor(vrrig.playerColor.r * 9f)}, {Mathf.Floor(vrrig.playerColor.g * 9f)}, {Mathf.Floor(vrrig.playerColor.b * 9f)}", Color.cyan);
                                break;
                            case 2:
                                Notifications.Send("PLAYER", $"{vrrig.Creator.NickName} has {vrrig.concatStringOfCosmeticsAllowed.Count(period => period == '.')} cosmetics", Color.cyan);
                                break;
                        }

                    }
                });
            }
            else
                gun.Cleanup();
        }

        public static void Cleanup() => gun.Cleanup();
    }
}
