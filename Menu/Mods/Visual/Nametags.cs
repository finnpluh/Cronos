using Cronos.Menu.Libraries;
using Cronos.Menu.Management.Watch;
using Cronos.Menu.Utilities;
using GorillaGameModes;
using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.UIElements;
using static Drawing.Palette.Colorbrewer;

namespace Cronos.Menu.Mods.Visual
{
    public class Nametags
    {
        private static Dictionary<int, GameObject> cache = new Dictionary<int, GameObject>();

        public static void Run()
        {
            if (CronosRoomUtilities.InRoomWithOthers())
            {
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    if (!vrrig.isMyPlayer && !vrrig.isOfflineVRRig)
                    {
                        if (vrrig.colorInitialized)
                        {
                            VRRigManager.FixVRRigColors(vrrig);
                            if (!cache.TryGetValue(vrrig.Creator.ActorNumber, out GameObject nametag))
                            {
                                nametag = new GameObject();
                                TextMeshPro indicator = nametag.AddComponent<TextMeshPro>();
                                indicator.font = Cronos.Menu.Management.Watch.Settings.font;
                                indicator.alignment = TextAlignmentOptions.Center;
                                indicator.renderer.material.shader = Shader.Find("GUI/Text Shader");
                                cache[vrrig.Creator.ActorNumber] = nametag;
                            }

                            TextMeshPro text = nametag.GetComponent<TextMeshPro>();
                            float distance = Vector3.Distance(vrrig.head.rigTarget.transform.position, GorillaLocomotion.Player.Instance.headCollider.transform.position);
                            float opacity = distance < 12f ? 0.75f : 0.2f;
                            string display = vrrig.Creator.NickName + $"\n<size={text.fontSize / 2}><color=grey>({Mathf.RoundToInt(distance)}M) </color><color=white>{Mathf.Floor(vrrig.playerColor.r * 9f)}, {Mathf.Floor(vrrig.playerColor.g * 9f)}, {Mathf.Floor(vrrig.playerColor.b * 9f)}</color>";

                            if (text.text != display)
                                text.text = display;

                            text.fontSize = Vector3.Distance(vrrig.head.rigTarget.transform.position, GorillaLocomotion.Player.Instance.headCollider.transform.position) / 4.5f + vrrig.scaleFactor * 2;

                            if (GorillaGameManager.instance.GameType() == GameModeType.Infection)
                            {
                                Color infected = Color.red;
                                if (infected.a != opacity)
                                    infected.a = opacity;

                                Color normal = Cronos.Menu.Management.Watch.Settings.follow_theme ? Cronos.Menu.Management.Watch.Cronos.theme : Color.green;
                                if (normal.a != opacity)
                                    normal.a = opacity;

                                if (VRRigManager.VRRigIsTagged(vrrig))
                                {
                                    if (text.color != infected)
                                        text.color = infected;
                                }
                                else
                                {
                                    if (text.color != normal)
                                        text.color = normal;
                                }
                            }
                            else if (GorillaGameManager.instance.GameType() == GameModeType.Casual || GorillaGameManager.instance.GameType() == GameModeType.Guardian)
                            {
                                Color skin = Cronos.Menu.Management.Watch.Settings.follow_theme ? Cronos.Menu.Management.Watch.Cronos.theme : vrrig.playerColor;
                                if (skin.a != opacity)
                                    skin.a = opacity;

                                if (text.color != skin)
                                    text.color = skin;
                            }
                            else if (GorillaGameManager.instance.GameType() == GameModeType.Paintbrawl)
                            {
                                float divisor = 1.75f;

                                Color blue = Color.blue;
                                if (blue.a != opacity)
                                    blue.a = opacity;

                                Color orange = CronosColorUtilities.HTMLToColor32("#ff9700");
                                if (orange.a != opacity)
                                    orange.a = opacity;

                                if (vrrig.mainSkin.material.name.Contains("bluealive") || vrrig.mainSkin.material.name.Contains("bluehit") || vrrig.mainSkin.material.name.Contains("bluestunned"))
                                {
                                    if (text.color != blue)
                                        text.color = blue;
                                }
                                else if (vrrig.mainSkin.material.name.Contains("orangealive") || vrrig.mainSkin.material.name.Contains("orangehit") || vrrig.mainSkin.material.name.Contains("orangestanned"))
                                {
                                    if (text.color != orange)
                                        text.color = orange;
                                }
                                else if (vrrig.mainSkin.material.name.Contains("paintsplattersmallblue"))
                                {
                                    if (text.color != new Color(blue.r / divisor, blue.g / divisor, blue.b / divisor, blue.a))
                                        text.color = new Color(blue.r / divisor, blue.g / divisor, blue.b / divisor, blue.a);
                                }
                                else if (vrrig.mainSkin.material.name.Contains("paintsplattersmallorange"))
                                {
                                    if (text.color != new Color(orange.r / divisor, orange.g / divisor, orange.b / divisor, orange.a))
                                        text.color = new Color(orange.r / divisor, orange.g / divisor, orange.b / divisor, orange.a);
                                }
                            }
                            else if (GorillaGameManager.instance.GameType() == GameModeType.FreezeTag)
                            {
                                Color alive = Cronos.Menu.Management.Watch.Settings.follow_theme ? Cronos.Menu.Management.Watch.Cronos.theme : Color.green;
                                if (alive.a != opacity)
                                    alive.a = opacity;

                                Color frozen = Color.cyan;
                                if (frozen.a != opacity)
                                    frozen.a = opacity;

                                if (vrrig.mainSkin.material.name.Contains("Ice_Body"))
                                {
                                    if (text.color != frozen)
                                        text.color = frozen;
                                }
                                else
                                {
                                    if (text.color != alive)
                                        text.color = alive;
                                }
                            }

                            nametag.transform.position = vrrig.head.rigTarget.transform.position + new Vector3(0f, Vector3.Distance(vrrig.head.rigTarget.transform.position, GorillaLocomotion.Player.Instance.headCollider.transform.position) / 20f + 0.5f * vrrig.scaleFactor, 0f);
                            nametag.transform.LookAt(Camera.main.transform);
                            nametag.transform.Rotate(0f, 180f, 0f);

                            if (Cronos.Menu.Management.Watch.Settings.ghost_mode)
                            {
                                if (nametag.layer != 19)
                                    nametag.layer = 19;
                            }
                            else
                                if (nametag.layer != 0)
                                nametag.layer = 0;
                        }
                    }
                }

                foreach (var actor in new List<int>(cache.Keys))
                {
                    if (!new HashSet<int>(PhotonNetwork.CurrentRoom.Players.Keys).Contains(actor))
                    {
                        GameObject.Destroy(cache[actor]);
                        cache.Remove(actor);
                    }
                }
            }
            else
                Cleanup();
        }

        public static void Cleanup()
        {
            foreach (var nametag in cache.Values)
                GameObject.Destroy(nametag);
            cache.Clear();
        }
    }
}