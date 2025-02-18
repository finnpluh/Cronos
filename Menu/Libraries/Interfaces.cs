using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Cronos.Menu.Libraries
{
    public class Interfaces
    {
        public static void Create(string name, ref GameObject parent, ref TextMeshPro text, TextAlignmentOptions alignment, float size = 0.5f)
        {
            parent = new GameObject(name);
            text = parent.AddComponent<TextMeshPro>();

            RectTransform transform = parent.GetComponent<RectTransform>();
            transform.sizeDelta = new Vector2(1.75f, 1.75f);

            TextMeshPro nametag = GorillaTagger.Instance.offlineVRRig.playerText1;

            text.font = nametag.font;
            text.characterSpacing = nametag.characterSpacing;
            text.lineSpacing = 50f;
            text.alignment = alignment;
            text.fontSize = size;
            text.text = string.Empty;

            parent.transform.LookAt(Camera.main.transform);
            parent.transform.Rotate(0f, 180f, 0f);
        }
    }
}
