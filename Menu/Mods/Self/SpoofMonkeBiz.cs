using Cronos.Menu.Management.Watch;
using Cronos.Menu.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cronos.Menu.Mods.Self
{
    public class SpoofMonkeBiz
    {
        public static void Run()
        {
            int amount = 0;
            Button button = CronosButtonUtilities.GetButtonFromName("Spoof Monke Biz");
            switch (button.optionIndex)
            {
                case 0:
                    if (amount != 200)
                        amount = 200;
                    break;
                case 1:
                    if (amount != 400)
                        amount = 400;
                    break;
                case 2:
                    if (amount != 600)
                        amount = 600;
                    break;
                case 3:
                    if (amount != 800)
                        amount = 800;
                    break;
                case 4:
                    if (amount != 1000)
                        amount = 1000;
                    break;
                case 5:
                    if (amount != 5000)
                        amount = 5000;
                    break;
                case 6:
                    if (amount != 7500)
                        amount = 7500;
                    break;
                case 7:
                    if (amount != 10000)
                        amount = 10000;
                    break;
                case 8:
                    if (amount != 99999)
                        amount = 99999;
                    break;
            }

            if (GorillaTagger.Instance.offlineVRRig.GetCurrentQuestScore() != amount)
                GorillaTagger.Instance.offlineVRRig.SetQuestScore(amount);
        }
    }
}
