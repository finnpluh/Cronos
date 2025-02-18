using Cronos.Menu.Libraries;
using Cronos.Menu.Management.Watch;
using Cronos.Menu.Utilities;
using Photon.Pun;
using PlayFab;
using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class Notifications
{
    private static GameObject parent = null;
    private static TextMeshPro text = null;

    private static string last_notification = string.Empty;
    private static float cooldown;

    public static void Run()
    {
        if (parent == null)
            Interfaces.Create("Notifications - Cronos", ref parent, ref text, TextAlignmentOptions.BottomLeft);
        else
        {
            if (!parent.activeSelf)
                parent.SetActive(true);
            else
            {
                if (text.renderer.material.shader != Shader.Find("GUI/Text Shader"))
                    text.renderer.material.shader = Shader.Find("GUI/Text Shader");

                parent.transform.position = GorillaTagger.Instance.headCollider.transform.position + GorillaTagger.Instance.headCollider.transform.forward * 2.75f;
                parent.transform.rotation = GorillaTagger.Instance.headCollider.transform.rotation;

                if (Cronos.Menu.Management.Watch.Preferences.preferences[1])
                {
                    if (parent.layer != 19)
                        parent.layer = 19;
                }
                else
                    if (parent.layer != 0)
                        parent.layer = 0;

                if (!string.IsNullOrEmpty(text.text))
                {
                    if (Time.time >= cooldown)
                    {
                        int index = text.text.IndexOf('\n');
                        if (index != -1)
                            text.text = text.text.Substring(index + 1);
                        else
                            text.text = string.Empty;

                        cooldown = Time.time + 1.25f;
                    }
                }
            }
        }
    }

    public static void Send(string title, string notification, Color color)
    {
        if (parent != null)
        {
            if (parent.activeSelf)
            {
                string display = $"<color=grey>[</color><color={CronosColorUtilities.Color32ToHTML(color)}>{title}</color><color=grey>]</color> {notification}\n";
                if (!last_notification.Contains(display))
                {
                    if (!text.text.Contains(display))
                    {
                        text.text += display;
                        last_notification = display;

                        cooldown = Time.time + 1.25f;
                    }
                }
            }
        }
    }

    public static void Cleanup()
    {
        if (parent != null)
            if (parent.activeSelf)
                parent.SetActive(false);
    }
}