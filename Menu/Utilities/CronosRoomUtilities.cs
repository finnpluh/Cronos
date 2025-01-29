using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Menu.Utilities
{
    public class CronosRoomUtilities
    {
        public static bool InRoomWithOthers()
        {
            if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount > 1)
                return true;
            return false;
        }
    }
}
