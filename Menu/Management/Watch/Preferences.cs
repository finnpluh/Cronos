using Cronos.Menu.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Cronos.Menu.Management.Watch
{
    public class Preferences
    {
        public static Color[] theme = new Color[2] { CronosColorUtilities.HTMLToColor32("#354DE7"), CronosColorUtilities.HTMLToColor32("#745CEE") };
        public static Color[] toggles = new Color[3] { Color.green, Color.red, Color.grey };
        public static Color text_accent = new Color(Color.white.r / 1.75f, Color.white.g / 1.75f, Color.white.b / 1.75f, Color.white.a);

        public static float volume = 0.5f;
        public static bool[] preferences = new bool[] { true, false, true, false };
    }
}
