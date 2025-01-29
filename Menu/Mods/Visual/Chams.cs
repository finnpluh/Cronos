using Cronos.Menu.Utilities;
using GorillaGameModes;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cronos.Menu.Mods.Visual
{
    public class Chams
    {
        public static void Run()
        {
            if (CronosRoomUtilities.InRoomWithOthers())
            {
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    if (!vrrig.isOfflineVRRig && !vrrig.isMyPlayer)
                    {
                        if (vrrig.colorInitialized)
                        {
                            float opactity = 0.5f;
                            Shader shader = Shader.Find("GUI/Text Shader");
                            Renderer renderer = vrrig.mainSkin;

                            if (renderer.material.shader != shader)
                                renderer.material.shader = shader;

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
                                Color skin = Cronos.Menu.Management.Watch.Settings.follow_theme? Cronos.Menu.Management.Watch.Cronos.theme : vrrig.playerColor;
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

                                if (renderer.material.name.Contains("bluealive") || renderer.material.name.Contains("bluehit") || renderer.material.name.Contains("bluestunned"))
                                {
                                    if (renderer.material.color != blue)
                                        renderer.material.color = blue;
                                }
                                else if (renderer.material.name.Contains("orangealive") || renderer.material.name.Contains("orangehit") || renderer.material.name.Contains("orangestanned"))
                                {
                                    if (renderer.material.color != orange)
                                        renderer.material.color = orange;
                                }
                                else if (renderer.material.name.Contains("paintsplattersmallblue"))
                                {
                                    if (renderer.material.color != new Color(blue.r / divisor, blue.g / divisor, blue.b / divisor, blue.a))
                                        renderer.material.color = new Color(blue.r / divisor, blue.g / divisor, blue.b / divisor, blue.a);
                                }
                                else if (renderer.material.name.Contains("paintsplattersmallorange"))
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

                                if (renderer.material.name.Contains("Ice_Body"))
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
                        }
                    }
                }
            }
        }

        public static void Cleanup()
        {
            if (PhotonNetwork.InRoom)
                if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
                    foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                        if (!vrrig.isOfflineVRRig && !vrrig.isMyPlayer)
                            vrrig.mainSkin.material.shader = Shader.Find("GorillaTag/UberShader");
        }
    }
}
