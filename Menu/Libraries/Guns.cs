using Cronos.Menu.Utilities;
using GorillaLocomotion.Gameplay;
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
        private static Color color = Color.red;

        public void Create(Action action, Action cleanup = null, bool once = true)
        {
            if (ControllerInput.rightGrip())
            {
                Physics.Raycast(GorillaLocomotion.Player.Instance.rightControllerTransform.position, -GorillaLocomotion.Player.Instance.rightControllerTransform.up, out raycast);
                if (pointer == null)
                {
                    pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    pointer.transform.position = raycast.point;
                    pointer.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f) * GorillaLocomotion.Player.Instance.scale;
                    pointer.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                    pointer.GetComponent<Renderer>().material.color = color;
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

                            if (line.material.color != color)
                                line.material.color = color;

                            if (pointer.GetComponent<Renderer>().material.color != color)
                                pointer.GetComponent<Renderer>().material.color = color;

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
                                    GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(66, false, Cronos.Menu.Management.Watch.Preferences.volume);
                                    cooldown = true;
                                }
                            }
                            else
                            {
                                if (!cooldown)
                                {
                                    GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(66, false, Cronos.Menu.Management.Watch.Preferences.volume);
                                    cooldown = true;
                                }
                                action();
                            }

                            if (raycast.collider.GetComponentInParent<VRRig>() || raycast.collider.GetComponentInParent<GliderHoldable>() || raycast.collider.GetComponentInParent<ThrowableBug>() || raycast.collider.GetComponentInParent<MonkeyeAI>() || raycast.collider.GetComponentInParent<GorillaRopeSwing>())
                                if (color != Color.green)
                                    color = Color.green;
                        }
                        else
                        {
                            if (cooldown)
                            {
                                if (cleanup != null)
                                    cleanup();
                                cooldown = false;
                            }

                            if (raycast.collider.GetComponentInParent<VRRig>() || raycast.collider.GetComponentInParent<GliderHoldable>() || raycast.collider.GetComponentInParent<ThrowableBug>() || raycast.collider.GetComponentInParent<MonkeyeAI>() || raycast.collider.GetComponentInParent<GorillaRopeSwing>())
                            {
                                if (color != Color.blue)
                                    color = Color.blue;
                            }
                            else
                                if (color != Color.red)
                                    color = Color.red;
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
