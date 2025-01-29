using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cronos.Menu.Utilities
{
    public class CronosColorUtilities
    {
        public static Color HTMLToColor32(string hex)
        {
            Color color;
            if (ColorUtility.TryParseHtmlString(hex, out color))
                return (Color32)color;
            return new Color32(0, 0, 0, 255);
        }

        public static string Color32ToHTML(Color32 color)
        {
            return $"#{color.r:X2}{color.g:X2}{color.b:X2}";
        }
    }
}
