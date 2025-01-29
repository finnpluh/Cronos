using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cronos.Menu.Mods.Saftey
{
    public class PcIglooBypass
    {
        public static void Run()
        {
            GameObject igloo = GameObject.Find("Mountain/Geometry/goodigloo");
            if (igloo != null)
            {
                Collider collider = igloo.GetComponent<Collider>();
                if (collider != null)
                {
                    if (collider.enabled)
                        collider.enabled = false;
                }
            }
        }

        public static void Cleanup()
        {
            GameObject igloo = GameObject.Find("Mountain/Geometry/goodigloo");
            if (igloo != null)
            {
                Collider collider = igloo.GetComponent<Collider>();
                if (collider != null)
                {
                    if (!collider.enabled)
                        collider.enabled = true;
                }
            }
        }
    }
}
