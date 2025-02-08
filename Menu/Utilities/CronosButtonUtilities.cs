using Cronos.Menu.Libraries;
using Cronos.Menu.Management.Watch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cronos.Menu.Utilities
{
    public class CronosButtonUtilities
    {
        public static Button GetButtonFromName(string name)
        {
            Button button = null;
            if (button == null)
                foreach (Button[] modules in Cronos.Menu.Management.Watch.Cronos.pages)
                    foreach (Button module in modules)
                        if (module.title == name)
                            button = module;
            return button;
        }
    }
}
