using Cronos.Menu.Management.Watch;
using Cronos.Menu.Utilities;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Menu.Mods.Room
{
    public class ChangeRegion
    {
        public static void Run()
        {
            Button button = CronosButtonUtilities.GetButtonFromName("Change Region");
            switch (button.optionIndex)
            {
                case 0:
                    PhotonNetwork.ConnectToRegion("us");
                    break;
                case 1:
                    PhotonNetwork.ConnectToRegion("usw");
                    break;
                case 2:
                    PhotonNetwork.ConnectToRegion("eu");
                    break;
            }
        }
    }
}
