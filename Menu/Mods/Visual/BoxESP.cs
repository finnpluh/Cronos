using Cronos.Menu.Management.Watch;
using Cronos.Menu.Utilities;
using g3;
using GorillaGameModes;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace Cronos.Menu.Mods.Visual
{
    public class BoxESP
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
                            float opacity = 0.75f;
                            VRRigManager.FixVRRigColors(vrrig);
                            if (!cache.TryGetValue(vrrig.Creator.ActorNumber, out GameObject parent))
                            {
                                parent = new GameObject();
                                LineRenderer renderer = parent.AddComponent<LineRenderer>();

                                renderer.useWorldSpace = true;
                                renderer.loop = true;
                                renderer.positionCount = 5;
                                renderer.startWidth = 0.035f;
                                renderer.endWidth = 0.035f;
                                renderer.material.shader = Shader.Find("GUI/Text Shader");

                                cache[vrrig.Creator.ActorNumber] = parent;
                            }

                            LineRenderer line = parent.GetComponent<LineRenderer>();
                            float distance = vrrig.scaleFactor + Vector3.Distance(GorillaTagger.Instance.bodyCollider.transform.position, vrrig.bodyTransform.transform.position) / 45;

                            Vector3 forward = (vrrig.head.rigTarget.transform.position - Camera.main.transform.position).normalized * (distance / 2);
                            Vector3 right = Vector3.Cross(Vector3.up, forward).normalized * (distance / 2);
                            Vector3 up = Vector3.Cross(forward, right).normalized * (distance / 2);

                            line.SetPosition(0, vrrig.head.rigTarget.transform.position + -right + up);
                            line.SetPosition(1, vrrig.head.rigTarget.transform.position + right + up);
                            line.SetPosition(2, vrrig.head.rigTarget.transform.position + right + -up);
                            line.SetPosition(3, vrrig.head.rigTarget.transform.position + -right + -up);
                            line.SetPosition(4, vrrig.head.rigTarget.transform.position + -right + up);

                            parent.transform.position = vrrig.head.rigTarget.transform.position;
                            parent.transform.LookAt(Camera.main.transform);

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
                                    if (line.startColor != infected)
                                        line.startColor = infected;

                                    if (line.endColor != infected)
                                        line.endColor = infected;
                                }
                                else
                                {
                                    if (line.startColor != normal)
                                        line.startColor = normal;

                                    if (line.endColor != normal)
                                        line.endColor = normal;
                                }
                            }
                            else if (GorillaGameManager.instance.GameType() == GameModeType.Casual || GorillaGameManager.instance.GameType() == GameModeType.Guardian)
                            {
                                Color skin = Cronos.Menu.Management.Watch.Preferences.preferences[3] ? Cronos.Menu.Management.Watch.Cronos.theme : vrrig.playerColor;
                                if (skin.a != opacity)
                                    skin.a = opacity;

                                if (line.startColor != skin)
                                    line.startColor = skin;

                                if (line.endColor != skin)
                                    line.endColor = skin;
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
                                    if (line.startColor != blue)
                                        line.startColor = blue;

                                    if (line.endColor != blue)
                                        line.endColor = blue;
                                }
                                else if (vrrig.mainSkin.material.name.Contains("orangealive") || vrrig.mainSkin.material.name.Contains("orangehit") || vrrig.mainSkin.material.name.Contains("orangestanned"))
                                {
                                    if (line.startColor != orange)
                                        line.startColor = orange;

                                    if (line.endColor != orange)
                                        line.endColor = orange;
                                }
                                else if (vrrig.mainSkin.material.name.Contains("paintsplattersmallblue"))
                                {
                                    if (line.startColor != new Color(blue.r / divisor, blue.g / divisor, blue.b / divisor, blue.a))
                                        line.startColor = new Color(blue.r / divisor, blue.g / divisor, blue.b / divisor, blue.a);

                                    if (line.endColor != new Color(blue.r / divisor, blue.g / divisor, blue.b / divisor, blue.a))
                                        line.endColor = new Color(blue.r / divisor, blue.g / divisor, blue.b / divisor, blue.a);
                                }
                                else if (vrrig.mainSkin.material.name.Contains("paintsplattersmallorange"))
                                {
                                    if (line.startColor != new Color(orange.r / divisor, orange.g / divisor, orange.b / divisor, orange.a))
                                        line.startColor = new Color(orange.r / divisor, orange.g / divisor, orange.b / divisor, orange.a);

                                    if (line.endColor != new Color(orange.r / divisor, orange.g / divisor, orange.b / divisor, orange.a))
                                        line.endColor = new Color(orange.r / divisor, orange.g / divisor, orange.b / divisor, orange.a);
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
                                    if (line.startColor != frozen)
                                        line.startColor = frozen;

                                    if (line.endColor != frozen)
                                        line.endColor = frozen;
                                }
                                else
                                {
                                    if (line.startColor != alive)
                                        line.startColor = alive;

                                    if (line.endColor != alive)
                                        line.endColor = alive;
                                }
                            }

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
            foreach (var tracer in cache.Values)
                GameObject.Destroy(tracer);
            cache.Clear();
        }
    }
}
