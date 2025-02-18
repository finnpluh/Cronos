using Cronos.Menu.Management.Watch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Cronos.Menu.Management.Watch.Cronos;

namespace Cronos.Menu.Mods.Saftey
{
    public class Panic
    {
        public static void Run()
        {
            foreach (Button[] modules in pages)
            {
                foreach (Button module in modules)
                {
                    if (module.isToggleable)
                    {
                        if (module.toggled)
                        {
                            if (module.disableAction != null)
                                module.disableAction();
                            module.toggled = false;
                        }
                    }
                }
            }
        }
    }
}
