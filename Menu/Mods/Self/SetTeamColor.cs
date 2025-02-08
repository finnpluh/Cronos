using Cronos.Menu.Management.Watch;
using Cronos.Menu.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cronos.Menu.Mods.Self
{
    public class SetTeamColor
    {
        private static float cooldown;

        public static void Run()
        {
            if (CronosRoomUtilities.InRoom())
            {
                Button button = CronosButtonUtilities.GetButtonFromName("Set Team Color");
                switch (button.optionIndex)
                {
                    case 0:
                        MonkeBallGame.Instance.RequestSetTeam(1);
                        break;
                    case 1:
                        MonkeBallGame.Instance.RequestSetTeam(0);
                        break;
                }
            }
        }
    }
}
