using Cronos.Menu.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cronos.Menu.Mods.World
{
    public class ResetSoccerGame
    {
        public static void Run()
        {
            if (CronosRoomUtilities.InRoomWithOthers())
                if (CronosRoomUtilities.IsMasterClient(NetworkSystem.Instance.LocalPlayer))
                    MonkeBallGame.Instance.RequestResetGame();
        }
    }
}
