using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cronos.Menu.Mods.Visual
{
    public class RainbowSky
    {
        private static Shader shader = null;

        public static void Run()
        {
            GameObject sky = GameObject.Find("Environment Objects/LocalObjects_Prefab/Standard Sky");
            Renderer renderer = sky.GetComponent<Renderer>();
            if (renderer != null)
            {
                if (shader == null)
                    shader = renderer.material.shader;
                else
                {
                    if (renderer.material.shader != Shader.Find("GorillaTag/UberShader"))
                        renderer.material.shader = Shader.Find("GorillaTag/UberShader");

                    Color color = Cronos.Menu.Management.Watch.Settings.follow_theme ? Cronos.Menu.Management.Watch.Cronos.theme : Color.HSVToRGB(Mathf.PingPong(Time.time * 0.5f, 1f), 1f, 1f);
                    renderer.material.color = color;
                }
            }
        }

        public static void Cleanup()
        {
            if (shader != null)
            {
                GameObject sky = GameObject.Find("Environment Objects/LocalObjects_Prefab/Standard Sky");
                sky.GetComponent<Renderer>().material.shader = shader;
            }
        }
    }
}
