using Cronos.Menu.Management.Watch;
using Cronos.Menu.Utilities;
using GorillaGameModes;
using GorillaTagScripts;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Cronos.Menu.Mods.Visual
{
    public class ESP
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
                            if (!cache.TryGetValue(vrrig.Creator.ActorNumber, out GameObject box))
                            {
                                box = GameObject.CreatePrimitive(PrimitiveType.Cube);
                                box.transform.localScale = new Vector3(1f, 1f, 0f);
                                box.GetComponent<Renderer>().material = new Material(Shader.Find("GUI/Text Shader"));

                                GameObject.Destroy(box.GetComponent<Collider>());
                                cache[vrrig.Creator.ActorNumber] = box;
                            }

                            if (Cronos.Menu.Management.Watch.Settings.ghost_mode)
                            {
                                if (box.layer != 19)
                                    box.layer = 19;
                            }
                            else
                                if (box.layer != 0)
                                    box.layer = 0;

                            float distance = Vector3.Distance(GorillaTagger.Instance.bodyCollider.transform.position, vrrig.bodyTransform.transform.position) / 45;
                            float opactity = 0.2f;
                            Renderer renderer = box.GetComponent<Renderer>();

                            if (GorillaGameManager.instance.GameType() == GameModeType.Infection)
                            {
                                Color infected = Color.red;
                                if (infected.a != opactity)
                                    infected.a = opactity;

                                Color normal = Cronos.Menu.Management.Watch.Settings.follow_theme ? Cronos.Menu.Management.Watch.Cronos.theme : Color.green;
                                if (normal.a != opactity)
                                    normal.a = opactity;

                                if (VRRigManager.VRRigIsTagged(vrrig))
                                {
                                    if (renderer.material.color != infected)
                                        renderer.material.color = infected;
                                }
                                else
                                {
                                    if (renderer.material.color != normal)
                                        renderer.material.color = normal;
                                }
                            }
                            else if (GorillaGameManager.instance.GameType() == GameModeType.Casual || GorillaGameManager.instance.GameType() == GameModeType.Guardian)
                            {
                                Color skin = Cronos.Menu.Management.Watch.Settings.follow_theme ? Cronos.Menu.Management.Watch.Cronos.theme : vrrig.playerColor;
                                if (skin.a != opactity)
                                    skin.a = opactity;

                                if (renderer.material.color != skin)
                                    renderer.material.color = skin;
                            }
                            else if (GorillaGameManager.instance.GameType() == GameModeType.Paintbrawl)
                            {
                                float divisor = 1.75f;

                                Color blue = Color.blue;
                                if (blue.a != opactity)
                                    blue.a = opactity;

                                Color orange = CronosColorUtilities.HTMLToColor32("#ff9700");
                                if (orange.a != opactity)
                                    orange.a = opactity;

                                if (vrrig.mainSkin.material.name.Contains("bluealive") || vrrig.mainSkin.material.name.Contains("bluehit") || vrrig.mainSkin.material.name.Contains("bluestunned"))
                                {
                                    if (renderer.material.color != blue)
                                        renderer.material.color = blue;
                                }
                                else if (vrrig.mainSkin.material.name.Contains("orangealive") || vrrig.mainSkin.material.name.Contains("orangehit") || vrrig.mainSkin.material.name.Contains("orangestanned"))
                                {
                                    if (renderer.material.color != orange)
                                        renderer.material.color = orange;
                                }
                                else if (vrrig.mainSkin.material.name.Contains("paintsplattersmallblue"))
                                {
                                    if (renderer.material.color != new Color(blue.r / divisor, blue.g / divisor, blue.b / divisor, blue.a))
                                        renderer.material.color = new Color(blue.r / divisor, blue.g / divisor, blue.b / divisor, blue.a);
                                }
                                else if (vrrig.mainSkin.material.name.Contains("paintsplattersmallorange"))
                                {
                                    if (renderer.material.color != new Color(orange.r / divisor, orange.g / divisor, orange.b / divisor, orange.a))
                                        renderer.material.color = new Color(orange.r / divisor, orange.g / divisor, orange.b / divisor, orange.a);
                                }
                            }
                            else if (GorillaGameManager.instance.GameType() == GameModeType.FreezeTag)
                            {
                                Color alive = Cronos.Menu.Management.Watch.Settings.follow_theme ? Cronos.Menu.Management.Watch.Cronos.theme : Color.green;
                                if (alive.a != opactity)
                                    alive.a = opactity;

                                Color frozen = Color.cyan;
                                if (frozen.a != opactity)
                                    frozen.a = opactity;

                                if (vrrig.mainSkin.material.name.Contains("Ice_Body"))
                                {
                                    if (renderer.material.color != frozen)
                                        renderer.material.color = frozen;
                                }
                                else
                                {
                                    if (renderer.material.color != alive)
                                        renderer.material.color = alive;
                                }
                            }

                            box.transform.localScale = new Vector3(vrrig.scaleFactor + distance, vrrig.scaleFactor + distance, 0f);
                            box.transform.position = vrrig.head.rigTarget.transform.position;
                            box.transform.LookAt(Camera.main.transform);
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
            foreach (var box in cache.Values)
                GameObject.Destroy(box);
            cache.Clear();
        }
    }
}
