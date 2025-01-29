using Cronos.Menu.Management.Watch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cronos.Menu.Utilities
{
    public class GhostRigManager : MonoBehaviour
    {
        private VRRig vrrig = null;

        private void Update()
        {
            if (GorillaTagger.hasInstance)
            {
                if (Settings.ghost_rig)
                {
                    if (!GorillaTagger.Instance.offlineVRRig.enabled)
                    {
                        if (vrrig == null)
                        {
                            vrrig = UnityEngine.Object.Instantiate(GorillaTagger.Instance.offlineVRRig, GorillaLocomotion.Player.Instance.transform.position, GorillaLocomotion.Player.Instance.transform.rotation);
                            vrrig.name = "Ghost Rig - Cronos";
                            vrrig.enabled = true;

                            vrrig.transform.Find("RigAnchor/rig/body/shoulder.L/upper_arm.L/forearm.L/hand.L/Watch - Cronos").gameObject.SetActive(false);
                            vrrig.transform.Find("RigAnchor/rig/bodySlideAudio").gameObject.SetActive(false);
                            vrrig.transform.Find("VR Constraints/LeftArm/Left Arm IK/SlideAudio").gameObject.SetActive(false);
                            vrrig.transform.Find("VR Constraints/RightArm/Right Arm IK/SlideAudio").gameObject.SetActive(false);
                        }
                        else
                        {
                            if (!vrrig.gameObject.activeSelf)
                                vrrig.gameObject.SetActive(true);
                            else
                            {
                                Color color = Color.black;
                                if (color.a != 0.5f)
                                    color.a = 0.5f;

                                if (vrrig.mainSkin.material.shader != Shader.Find("GUI/Text Shader"))
                                    vrrig.mainSkin.material.shader = Shader.Find("GUI/Text Shader");

                                if (vrrig.mainSkin.material.color != color)
                                    vrrig.mainSkin.material.color = color;

                                vrrig.head.rigTarget.transform.position = GorillaLocomotion.Player.Instance.headCollider.transform.position;
                                vrrig.head.rigTarget.transform.rotation = GorillaLocomotion.Player.Instance.headCollider.transform.rotation;

                                vrrig.bodyTransform.transform.position = GorillaLocomotion.Player.Instance.bodyCollider.transform.position;
                                vrrig.bodyTransform.transform.rotation = GorillaLocomotion.Player.Instance.bodyCollider.transform.rotation;

                                vrrig.leftHandTransform.rotation = GorillaLocomotion.Player.Instance.leftControllerTransform.rotation;
                                vrrig.leftHandTransform.position = GorillaLocomotion.Player.Instance.leftControllerTransform.position;

                                vrrig.rightHandTransform.position = GorillaLocomotion.Player.Instance.rightControllerTransform.position;
                                vrrig.rightHandTransform.rotation = GorillaLocomotion.Player.Instance.rightControllerTransform.rotation;
                            }
                        }
                    }
                    else
                    if (vrrig != null)
                        if (vrrig.gameObject.activeSelf)
                            vrrig.gameObject.SetActive(false);
                }
                else
                    if (vrrig != null)
                        if (vrrig.gameObject.activeSelf)
                            vrrig.gameObject.SetActive(false);
            }
        }
    }
}
