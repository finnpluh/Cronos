using Cronos.Menu.Libraries;
using Cronos.Menu.Management.Watch;
using Cronos.Menu.Utilities;
using HarmonyLib;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cronos.Menu.Patches.GorillaNotPatches
{
    [HarmonyPatch(typeof(GorillaNot), "SendReport")]
    public class SendReportPatch : MonoBehaviour
    {
        private static void Prefix(string susReason, string susId, string susNick)
        {
            if (susId == PhotonNetwork.LocalPlayer.UserId)
            {
                Button button = CronosButtonUtilities.GetButtonFromName("RPC Bypass");
                if (button.toggled)
                {
                    if (susId != null)
                        susId = null;

                    if (susNick != null)
                        susNick = null;

                    if (susReason != null)
                        susReason = null;

                    Notifications.Send("<color=yellow>Report</color>", $"Anti Cheat attempted to report you");
                }
                else
                    Notifications.Send("<color=yellow>Report</color>", $"Reported for {susReason}");
            }
            else
                Notifications.Send("<color=yellow>Report</color>", $"{susNick} was reported for: {susReason}");
        }
    }
}
