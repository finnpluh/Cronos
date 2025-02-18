using Cronos.Menu.Utilities;
using GorillaGameModes;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Cronos.Menu.Mods.Visual
{
    public class DistanceText
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
                            if (!cache.TryGetValue(vrrig.Creator.ActorNumber, out GameObject parent))
                            {
                                parent = new GameObject();

                                TextMeshPro indicator = parent.AddComponent<TextMeshPro>();
                                TextMeshPro nametag = GorillaTagger.Instance.offlineVRRig.playerText1;

                                indicator.font = nametag.font;
                                indicator.characterSpacing = nametag.characterSpacing;
                                indicator.alignment = TextAlignmentOptions.Center;
                                indicator.renderer.material.shader = Shader.Find("GUI/Text Shader");

                                cache[vrrig.Creator.ActorNumber] = parent;
                            }

                            TextMeshPro text = parent.GetComponent<TextMeshPro>();
                            float distance = Vector3.Distance(vrrig.head.rigTarget.transform.position, GorillaLocomotion.Player.Instance.headCollider.transform.position);
                            float opacity = 0.2f;

                            if (GorillaGameManager.instance.GameType() == GameModeType.Infection)
                            {
                                Color infected = Color.red;
                                if (infected.a != opacity)
                                    infected.a = opacity;

                                Color normal = Cronos.Menu.Management.Watch.Preferences.preferences[3] ? Cronos.Menu.Management.Watch.Cronos.theme : Color.green;
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
                                Color skin = Cronos.Menu.Management.Watch.Preferences.preferences[3] ? Cronos.Menu.Management.Watch.Cronos.theme : vrrig.playerColor;
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
                                Color alive = Cronos.Menu.Management.Watch.Preferences.preferences[3] ? Cronos.Menu.Management.Watch.Cronos.theme : Color.green;
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

                            text.text = $"{Mathf.RoundToInt(distance)}M";
                            text.fontSize = distance / 6f + vrrig.scaleFactor * 2;

                            parent.transform.position = vrrig.bodyTransform.transform.position + vrrig.bodyTransform.transform.right * (Vector3.Distance(vrrig.bodyTransform.transform.position, GorillaLocomotion.Player.Instance.headCollider.transform.position) / 20f + 0.5f * vrrig.scaleFactor);
                            parent.transform.LookAt(Camera.main.transform);
                            parent.transform.Rotate(0f, 180f, 0f);

                            if (Cronos.Menu.Management.Watch.Preferences.preferences[1])
                            {
                                if (parent.layer != 19)
                                    parent.layer = 19;
                            }
                            else
                                if (parent.layer != 0)
                                    parent.layer = 0;
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
