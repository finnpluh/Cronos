using Photon.Pun;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Cronos.Menu.Mods.Visual
{
    public class Boards
    {
        private static bool initialize = false;
        private static string controls =
            "<align=center>Controls</align>" +
            "\n-Click <color=grey><BOTH-STICKS></color> to toggle the watch" +
            "\n-Hold <color=grey><LEFT-STICK></color> to control" +
            "\n-Hold <color=grey><RIGHT-STICK></color> to control your current module's mode" +
            "\n-Click <color=grey><RIGHT-TRIGGER></color> or <color=grey><RIGHT-GRIP></color> to switch your current module" +
            "\n-Click <color=grey><LEFT-TRIGGER></color> or <color=grey><LEFT-GRIP></color> to switch your current module's mode" +
            "\n-Click <color=grey><RIGHT-SECONDARY></color> to toggle your current module's tooltip" +
            "\n-Click <color=grey><RIGHT-PRIMARY></color> to toggle your current module" +
            "\n<align=center>Contributors</align>" +
            "\nFinn: <color=grey>Owner</color>, Blaku, Lars, & Misa: <color=grey>Helpers</color>";
        private static string changelog =
            "-Added Spoof Monke Biz, Mod Checker, RPC Bypass, RPC Uncapper, Reach, Graphics, & Wall Walk" +
            "\n-Added a notification when you get master client" +
            "\n-Changed watch interface to fit more text" +
            "\n-Changed the stock theme to the Discord server colors" +
            "\n-Notifications now pivot to the bottom left & have titles" +
            "\n-Moved Cronos Sense to category Modders" +
            "\n-Renamed \"Log Color Gun\" to \"Log Player Gun\" and added modes (ID, Color, Items)";
        private static string previous = string.Empty;
        private static string motd = string.Empty;
        private static List<GameObject> gameobjects = new List<GameObject>();
        private static Material material = new Material(Shader.Find("GorillaTag/UberShader"));
        private static List<Material> materials = new List<Material>();
        private static List<Color> colors = new List<Color>();

        public static void Change()
        {
            if (!initialize)
            {
                foreach (GameObject gameobject in gameobjects)
                {
                    Renderer renderer = gameobject.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        materials.Add(renderer.material);
                        colors.Add(renderer.material.color);
                    }
                }

                int count = 0;
                foreach (Renderer renderers in GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom").transform.GetComponentsInChildren<Renderer>())
                {
                    if (renderers.gameObject.name.Contains("UnityTempFile"))
                    {
                        count++;
                        if (count == 3)
                        {
                            if (!gameobjects.Contains(renderers.gameObject))
                            {
                                gameobjects.Add(renderers.gameObject);
                                if (!materials.Contains(renderers.material))
                                {
                                    materials.Add(renderers.material);
                                    colors.Add(renderers.material.color);
                                }
                            }
                        }
                    }
                }

                TextMeshPro coc = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/COC Text").GetComponent<TextMeshPro>();
                TextMeshPro motd_text = GameObject.Find("motdtext").GetComponent<TextMeshPro>();

                GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/CodeOfConduct").GetComponent<TextMeshPro>().text = $"Cronos <color=grey>(v{PluginInfo.Version})</color>";
                GameObject.Find("motd (1)").GetComponent<TextMeshPro>().text = "Cronos changelogs & announcements";

                if (previous == string.Empty)
                    previous = coc.text;

                if (motd == string.Empty)
                    motd = motd_text.text;

                coc.text = controls;
                coc.richText = true;

                motd_text.text = changelog;

                initialize = true;
            }
            else
            {
                foreach (GameObject gameobject in gameobjects)
                {
                    Renderer renderer = gameobject.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderer.material = material;
                        renderer.material.color = Cronos.Menu.Management.Watch.Cronos.theme;
                    }
                }
            }
        }

        public static void Cleanup()
        {
            if (initialize)
            {
                for (int i = 0; i < gameobjects.Count; i++)
                {
                    Renderer renderer = gameobjects[i].GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderer.material = materials[i];
                        renderer.material.color = colors[i];
                    }
                }

                GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/CodeOfConduct").GetComponent<TextMeshPro>().text = "GORILLA CODE OF CONDUCT";
                GameObject.Find("motd (1)").GetComponent<TextMeshPro>().text = "MESSAGE OF THE DAY";

                TextMeshPro coc = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/COC Text").GetComponent<TextMeshPro>();
                coc.GetComponent<TextMeshPro>().text = previous;
                coc.GetComponent<TextMeshPro>().richText = false;

                GameObject.Find("motdtext").GetComponent<TextMeshPro>().text = motd;

                initialize = false;
            }
        }
    }
}