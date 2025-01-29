using Cronos.Menu.Management.Watch;
using Cronos.Menu.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cronos.Menu.Mods.Movement
{
    public class Gravity
    {
        public static void Run()
        {
            Button button = CronosButtonUtilities.GetButtonFromName("Gravity");
            switch (button.optionIndex)
            {
                case 0:
                    GorillaTagger.Instance.bodyCollider.attachedRigidbody.AddForce(-Physics.gravity / 2f, ForceMode.Acceleration);
                    break;
                case 1:
                    GorillaTagger.Instance.bodyCollider.attachedRigidbody.AddForce(-Physics.gravity / 4f, ForceMode.Acceleration);
                    break;
                case 2:
                    GorillaTagger.Instance.bodyCollider.attachedRigidbody.AddForce(Physics.gravity * 0.5f, ForceMode.Acceleration);
                    break;
                case 3:
                    GorillaTagger.Instance.bodyCollider.attachedRigidbody.AddForce(Physics.gravity * 1f, ForceMode.Acceleration);
                    break;
            }
        }
    }
}
