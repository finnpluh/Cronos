using Cronos.Menu.Libraries;
using Cronos.Menu.Management.Watch;
using Cronos.Menu.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cronos.Menu.Mods.Settings
{
    public class Configuration
    {
        private static string directory = "Cronos";
        private static string path = Path.Combine(directory, "Cronos_Configuration.json");

        public static void Update()
        {
            Button button = CronosButtonUtilities.GetButtonFromName("Configuration");
            switch (button.optionIndex)
            {
                case 0:
                    Load();
                    break;
                case 1:
                    Save();
                    break;
            }
        }

        private static void Save()
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            var data = new Dictionary<string, string>();
            foreach (Button[] modules in Cronos.Menu.Management.Watch.Cronos.pages)
            {
                foreach (Button module in modules)
                {
                    if (module.isToggleable)
                    {
                        string value = module.toggled ? "on" : "off";
                        if (module.isAdjustable)
                            value += $":{module.optionIndex}";

                        data[module.title.ToLower()] = value;
                    }
                }
            }

            File.WriteAllText(path, JsonConvert.SerializeObject(data, Formatting.Indented));
            Notifications.Send("SUCCESS", $"Configuration saved", Color.green);
        }

        private static void Load()
        {
            if (File.Exists(path))
            {
                var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(path));
                foreach (Button[] modules in Cronos.Menu.Management.Watch.Cronos.pages)
                {
                    foreach (Button module in modules)
                    {
                        try
                        {
                            if (module.isToggleable)
                            {
                                if (data.ContainsKey(module.title.ToLower()))
                                {
                                    string[] values = data[module.title.ToLower()].Split(':');
                                    module.toggled = values[0] == "on";

                                    if (module.isAdjustable && values.Length > 1 && int.TryParse(values[1], out int index))
                                        module.optionIndex = index;

                                    if (module.status != Button.Statuses.Broken)
                                        if (!module.toggled)
                                            if (module.disableAction != null)
                                                module.disableAction();

                                    Notifications.Send("SUCCESS", $"Configuration loaded", Color.green);
                                }
                            }
                        }
                        catch { }
                    }
                }
            }
            else
                Notifications.Send("ERROR", $"No configuration saved", Color.red);
        }
    }
}
