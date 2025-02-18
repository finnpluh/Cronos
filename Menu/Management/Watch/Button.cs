using System;

namespace Cronos.Menu.Management.Watch
{
    public class Button
    {
        public enum Statuses { Functional, Broken, Unsafe, Master }
        public string title { get; set; }
        public string[] options { get; set; }
        public string tooltip { get; set; }
        public bool toggled { get; set; }
        public bool blatant { get; set; }
        public Statuses status { get; set; }
        public bool isToggleable { get; set; }
        public string optionTitle { get; set; }
        public bool isAdjustable { get; set; }
        public int optionIndex { get; set; }
        public Action action { get; set; }
        public Action disableAction { get; set; }
    }
}
