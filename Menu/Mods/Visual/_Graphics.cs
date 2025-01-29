using Cronos.Menu.Management.Watch;
using Cronos.Menu.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cronos.Menu.Mods.Visual
{
    public class _Graphics
    {
        public static void Run()
        {
            Button button = CronosButtonUtilities.GetButtonFromName("Graphics");
            switch (button.optionIndex)
            {
                case 0:
                    if (QualitySettings.globalTextureMipmapLimit != 1)
                        QualitySettings.globalTextureMipmapLimit = 1;
                    break;
                case 1:
                    if (QualitySettings.globalTextureMipmapLimit != 2)
                        QualitySettings.globalTextureMipmapLimit = 2;
                    break;
                case 2:
                    if (QualitySettings.globalTextureMipmapLimit != 3)
                        QualitySettings.globalTextureMipmapLimit = 3;
                    break;
                case 3:
                    if (QualitySettings.globalTextureMipmapLimit != 4)
                        QualitySettings.globalTextureMipmapLimit = 4;
                    break;
            }
        }

        public static void Cleanup()
        {
            if (QualitySettings.globalTextureMipmapLimit != 0)
                QualitySettings.globalTextureMipmapLimit = 0;
        }
    }
}
