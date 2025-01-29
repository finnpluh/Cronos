using System;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Cronos.Menu.Management.Watch;
using Valve.VR.InteractionSystem;
using GorillaNetworking;
using UnityEngine.UIElements;

namespace Cronos.Menu.Libraries
{
    public class Notifications : MonoBehaviour
    {
        public static GameObject parent = null;
        public static TextMeshPro text = null;
        int decay_time = 200;
        int decay_time_counter = 200;
        string[] lines;
        string next;
        public static string previous;
        public static int repeated = -1;
        public static bool toggled = true;
        public static float delay;

        private void FixedUpdate() //Made by lars, edited by Finn
        {
            if (GorillaTagger.hasInstance)
            {
                if (toggled)
                {
                    if (parent == null)
                    {
                        if (Settings.font == null)
                            Settings.font = GameObject.Find("LabelText").GetComponent<TextMeshPro>().font;

                        parent = new GameObject("Notifications - Cronos");
                        text = parent.AddComponent<TextMeshPro>();

                        RectTransform transform = parent.GetComponent<RectTransform>();
                        transform.sizeDelta = new Vector2(1.75f, 1.75f);

                        text.lineSpacing = 25f;
                        text.font = Settings.font;
                        text.alignment = TextAlignmentOptions.BottomLeft;
                        text.fontSize = 0.5f;
                        text.renderer.material.shader = Shader.Find("GUI/Text Shader");

                        parent.transform.SetParent(GorillaTagger.Instance.headCollider.transform);
                        parent.transform.LookAt(Camera.main.transform);
                        parent.transform.Rotate(0f, 180f, 0f);
                    }
                    else
                    {
                        if (Cronos.Menu.Management.Watch.Settings.ghost_mode)
                        {
                            if (parent.layer != 19)
                                parent.layer = 19;
                        }
                        else
                            if (parent.layer != 0)
                                parent.layer = 0;

                        float scale = 0.5f / GorillaLocomotion.Player.Instance.scale;
                        if (text.fontSize != scale)
                            text.fontSize = scale;

                        parent.transform.position = GorillaTagger.Instance.headCollider.transform.position + GorillaTagger.Instance.headCollider.transform.forward * 2.5f;
                        parent.transform.rotation = GorillaTagger.Instance.headCollider.transform.rotation;
                    }
                }

                if (text.text != string.Empty)
                {
                    decay_time_counter++;
                    if (decay_time_counter > decay_time)
                    {
                        lines = null;
                        next = string.Empty;
                        decay_time_counter = 0;
                        lines = text.text.Split(Environment.NewLine.ToCharArray()).Skip(2).ToArray();

                        foreach (string line in lines)
                            if (line != string.Empty)
                                next = next + line + "\n";

                        text.text = next;
                    }
                }
                else
                    decay_time_counter = 0;
            }
        }

        public static void Clear() => text.text = string.Empty;

        public static void Send(string title, string notification)
        {
            if (Time.time >= delay)
            {
                delay = Time.time + 0.05f;
                if (toggled)
                {
                    string display = $"<size=0.7>{title}</size>\n{notification}";
                    if (previous == display)
                    {
                        repeated++;
                        string[] lines = text.text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                        lines[lines.Length - 2] = $"<size=0.7>{title} <color=grey>({repeated})</color></size>\n{notification}";
                        text.text = string.Join(Environment.NewLine, lines);
                    }
                    else
                    {
                        previous = display;
                        repeated = 1;
                        text.text += display + Environment.NewLine;
                    }
                }
            }
        }

        public static void Toggle(bool toggle)
        {
            toggled = toggle;
            if (!toggle)
                Clear();
        }
    }
}