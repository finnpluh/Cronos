using BepInEx;
using Cronos.Menu.Libraries;
using Cronos.Menu.Management;
using Cronos.Menu.Management.Watch;
using Cronos.Menu.Mods;
using Cronos.Menu.Mods.Modders;
using Cronos.Menu.Mods.Saftey;
using Cronos.Menu.Mods.Settings;
using Cronos.Menu.Mods.Visual;
using Cronos.Menu.Mods.World;
using Cronos.Menu.Utilities;
using ExitGames.Client.Photon;
using GorillaNetworking;
using Newtonsoft.Json;
using Photon.Pun;
using Photon.Voice.PUN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Valve.VR;
using static FriendBackendController;
using static MB3_MeshBakerRoot.ZSortObjects;

namespace Cronos
{
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public void OnEnable() => HarmonyPatches.Patch(true);

        public void OnDisable() => HarmonyPatches.Patch(false);

        public void Start() => Cronos.Menu.Management.Watch.Cronos.Start();

        public void Update() => Cronos.Menu.Management.Watch.Cronos.Load();
    }
}
