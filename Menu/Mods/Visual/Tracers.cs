using Cronos.Menu.Management.Watch;
using Cronos.Menu.Utilities;
using GorillaGameModes;
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
    public class Tracers
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
                            if (!cache.TryGetValue(vrrig.Creator.ActorNumber, out GameObject tracer))
                            {
                                tracer = new GameObject();
                                LineRenderer renderer = tracer.AddComponent<LineRenderer>();
                                renderer.useWorldSpace = true;
                                renderer.material.shader = Shader.Find("GUI/Text Shader");
                                cache[vrrig.Creator.ActorNumber] = tracer;
                            }

                            LineRenderer line = tracer.GetComponent<LineRenderer>();
                            line.SetPosition(0, GorillaTagger.Instance.bodyCollider.transform.position);
                            line.SetPosition(1, vrrig.bodyTransform.transform.position);

                            float size = GorillaLocomotion.Player.Instance.scale;
                            Button button = CronosButtonUtilities.GetButtonFromName("Tracers");
                            switch (button.optionIndex)
                            {
                                case 0:
                                    if (line.startWidth != 0.002f * size)
                                        line.startWidth = 0.002f * size;

                                    if (line.endWidth != 0.002f * size)
                                        line.endWidth = 0.002f * size;
                                    break;
                                case 1:
                                    if (line.startWidth != 0.004f * size)
                                        line.startWidth = 0.004f * size;

                                    if (line.endWidth != 0.004f * size)
                                        line.endWidth = 0.004f * size;
                                    break;
                                case 2:
                                    if (line.startWidth != 0.006f * size)
                                        line.startWidth = 0.006f * size;

                                    if (line.endWidth != 0.006f * size)
                                        line.endWidth = 0.006f * size;
                                    break;
                                case 3:
                                    if (line.startWidth != 0.008f * size)
                                        line.startWidth = 0.008f * size;

                                    if (line.endWidth != 0.008f * size)
                                        line.endWidth = 0.008f * size;
                                    break;
                                case 4:
                                    if (line.startWidth != 0.01f * size)
                                        line.startWidth = 0.01f * size;

                                    if (line.endWidth != 0.01f * size)
                                        line.endWidth = 0.01f * size;
                                    break;
                                case 5:
                                    if (line.startWidth != 0.012f * size)
                                        line.startWidth = 0.012f * size;

                                    if (line.endWidth != 0.012f * size)
                                        line.endWidth = 0.012f * size;
                                    break;
                                case 6:
                                    if (line.startWidth != 0.014f * size)
                                        line.startWidth = 0.014f * size;

                                    if (line.endWidth != 0.014f * size)
                                        line.endWidth = 0.014f * size;
                                    break;
                                case 7:
                                    if (line.startWidth != 0.016f * size)
                                        line.startWidth = 0.016f * size;

                                    if (line.endWidth != 0.016f * size)
                                        line.endWidth = 0.016f * size;
                                    break;
                                case 8:
                                    if (line.startWidth != 0.018f * size)
                                        line.startWidth = 0.018f * size;

                                    if (line.endWidth != 0.018f * size)
                                        line.endWidth = 0.018f * size;
                                    break;
                                case 9:
                                    if (line.startWidth != 0.02f * size)
                                        line.startWidth = 0.02f * size;

                                    if (line.endWidth != 0.02f * size)
                                        line.endWidth = 0.02f * size;
                                    break;
                            }


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
                                if (tracer.layer != 19)
                                    tracer.layer = 19;
                            }
                            else
                                if (tracer.layer != 0)
                                tracer.layer = 0;
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
