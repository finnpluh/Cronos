using Cronos.Menu.Libraries;
using GorillaNetworking;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cronos.Menu.Mods.World
{
    public class ButtonGun
    {
        private static Guns gun = null;
        private static GameObject pointer = null;
        private static LineRenderer line = null;

        public static void Run()
        {
            if (gun == null)
                gun = new Guns();

            if (pointer != gun.pointer)
                pointer = gun.pointer;

            if (line != gun.line)
                line = gun.line;

            gun.Create(() =>
            {
                Collider collider = GameObject.Find("Player Objects/Player VR Controller/GorillaPlayer/TurnParent/RightHandTriggerCollider").GetComponent<Collider>();

                GorillaPressableButton pressable = gun.raycast.collider.GetComponentInParent<GorillaPressableButton>();
                if (pressable != null)
                    pressable.GetType().GetMethod("OnTriggerEnter", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(pressable, new object[] { collider });

                GorillaPlayerLineButton scoreboard = gun.raycast.collider.GetComponentInParent<GorillaPlayerLineButton>();
                if (scoreboard != null)
                    scoreboard.GetType().GetMethod("OnTriggerEnter", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(scoreboard, new object[] { collider });

                GorillaKeyboardButton keyboard = gun.raycast.collider.GetComponentInParent<GorillaKeyboardButton>();
                if (keyboard != null)
                    keyboard.GetType().GetMethod("OnTriggerEnter", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(keyboard, new object[] { collider });

                GorillaReportButton report = gun.raycast.collider.GetComponent<GorillaReportButton>();
                if (report != null)
                    report.GetType().GetMethod("OnTriggerEnter", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(report, new object[] { collider });
            });
        }

        public static void Cleanup() => gun.Cleanup();
    }
}
