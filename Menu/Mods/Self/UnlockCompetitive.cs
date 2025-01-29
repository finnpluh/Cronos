using GorillaNetworking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Menu.Mods.Self
{
    public class UnlockCompetitive
    {
        public static void Run()
        {
            if (!GorillaComputer.instance.allowedInCompetitive)
                GorillaComputer.instance.CompQueueUnlockButtonPress();
        }
    }
}
