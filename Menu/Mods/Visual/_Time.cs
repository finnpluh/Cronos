using Cronos.Menu.Management.Watch;
using Cronos.Menu.Utilities;
using Pathfinding.RVO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Valve.VR.SteamVR_PlayArea;

namespace Cronos.Menu.Mods.Visual
{
    public class _Time
    {
        public static void Run()
        {
            Button button = CronosButtonUtilities.GetButtonFromName("Time");
            switch (button.optionIndex)
            {
                case 0:
                    BetterDayNightManager.instance.SetTimeOfDay(0);
                    break;
                case 1:
                    BetterDayNightManager.instance.SetTimeOfDay(1);
                    break;
                case 2:
                    BetterDayNightManager.instance.SetTimeOfDay(3);
                    break;
                case 3:
                    BetterDayNightManager.instance.SetTimeOfDay(7);
                    break;
            }
        }
    }
}
