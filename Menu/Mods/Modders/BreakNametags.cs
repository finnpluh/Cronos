using Cronos.Menu.Utilities;
using GorillaNetworking;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Menu.Mods.Modders
{
    public class BreakNametags
    {
        private static string name = string.Empty;

        public static void Run()
        {
            if (name == string.Empty)
                name = PhotonNetwork.LocalPlayer.NickName;
            else
            {
                string nickname = string.Concat(Enumerable.Repeat(name + " ", 100));
                if (PhotonNetwork.LocalPlayer.NickName != nickname)
                    PhotonNetwork.LocalPlayer.NickName = nickname;

                if (GorillaComputer.instance.currentName != nickname)
                    GorillaComputer.instance.currentName = nickname;

                if (GorillaComputer.instance.savedName != nickname)
                    GorillaComputer.instance.savedName = nickname;
            }
        }

        public static void Cleanup()
        {
            if (name != string.Empty)
            {
                if (PhotonNetwork.LocalPlayer.NickName != name)
                    PhotonNetwork.LocalPlayer.NickName = name;

                if (GorillaComputer.instance.currentName != name)
                    GorillaComputer.instance.currentName = name;

                if (GorillaComputer.instance.savedName != name)
                    GorillaComputer.instance.savedName = name;
            }
        }
    }
}
