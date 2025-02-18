using BepInEx;
using Cronos.Menu.Libraries;
using Cronos.Menu.Management.Watch;
using Cronos.Menu.Utilities;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace Cronos.Menu.Mods.Visual
{
    public class StatisticsHUD
    {
        private static GameObject parent = null;
        private static TextMeshPro text = null;
        private static string direction = string.Empty;

        public static void Run()
        {
            if (parent == null)
                Interfaces.Create("Statistics HUD - Cronos", ref parent, ref text, TextAlignmentOptions.BottomRight);
            else
            {
                if (!parent.activeSelf)
                    parent.SetActive(true);
                else
                {
                    if (Vector3.Dot(GorillaTagger.Instance.bodyCollider.transform.forward, Vector3.forward) > 0.7f)
                        direction = "N";
                    else if (Vector3.Dot(GorillaTagger.Instance.bodyCollider.transform.forward, Vector3.back) > 0.7f)
                        direction = "S";
                    else if (Vector3.Dot(GorillaTagger.Instance.bodyCollider.transform.forward, Vector3.right) > 0.7f)
                        direction = "E";
                    else if (Vector3.Dot(GorillaTagger.Instance.bodyCollider.transform.forward, Vector3.left) > 0.7f)
                        direction = "W";

                    string statistics = $"{Mathf.RoundToInt(1f / Time.deltaTime)} <color={CronosColorUtilities.Color32ToHTML(Cronos.Menu.Management.Watch.Cronos.theme)}>fps</color>" +
                        $"\n{(PhotonNetwork.GetPing() == 0 ? "N/A" : PhotonNetwork.GetPing().ToString())} <color={CronosColorUtilities.Color32ToHTML(Cronos.Menu.Management.Watch.Cronos.theme)}>ms</color>" +
                        $"\n{direction} <color={CronosColorUtilities.Color32ToHTML(Cronos.Menu.Management.Watch.Cronos.theme)}>facing</color>" +
                        $"\n{(string.IsNullOrEmpty(PhotonNetwork.CloudRegion) ? "N/A" : PhotonNetwork.CloudRegion)} <color={CronosColorUtilities.Color32ToHTML(Cronos.Menu.Management.Watch.Cronos.theme)}>region</color>" +
                        $"\n{(PhotonNetwork.InRoom ? PhotonNetwork.CurrentRoom.PlayerCount.ToString() : "N/A")} <color={CronosColorUtilities.Color32ToHTML(Cronos.Menu.Management.Watch.Cronos.theme)}>player(s)</color>";

                    if (text.text != statistics)
                        text.text = statistics;

                    if (text.renderer.material.shader != Shader.Find("GUI/Text Shader"))
                        text.renderer.material.shader = Shader.Find("GUI/Text Shader");

                    if (Cronos.Menu.Management.Watch.Preferences.preferences[1])
                    {
                        if (parent.layer != 19)
                            parent.layer = 19;
                    }
                    else
                        if (parent.layer != 0)
                            parent.layer = 0;

                    if (Cronos.Menu.Management.Watch.Preferences.preferences[1])
                    {
                        if (parent.layer != 19)
                            parent.layer = 19;
                    }
                    else
                        if (parent.layer != 0)
                            parent.layer = 0;

                    parent.transform.position = GorillaTagger.Instance.headCollider.transform.position + GorillaTagger.Instance.headCollider.transform.forward * 2.75f;
                    parent.transform.rotation = GorillaTagger.Instance.headCollider.transform.rotation;
                }
            }
        }

        public static void Cleanup()
        {
            if (parent != null)
                if (parent.activeSelf)
                    parent.SetActive(false);
        }
    }
}
