using Cronos.Menu.Libraries;
using Cronos.Menu.Management.Watch;
using Cronos.Menu.Mods.Settings;
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

namespace Cronos.Menu.Mods.Player
{
    public class NearDistance
    {
        private static GameObject parent = null;
        private static TextMeshPro indicator = null;

        public static void Run()
        {
            if (CronosRoomUtilities.InRoomWithOthers())
            {
                if (!GorillaTagger.Instance.offlineVRRig.mainSkin.material.name.ToLower().Contains("fected") && !GorillaTagger.Instance.offlineVRRig.mainSkin.material.name.Contains("Ice_Body"))
                {
                    if (parent == null)
                    {
                        parent = new GameObject("Near Distance - Cronos");
                        indicator = parent.AddComponent<TextMeshPro>();

                        TextMeshPro nametag = GorillaTagger.Instance.offlineVRRig.playerText1;

                        indicator.font = nametag.font;
                        indicator.characterSpacing = nametag.characterSpacing;
                        indicator.alignment = TextAlignmentOptions.Center;
                        indicator.lineSpacing = 25f;
                        indicator.fontSize = 1f;
                    }
                    else
                    {
                        if (CronosButtonUtilities.GetButtonFromName("Full Ghost Mode").toggled)
                        {
                            if (parent.layer != 19)
                                parent.layer = 19;
                        }
                        else
                            if (parent.layer != 0)
                                parent.layer = 0;

                        if (!parent.activeSelf)
                            parent.SetActive(true);
                        else
                        {
                            Vector3 scale = new Vector3(1f, 1f, 1f) * GorillaLocomotion.Player.Instance.scale;
                            if (parent.gameObject.transform.localScale != scale)
                                parent.gameObject.transform.localScale = scale;

                            parent.transform.position = (CronosButtonUtilities.GetButtonFromName("Near Distance").optionIndex == 0 ? GorillaTagger.Instance.leftHandTransform.transform.position : GorillaTagger.Instance.rightHandTransform.transform.position) + new Vector3(0f, 0.15f * GorillaLocomotion.Player.Instance.scale, 0f);
                            parent.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
                            parent.transform.Rotate(0f, 180f, 0f);
                        }
                    }

                    VRRig closest = null;
                    float value = float.MaxValue;
                    foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                    {
                        if (!vrrig.isOfflineVRRig && !vrrig.isMyPlayer)
                        {
                            bool hunter = (GorillaGameManager.instance.GameType() == GorillaGameModes.GameModeType.Infection && VRRigManager.VRRigIsTagged(vrrig)) || (GorillaGameManager.instance.GameType() == GorillaGameModes.GameModeType.FreezeTag && vrrig.mainSkin.material.name.Contains("Ice_Body"));
                            if (hunter)
                            {
                                float distance = Vector3.Distance(vrrig.bodyTransform.transform.position, GorillaTagger.Instance.bodyCollider.transform.position);
                                if (distance < value)
                                {
                                    value = distance;
                                    closest = vrrig;
                                }
                            }
                        }
                    }

                    if (closest != null)
                    {
                        string text = string.Empty;
                        if (value > 20f)
                            text = $"<color=green>Safe\n{Mathf.RoundToInt(value)}M</color>";
                        else if (value > 15f)
                            text = $"<color=blue>Nearby\n{Mathf.RoundToInt(value)}M</color>";
                        else if (value > 10f)
                            text = $"<color=yellow>Close\n{Mathf.RoundToInt(value)}M</color>";
                        else if (value > 6f)
                            text = $"<color=orange>Near\n{Mathf.RoundToInt(value)}M</color>";
                        else
                            text = $"<color=red>Very Near\n{Mathf.RoundToInt(value)}M</color>";
                        indicator.text = text;
                    }
                    else
                        if (parent.activeSelf)
                            parent.SetActive(false);
                }
                else
                    Cleanup();
            }
            else
                Cleanup();
        }

        public static void Cleanup()
        {
            if (parent != null)
                if (parent.activeSelf)
                    parent.SetActive(false);
        }
    }
}