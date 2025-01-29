using Cronos.Menu.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cronos.Menu.Mods.Movement
{
    public class Platforms
    {
        private static GameObject left = null;
        private static GameObject right = null;

        public static void Run()
        {
            if (!ControllerInput.rightStick())
            {
                if (ControllerInput.leftGrip())
                {
                    if (left == null)
                    {
                        left = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        left.transform.position = GorillaLocomotion.Player.Instance.leftControllerTransform.transform.position + new Vector3(0f, -0.1f, 0f);
                        left.transform.rotation = GorillaLocomotion.Player.Instance.leftControllerTransform.transform.rotation;
                        left.transform.localScale = new Vector3(0.05f, 0.2f, 0.2f) * GorillaLocomotion.Player.Instance.scale;

                        Renderer renderer = left.GetComponent<Renderer>();
                        renderer.material.shader = Shader.Find("GorillaTag/UberShader");
                        renderer.material.color = Cronos.Menu.Management.Watch.Cronos.theme;
                    }
                    else
                    {
                        if (left.activeSelf)
                        {
                            if (left.GetComponent<Renderer>().material.color != Cronos.Menu.Management.Watch.Cronos.theme)
                                left.GetComponent<Renderer>().material.color = Cronos.Menu.Management.Watch.Cronos.theme;
                        }
                        else
                        {
                            left.transform.localScale = new Vector3(0.05f, 0.2f, 0.2f) * GorillaLocomotion.Player.Instance.scale;
                            left.transform.position = GorillaLocomotion.Player.Instance.leftControllerTransform.transform.position + new Vector3(0f, -0.1f, 0f);
                            left.transform.rotation = GorillaLocomotion.Player.Instance.leftControllerTransform.transform.rotation;
                            left.SetActive(true);
                        }
                    }
                }
                else
                    if (left != null)
                        if (left.activeSelf)
                            left.SetActive(false);
            }
            else
                if (left != null)
                    if (left.activeSelf)
                        left.SetActive(false);

            if (!ControllerInput.leftStick())
            {
                if (ControllerInput.rightGrip())
                {
                    if (right == null)
                    {
                        right = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        right.transform.position = GorillaLocomotion.Player.Instance.rightControllerTransform.transform.position + new Vector3(0f, -0.1f, 0f);
                        right.transform.rotation = GorillaLocomotion.Player.Instance.rightControllerTransform.transform.rotation;
                        right.transform.localScale = new Vector3(0.05f, 0.2f, 0.2f) * GorillaLocomotion.Player.Instance.scale;

                        Renderer renderer = right.GetComponent<Renderer>();
                        renderer.material.shader = Shader.Find("GorillaTag/UberShader");
                        renderer.material.color = Cronos.Menu.Management.Watch.Cronos.theme;
                    }
                    else
                    {
                        if (right.activeSelf)
                        {
                            if (right.GetComponent<Renderer>().material.color != Cronos.Menu.Management.Watch.Cronos.theme)
                                right.GetComponent<Renderer>().material.color = Cronos.Menu.Management.Watch.Cronos.theme;
                        }
                        else
                        {
                            left.transform.localScale = new Vector3(0.05f, 0.2f, 0.2f) * GorillaLocomotion.Player.Instance.scale;
                            right.transform.position = GorillaLocomotion.Player.Instance.rightControllerTransform.transform.position + new Vector3(0f, -0.1f, 0f);
                            right.transform.rotation = GorillaLocomotion.Player.Instance.rightControllerTransform.transform.rotation;
                            right.SetActive(true);
                        }
                    }
                }
                else
                    if (right != null)
                        if (right.activeSelf)
                            right.SetActive(false);
            }
            else
                if (right != null)
                    if (right.activeSelf)
                        right.SetActive(false);
        }

        public static void Cleanup()
        {
            if (left != null)
                if (left.activeSelf)
                    left.SetActive(false);

            if (right != null)
                if (right.activeSelf)
                    right.SetActive(false);
        }
    }
}
