using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Menu.Mods.Saftey
{
    public class RPCUncapper
    {
        public static void Run()
        {
            if (GorillaNot.instance != null)
            {
                if (GorillaNot.instance.rpcCallLimit != int.MaxValue)
                    GorillaNot.instance.rpcCallLimit = int.MaxValue;

                if (GorillaNot.instance.rpcErrorMax != int.MaxValue)
                    GorillaNot.instance.rpcErrorMax = int.MaxValue;
            }
        }
    }
}