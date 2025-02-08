using Cronos.Menu.Management.Watch;
using Cronos.Menu.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Menu.Mods.World
{
    public class SetSoccerScore
    {
        public static void Run()
        {
            if (CronosRoomUtilities.InRoomWithOthers())
            {
                if (CronosRoomUtilities.IsMasterClient(NetworkSystem.Instance.LocalPlayer))
                {
                    Button button = CronosButtonUtilities.GetButtonFromName("Set Soccer Score");
                    switch (button.optionIndex)
                    {
                        case 0:
                            MonkeBallGame.Instance.RequestSetScore(1, MonkeBallGame.Instance.team[1].score + 1);
                            break;
                        case 1:
                            MonkeBallGame.Instance.RequestSetScore(0, MonkeBallGame.Instance.team[0].score + 1);
                            break;
                    }
                }
            }
        }
    }
}
