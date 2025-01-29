using GorillaNetworking;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Menu.Mods.Self
{
    public class AntiAFK
    {
        public static void Run()
        {
            if (!PhotonNetworkController.Instance.disableAFKKick)
                PhotonNetworkController.Instance.disableAFKKick = true;
        }

        public static void Cleanup()
        {
            if (PhotonNetworkController.Instance.disableAFKKick)
                PhotonNetworkController.Instance.disableAFKKick = false;
        }
    }
}
