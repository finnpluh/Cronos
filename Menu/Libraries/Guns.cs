using Cronos.Menu.Utilities;
using System;
using UnityEngine;

namespace Cronos.Menu.Libraries
{
    public class Guns
    {
        public RaycastHit raycast;
        public GameObject pointer = null;
        public LineRenderer line = null;
        public bool cooldown = false;

        public void Create(Action action, Action cleanup = null, bool once = true)
        {
            if (ControllerInput.rightGrip())
            {
                Physics.Raycast(GorillaLocomotion.Player.Instance.rightControllerTransform.position, -GorillaLocomotion.Player.Instance.rightControllerTransform.up, out raycast);
                if (pointer == null)
                {
                    pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    pointer.transform.position = raycast.point;
                    pointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f) * GorillaLocomotion.Player.Instance.scale;
                    pointer.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                    pointer.GetComponent<Renderer>().material.color = Cronos.Menu.Management.Watch.Cronos.theme;
                    GameObject.Destroy(pointer.GetComponent<Collider>());
                }
                else
                {
                    pointer.transform.position = raycast.point;

                    if (pointer.activeSelf)
                    {
                        if (line == null)
                            line = pointer.AddComponent<LineRenderer>();
                        else
                        {
                            if (line.startWidth != 0.015f * GorillaLocomotion.Player.Instance.scale)
                                line.startWidth = 0.015f * GorillaLocomotion.Player.Instance.scale;

                            if (line.endWidth != 0.015f * GorillaLocomotion.Player.Instance.scale)
                                line.endWidth = 0.015f * GorillaLocomotion.Player.Instance.scale;

                            if (line.positionCount != 2)
                                line.positionCount = 2;

                            if (!line.useWorldSpace)
                                line.useWorldSpace = true;

                            line.SetPosition(0, GorillaLocomotion.Player.Instance.rightControllerTransform.transform.position);
                            line.SetPosition(1, pointer.transform.position);

                            if (line.material.color != Cronos.Menu.Management.Watch.Cronos.theme)
                                line.material.color = Cronos.Menu.Management.Watch.Cronos.theme;

                            if (line.material.shader != Shader.Find("GorillaTag/UberShader"))
                                line.material.shader = Shader.Find("GorillaTag/UberShader");
                        }

                        if (ControllerInput.rightTrigger())
                        {
                            if (once)
                            {
                                if (!cooldown)
                                {
                                    action();
                                    GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(66, false, Cronos.Menu.Management.Watch.Settings.volume);
                                    cooldown = true;
                                }
                            }
                            else
                            {
                                if (!cooldown)
                                {
                                    GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(66, false, Cronos.Menu.Management.Watch.Settings.volume);
                                    cooldown = true;
                                }
                                action();
                            }
                            pointer.GetComponent<Renderer>().material.color = new Color(Cronos.Menu.Management.Watch.Cronos.theme.r / 2, Cronos.Menu.Management.Watch.Cronos.theme.g / 2, Cronos.Menu.Management.Watch.Cronos.theme.b / 2);
                        }
                        else
                        {
                            if (cooldown)
                            {
                                if (cleanup != null)
                                    cleanup();
                                cooldown = false;
                            }
                            pointer.GetComponent<Renderer>().material.color = Cronos.Menu.Management.Watch.Cronos.theme;
                        }
                    }
                    else
                    {
                        if (!pointer.activeSelf)
                        {
                            pointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f) * GorillaLocomotion.Player.Instance.scale;
                            pointer.SetActive(true);
                        }
                    }
                }
            }
            else
                Cleanup();
        }

        public void Cleanup()
        {
            if (pointer != null)
                if (pointer.activeSelf)
                    pointer.SetActive(false);
        }
    }
}
