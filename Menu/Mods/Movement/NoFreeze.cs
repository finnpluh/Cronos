using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Menu.Mods.Movement
{
    public class NoFreeze
    {
        public static void Run()
        {
            if (GorillaLocomotion.Player.Instance.disableMovement)
                GorillaLocomotion.Player.Instance.disableMovement = false;
        }
    }
}
