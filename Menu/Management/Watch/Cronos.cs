#region Refrences
using BepInEx;
using Cronos.Menu.Libraries;
using Cronos.Menu.Mods;
using Cronos.Menu.Mods.Settings;
using Cronos.Menu.Mods.Player;
using Cronos.Menu.Mods.Saftey;
using Cronos.Menu.Utilities;
using GorillaNetworking;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Cronos.Menu.Mods.Movement;
using Cronos.Menu.Mods.Visual;
using System.Collections.Generic;
using PlayFab;
using ExitGames.Client.Photon;
using Cronos.Menu.Mods.World;
using Cronos.Menu.Mods.Self;
using TMPro;
using Cronos.Menu.Mods.Modders;
using static BoingKit.BoingWork;
using UnityEngine.XR;
using System.Security.Cryptography;
#endregion

namespace Cronos.Menu.Management.Watch
{
    public class Cronos
    {
        #region Variables
        public static Color theme;
        public static GameObject watch = null;
        public static GameObject screen = null;
        public static Text text = null;
        private static float mod_cooldown = 0f;
        private static float scroll_cooldown = 0f;
        private static float toggle_cooldown = 0f;
        private static float rate = 0.35f;
        private static float time = 0f;
        private static int mod_index = 0;
        public static int scroll_index = 0;
        private static bool authenticated = false;
        private static bool initialized = false;
        private static bool waked = false;
        private static bool toggled = true;
        private static bool tooltip = false;
        #endregion

        #region Modules
        public static Button[] modules = new Button[]
        {
            new Button { category = "Settings", title = "Configuration", status = Button.Statuses.Functional, toggled = false, isToggleable = false, isAdjustable = true, optionIndex = 0, optionTitle = "Mode", options = new string[] { "Load", "Save" }, blatant = false, tooltip = "Saves/loads your module configuration", action = () => Configuration.Update() },
            new Button { category = "Settings", title = "Notifications", status = Button.Statuses.Functional, toggled = true, isToggleable = true, blatant = false, tooltip = "Alerts you of various events (Joins, reports, etc)", action = () => Notifications.Toggle(true), disableAction = () => Notifications.Toggle(false) },
            new Button { category = "Settings", title = "Volume", status = Button.Statuses.Functional, toggled = false, isToggleable = false, isAdjustable = true, optionIndex = 5, optionTitle = "Mode", options = new string[] { "0%", "10%", "20%", "30%", "40%", "50%", "60%", "70%", "80%", "90%", "100%", }, blatant = false, tooltip = "Adjusts the volume of the watch sounds", action = () => Volume.Run() },
            new Button { category = "Settings", title = "Change Theme", status = Button.Statuses.Functional, toggled = false, isToggleable = false, isAdjustable = true, optionIndex = 0, optionTitle = "Style", options = new string[] { "Stock", "Red", "Amber", "Honey", "Lime", "Cyan", "Blue", "Mauve", "Pink", "Grey" }, blatant = false, tooltip = "Adjusts the color of your watch", action = () => ChangeTheme.Run() },
            new Button { category = "Settings", title = "Follow Theme", status = Button.Statuses.Functional, toggled = false, isToggleable = true, isAdjustable = false, blatant = false, tooltip = "Makes visual mods follow your color theme", action = () => FollowTheme.Update(true), disableAction = () => FollowTheme.Update(false) },
            new Button { category = "Settings", title = "Ghost Rig", status = Button.Statuses.Functional, toggled = true, isToggleable = true, isAdjustable = false, blatant = true, tooltip = "Adds a client-side rig when using rig mods", action = () => GhostRig.Update(true), disableAction = () => GhostRig.Update(false) },

            new Button { category = "Movement", title = "Fly", status = Button.Statuses.Functional, toggled = false, isToggleable = true, isAdjustable = true, optionIndex = 0, optionTitle = "Mode", options = new string[] { "Sloth", "Slow", "Stock", "Swift", "Fast" }, blatant = true, tooltip = "Fly forward with your heads rotation", action = () => Fly.Run() },
            new Button { category = "Movement", title = "Slingshot", status = Button.Statuses.Functional, toggled = false, isToggleable = true, isAdjustable = true, optionIndex = 0, optionTitle = "Mode", options = new string[] { "Sloth", "Slow", "Stock", "Swift", "Fast" }, blatant = true, tooltip = "Slingshot yourself forward with left trigger", action = () => Menu.Mods.Movement.Slingshot.Run() },
            new Button { category = "Movement", title = "Scale", status = Button.Statuses.Functional, toggled = false, isToggleable = true, isAdjustable = true, optionIndex = 0, optionTitle = "Length", options = new string[] { "0.2", "0.4", "0.6", "0.8", "1.2", "1.4", "1.6", "1.8", "2.0", "2.2", "2.4", "2.6", "2.8", "3.0" }, blatant = true, tooltip = "Adjusts your player scale bigger/smaller", action = () => Scale.Run(), disableAction = () => Scale.Cleanup() },
            new Button { category = "Movement", title = "Speed", status = Button.Statuses.Functional, toggled = false, isToggleable = true, isAdjustable = true, optionIndex = 0, optionTitle = "Boost", options = new string[] { "6.6", "6.7", "6.8", "6.9", "7.0", "7.1", "7.2", "7.3", "7.4", "7.5", "7.6", "7.7", "7.8", "7.9", "8.0", "8.1", "8.2", "8.3", "8.4", "8.5", "8.6" }, blatant = false, tooltip = "Adjusts your speed amount bigger", action = () => Speed.Run(), disableAction = () => Speed.Cleanup() },
            new Button { category = "Movement", title = "Dash", status = Button.Statuses.Functional, toggled = false, isToggleable = true, isAdjustable = true, optionIndex = 0, optionTitle = "Mode", options = new string[] { "Sloth", "Slow", "Stock", "Swift", "Fast" }, blatant = true, tooltip = "Click X to dash forward", action = () => Dash.Run() },
            new Button { category = "Movement", title = "Wall Walk", status = Button.Statuses.Functional, toggled = false, isToggleable = true, isAdjustable = true, optionIndex = 0, optionTitle = "Mode", options = new string[] { "Weak", "Soft", "Stock", "Hard", "Harsh" }, blatant = true, tooltip = "Hold any grip to stick to walls", action = () => WallWalk.Run() },
            new Button { category = "Movement", title = "Gravity", status = Button.Statuses.Functional, toggled = false, isToggleable = true, isAdjustable = true, optionIndex = 0, optionTitle = "Mode", options = new string[] { "Lower", "Low", "High", "Higher" }, blatant = true, tooltip = "Adjusts your player gravity", action = () => Gravity.Run() },
            new Button { category = "Movement", title = "Platforms", status = Button.Statuses.Functional, toggled = false, isToggleable = true, blatant = true, tooltip = "Both grips to spawn climable platforms", action = () => Platforms.Run(), disableAction = () => Platforms.Cleanup() },
            new Button { category = "Movement", title = "No Freeze", status = Button.Statuses.Functional, toggled = false, isToggleable = true, blatant = true, tooltip = "Removes the ability to get frozen", action = () => NoFreeze.Run() },
            new Button { category = "Movement", title = "Slip Control", status = Button.Statuses.Functional, toggled = false, isToggleable = true, isAdjustable = true, optionIndex = 0, optionTitle = "Mode", options = new string[] { "Weak", "Soft", "Stock", "Hard", "Harsh" }, blatant = false, tooltip = "Adds more control to slippery objects", action = () => SlipControl.Run(), disableAction = () => SlipControl.Cleanup() },
            new Button { category = "Movement", title = "Teleport Gun", status = Button.Statuses.Functional, toggled = false, isToggleable = true, blatant = true, tooltip = "Teleports to where you shoot at", action = () => TeleportGun.Run(), disableAction = () => TeleportGun.Cleanup() },
            new Button { category = "Movement", title = "Long Arms", status = Button.Statuses.Functional, toggled = false, isToggleable = true, isAdjustable = true, optionIndex = 0, optionTitle = "Length", options = new string[] { "1.02", "1.04", "1.05", "1.1", "1.15", "1.2", "1.25", "1.3", "1.35", "1.4", "1.45", "1.5" }, blatant = false, tooltip = "Adjusts your arm length longer", action = () => LongArms.Run(), disableAction = () => LongArms.Cleanup() },

            new Button { category = "Safety", title = "Full Ghost Mode", status = Button.Statuses.Functional, toggled = false, isToggleable = true, blatant = false, tooltip = "Untoggles blatent mods & toggles ghost visuals", action = () => FullGhostMode.Run(), disableAction = () => FullGhostMode.Cleanup() },
            new Button { category = "Safety", title = "Anti Report", status = Button.Statuses.Functional, toggled = false, isToggleable = true, blatant = true, tooltip = "Disconnects you before getting reported", action = () => AntiReport.Run() },
            new Button { category = "Safety", title = "Anti Moderator", status = Button.Statuses.Functional, toggled = false, isToggleable = true, blatant = true, tooltip = "Disconnects you when a moderator is in your room", action = () => AntiModerator.Run() },
            new Button { category = "Safety", title = "Pc Igloo Bypass", status = Button.Statuses.Functional, toggled = false, isToggleable = true, blatant = false, tooltip = "Simulates the Quest igloo colliders", action = () => PcIglooBypass.Run(), disableAction = () => PcIglooBypass.Cleanup() },
            new Button { category = "Safety", title = "RPC Bypass", status = Button.Statuses.Functional, toggled = true, isToggleable = true, blatant = false, tooltip = "Prevents you from getting RPC reports" },
            new Button { category = "Safety", title = "RPC Uncapper", status = Button.Statuses.Functional, toggled = true, isToggleable = true, blatant = false, tooltip = "Uncaps your RPC limit", action = () => RPCUncapper.Run() },

            new Button { category = "Visual", title = "Rainbow Sky", status = Button.Statuses.Functional, toggled = false, isToggleable = true, blatant = true, tooltip = "Makes the skybox color rainbow", action = () => RainbowSky.Run(), disableAction = () => RainbowSky.Cleanup() },
            new Button { category = "Visual", title = "Time", status = Button.Statuses.Functional, toggled = false, isToggleable = false, isAdjustable = true, optionIndex = 0, optionTitle = "Mode", options = new string[] { "Night", "Rise", "Day", "Dawn" }, blatant = false, tooltip = "Sets the time of day to your preference", action = () => _Time.Run() },
            new Button { category = "Visual", title = "Graphics", status = Button.Statuses.Functional, toggled = false, isToggleable = true, isAdjustable = true, optionIndex = 0, optionTitle = "Mode", options = new string[] { "Poor", "Bad", "Awful", "Worst" }, blatant = true, tooltip = "Sets the graphics quality to your preference", action = () => _Graphics.Run(), disableAction = () => _Graphics.Cleanup() },
            new Button { category = "Visual", title = "Boards", status = Button.Statuses.Functional, toggled = true, isToggleable = true, blatant = true, tooltip = "Edits stump boards to provide information", action = () => Boards.Change(), disableAction = () => Boards.Cleanup() },
            new Button { category = "Visual", title = "ESP", status = Button.Statuses.Functional, toggled = false, isToggleable = true, blatant = false, tooltip = "Makes see-through boxes around all players", action = () => ESP.Run(), disableAction = () => ESP.Cleanup() },
            new Button { category = "Visual", title = "Chams", status = Button.Statuses.Functional, toggled = false, isToggleable = true, blatant = true, tooltip = "Makes all players see through", action = () => Chams.Run(), disableAction = () => Chams.Cleanup() },
            new Button { category = "Visual", title = "Nametags", status = Button.Statuses.Functional, toggled = false, isToggleable = true, blatant = false, tooltip = "Creates nametags above all players", action = () => Nametags.Run(), disableAction = () => Nametags.Cleanup() },
            new Button { category = "Visual", title = "Tracers", status = Button.Statuses.Functional, toggled = false, isToggleable = true, isAdjustable = true, optionIndex = 0, optionTitle = "Width", options = new string[] { "0.002", "0.004", "0.006", "0.008", "0.01", "0.012", "0.014", "0.016", "0.018", "0.02" }, blatant = false, tooltip = "Creates tracers to all players", action = () => Tracers.Run(), disableAction = () => Tracers.Cleanup() },

            new Button { category = "Player", title = "Reach", status = Button.Statuses.Functional, toggled = false, isToggleable = true, isAdjustable = true, optionIndex = 0, optionTitle = "Amount", options = new string[] { "0.3", "0.4", "0.5", "0.6", "0.7", "0.8" }, blatant = false, tooltip = "Tag people from farther away" },
            new Button { category = "Player", title = "Near Distance", status = Button.Statuses.Functional, toggled = false, isToggleable = true, isAdjustable = true, optionIndex = 0, optionTitle = "Hand", options = new string[] { "Left", "Right" }, blatant = false, tooltip = "Alerts you when tagged players are nearby", action = () => NearDistance.Run(), disableAction = () => NearDistance.Cleanup() },
            new Button { category = "Player", title = "Log Player Gun", status = Button.Statuses.Functional, toggled = false, isToggleable = true, isAdjustable = true, optionIndex = 0, optionTitle = "Mode", options = new string[] { "ID", "Color", "Items" }, blatant = true, tooltip = "Displays other players information", action = () => LogPlayerGun.Run(), disableAction = () => LogPlayerGun.Cleanup() },

            new Button { category = "Modders", title = "Mod Checker", status = Button.Statuses.Functional, toggled = false, isToggleable = false, blatant = true, tooltip = "Notifies you for players using specific mods", action = () => ModChecker.Run() },
            new Button { category = "Modders", title = "Cronos Sense", status = Button.Statuses.Functional, toggled = true, isToggleable = true, blatant = true, tooltip = "Adds a tag to you & everyone using Cronos", action = () => CronosSense.Run(), disableAction = () => CronosSense.Cleanup() },
            new Button { category = "Modders", title = "Break Nametags", status = Button.Statuses.Functional, toggled = false, isToggleable = true, blatant = true, tooltip = "Breaks your nametag for prople using Nametags", action = () => BreakNametags.Run(), disableAction = () => BreakNametags.Cleanup() },

            new Button { category = "World", title = "Button Gun", status = Button.Statuses.Functional, toggled = false, isToggleable = true, blatant = true, tooltip = "Clicks or toggles the button you shoot at", action = () => ButtonGun.Run(), disableAction = () => ButtonGun.Cleanup() },
            new Button { category = "World", title = "Glider Gun", status = Button.Statuses.Functional, toggled = false, isToggleable = true, isAdjustable = true, optionIndex = 0, optionTitle = "Mode", options = new string[] { "Move", "Spaz", "Spawn" }, blatant = true, tooltip = "Move, spaz, or spawn the glider you shoot", action = () => GliderGun.Run(), disableAction = () => GliderGun.Cleanup() },
            new Button { category = "World", title = "Glider All", status = Button.Statuses.Functional, toggled = false, isToggleable = false, isAdjustable = true, optionIndex = 0, optionTitle = "Mode", options = new string[] { "Bring", "Spawn" }, blatant = true, tooltip = "Bring or spawn all gliders", action = () => GliderAll.Run() },
            new Button { category = "World", title = "Balloon Gun", status = Button.Statuses.Functional, toggled = false, isToggleable = true, isAdjustable = true, optionIndex = 0, optionTitle = "Mode", options = new string[] { "Move", "Spaz", "Kill" }, blatant = true, tooltip = "Move, spaz, or kill the balloon you shoot", action = () => BalloonGun.Run(), disableAction = () => BalloonGun.Cleanup() },
            new Button { category = "World", title = "Balloon All", status = Button.Statuses.Functional, toggled = false, isToggleable = false, isAdjustable = true, optionIndex = 0, optionTitle = "Mode", options = new string[] { "Bring", "Kill" }, blatant = true, tooltip = "Bring or kill all balloons", action = () => BalloonAll.Run() },

            new Button { category = "Self", title = "Spoof Monke Biz", status = Button.Statuses.Functional, toggled = false, isToggleable = false, isAdjustable = true, optionIndex = 0, optionTitle = "Level", options = new string[] { "200", "400", "600", "800", "1000", "5000", "7500", "10000", "99999" }, blatant = true, tooltip = "Spoofs the quest badge level", action = () => SpoofMonkeBiz.Run() },
            new Button { category = "Self", title = "Anti AFK", status = Button.Statuses.Functional, toggled = false, isToggleable = true, blatant = true, tooltip = "Prevents you from getting AFK kicked", action = () => AntiAFK.Run(), disableAction = () => AntiAFK.Cleanup() },
            new Button { category = "Self", title = "Ghost", status = Button.Statuses.Functional, toggled = false, isToggleable = true, blatant = true, tooltip = "Click A to walk around outside of your body", action = () => Ghost.Run(), disableAction = () => Ghost.Cleanup() },
            new Button { category = "Self", title = "Invisibility", status = Button.Statuses.Functional, toggled = false, isToggleable = true, blatant = true, tooltip = "Click X to turn invisible", action = () => Invisibility.Run(), disableAction = () => Invisibility.Cleanup() },
            new Button { category = "Self", title = "Rig Gun", status = Button.Statuses.Functional, toggled = false, isToggleable = true, blatant = true, tooltip = "Carries your rig to where you shoot", action = () => RigGun.Run(), disableAction = () => RigGun.Cleanup() },
            new Button { category = "Self", title = "Hold Rig", status = Button.Statuses.Functional, toggled = false, isToggleable = true, blatant = true, tooltip = "Left or right grip to hold your player", action = () => HoldRig.Run(), disableAction = () => HoldRig.Cleanup() },
            new Button { category = "Self", title = "Unlock Competitive", status = Button.Statuses.Functional, toggled = false, isToggleable = false, blatant = false, tooltip = "Unlocks the competitve queue", action = () => UnlockCompetitive.Run() },
            new Button { category = "Self", title = "Give All Cosmetics", status = Button.Statuses.Functional, toggled = false, isToggleable = false, blatant = false, tooltip = "Unlocks every cosmetic you don't already own", action = () => GiveAllCosmetics.Run() },

            new Button { category = "Room", title = "Disconnect", status = Button.Statuses.Functional, toggled = false, isToggleable = false, blatant = false, tooltip = "Remotely leaves your current room", action = () => NetworkSystem.Instance.ReturnToSinglePlayer() }
        };
        #endregion

        public static string Readjust(string text, int characters, int amount)
        {
            if (text.Length <= characters)
                return text;

            if (scroll_index + characters > text.Length)
                scroll_index = 0;

            string result = text.Substring(scroll_index, characters);

            if (scroll_index + characters < text.Length && amount > 0)
                result += $"<color={CronosColorUtilities.Color32ToHTML(Settings.text_accent)}>{new string('.', amount)}</color>";

            return result;
        }

        public static void Start()
        {
            GameObject manager = new GameObject("Manager - Cronos");
            manager.AddComponent<Notifications>();
            manager.AddComponent<GhostRigManager>();
            UnityEngine.Object.DontDestroyOnLoad(manager);
        }

        public static void Load()
        {
            if (GorillaTagger.hasInstance)
            {
                if (Settings.animated)
                    theme = Color.Lerp(Settings.theme[0], Settings.theme[1], (Mathf.Sin(Time.time * Mathf.PI / 3f) + 1f) / 2f);
                else
                    if (theme != Settings.theme[0])
                        theme = Settings.theme[0];

                scroll_cooldown += Time.deltaTime;

                if (scroll_cooldown >= 0.85f)
                {
                    scroll_index++;
                    scroll_cooldown = 0f;
                }

                if (!initialized) //Made by Blaku, edited by Finn
                {
                    if (GameObject.Find("[SteamVR]"))
                        Settings.steamvr = true;

                    string hand = "Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.L/upper_arm.L/forearm.L/hand.L";
                    watch = UnityEngine.Object.Instantiate(GameObject.Find($"{hand}/huntcomputer (1)"), GameObject.Find(hand).transform);
                    watch.SetActive(true);
                    watch.name = "Watch - Cronos";
                    watch.GetComponent<GorillaHuntComputer>().enabled = false;

                    Transform anchor = watch.transform.Find("HuntWatch_ScreenLocal/Canvas/Anchor");
                    anchor.Find("Hat").gameObject.SetActive(false);
                    anchor.Find("Face").gameObject.SetActive(false);
                    anchor.Find("Badge").gameObject.SetActive(false);
                    anchor.Find("Left Hand").gameObject.SetActive(false);
                    anchor.Find("Right Hand").gameObject.SetActive(false);
                    anchor.Find("Material").gameObject.SetActive(false);
                    anchor.Find("Material").GetComponent<Image>().color = Settings.toggles[2];

                    screen = watch.transform.Find("HuntWatch_ScreenLocal").gameObject;
                    screen.GetComponent<Renderer>().material = new Material(Shader.Find("GorillaTag/UberShader"));
                    screen.GetComponent<Renderer>().material.color = theme;

                    text = watch.transform.Find("HuntWatch_ScreenLocal/Canvas/Anchor").Find("Text").gameObject.GetComponent<Text>();
                    text.alignment = TextAnchor.UpperLeft;
                    text.fontSize = 6;
                    initialized = true;
                }
                else
                {
                    if (watch.GetComponent<Renderer>().material.color != theme)
                        watch.GetComponent<Renderer>().material.color = theme;

                    if (screen.GetComponent<Renderer>().material.color != theme)
                        screen.GetComponent<Renderer>().material.color = theme;

                    if (PlayFabClientAPI.IsClientLoggedIn())
                    {
                        if (!authenticated)
                        {
                            Notifications.Send("<color=blue>Cronos Solutions</color>", $"Thank you for using Cronos");
                            authenticated = true;
                        }
                    }

                    if (authenticated)
                    {
                        foreach (Button Mod in modules)
                            if (Mod.isToggleable)
                                if (Mod.toggled)
                                    if (Mod.action != null)
                                        Mod.action();

                        if (waked)
                        {
                            GameObject material = watch.transform.Find("HuntWatch_ScreenLocal/Canvas/Anchor").Find("Material").gameObject;

                            if (material.activeSelf != !tooltip)
                                material.SetActive(!tooltip);

                            if (tooltip)
                                text.text = modules[mod_index].tooltip;
                            else
                            {
                                string display = Readjust(modules[mod_index].title, 11, 3) +
                                    $"\n<color={CronosColorUtilities.Color32ToHTML(Settings.text_accent)}>{modules[mod_index].category}</color>" +
                                    $"{(modules[mod_index].isAdjustable ? $"\n<color={CronosColorUtilities.Color32ToHTML(Settings.text_accent)}>({modules[mod_index].optionTitle}: {modules[mod_index].options[modules[mod_index].optionIndex]})</color>" : null)}" +
                                    $"{$"{(modules[mod_index].isAdjustable ? "\n" : "\n\n")}{new string(' ', 5)}({mod_index + 1}/{modules.Length})"}";
                                Color indicator = modules[mod_index].status != Button.Statuses.Broken ? modules[mod_index].isToggleable ? (modules[mod_index].toggled ? Settings.toggles[0] : Settings.toggles[1]) : Settings.toggles[2] : Color.black;

                                if (text.text != display)
                                    text.text = display;

                                if (material.GetComponent<Image>().color != indicator)
                                    material.GetComponent<Image>().color = indicator;
                            }
                        }

                        if (ControllerInput.leftStick() && ControllerInput.rightStick())
                        {
                            if (!waked)
                            {
                                watch.transform.Find("HuntWatch_ScreenLocal/Canvas/Anchor").Find("Material").gameObject.SetActive(true);
                                GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(8, true, Settings.volume);
                                toggle_cooldown = Time.time + 0.5f;
                                waked = true;
                            }
                            else
                            {
                                if (Time.time >= toggle_cooldown)
                                {
                                    toggled = !toggled;
                                    watch.SetActive(toggled);
                                    screen.SetActive(toggled);

                                    GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(175, true, Settings.volume);
                                    toggle_cooldown = Time.time + 0.35f;
                                }
                            }
                        }

                        if (waked)
                        {
                            if (toggled)
                            {
                                if (ControllerInput.leftStick())
                                {
                                    if (ControllerInput.rightTrigger())
                                    {
                                        if (!tooltip)
                                        {
                                            if (Time.time >= mod_cooldown)
                                            {
                                                if (mod_index + 1 != modules.Length)
                                                    mod_index++;
                                                else
                                                    mod_index = 0;

                                                GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(8, true, Settings.volume);
                                                mod_cooldown = Time.time + rate;
                                            }

                                            if (rate > 0.175f)
                                                rate -= 0.0025f;
                                        }
                                    }
                                    else if (ControllerInput.rightGrip())
                                    {
                                        if (!tooltip)
                                        {
                                            if (Time.time >= mod_cooldown)
                                            {
                                                if (mod_index > 0)
                                                    mod_index--;
                                                else
                                                    mod_index = modules.Length - 1;

                                                GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(8, true, Settings.volume);
                                                mod_cooldown = Time.time + rate;
                                            }

                                            if (rate > 0.175f)
                                                rate -= 0.0025f;
                                        }
                                    }
                                    else
                                        if (rate != 0.35f)
                                            rate = 0.35f;

                                    if (ControllerInput.rightPrimary())
                                    {
                                        if (!tooltip)
                                        {
                                            if (Time.time >= mod_cooldown)
                                            {
                                                if (modules[mod_index].status != Button.Statuses.Broken)
                                                {
                                                    if (modules[mod_index].isToggleable)
                                                    {
                                                        modules[mod_index].toggled = !modules[mod_index].toggled;
                                                        Notifications.Send($"<color={(modules[mod_index].toggled ? "green" : "red")}>Toggled Mod</color>", $"{modules[mod_index].title} toggled {(modules[mod_index].toggled ? "On" : "Off")}");

                                                        if (!modules[mod_index].toggled)
                                                            if (modules[mod_index].disableAction != null)
                                                                modules[mod_index].disableAction();
                                                    }
                                                    else
                                                    {
                                                        if (Settings.ghost_mode)
                                                        {
                                                            if (modules[mod_index].blatant)
                                                                Notifications.Send("<color=blue>Notice</color>", $"Turn off Full Ghost Mode to use this mod");
                                                            else
                                                                modules[mod_index].action();
                                                        }
                                                        else
                                                            modules[mod_index].action();
                                                    }
                                                }
                                                else
                                                    Notifications.Send("<color=blue>Notice</color>", $"Cannot use {modules[mod_index].title}, this mod is currently broken");

                                                GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(8, true, Settings.volume);
                                                mod_cooldown = Time.time + 0.35f;
                                            }
                                        }
                                    }

                                    if (ControllerInput.rightSecondary())
                                    {
                                        if (Time.time >= mod_cooldown)
                                        {
                                            tooltip = !tooltip;
                                            GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(8, true, Settings.volume);
                                            mod_cooldown = Time.time + 0.35f;
                                        }
                                    }
                                }

                                if (modules[mod_index].isAdjustable)
                                {
                                    if (ControllerInput.rightStick())
                                    {
                                        if (ControllerInput.leftTrigger())
                                        {
                                            if (Time.time >= mod_cooldown)
                                            {
                                                if (modules[mod_index].optionIndex + 1 != modules[mod_index].options.Length)
                                                    modules[mod_index].optionIndex++;
                                                else
                                                    modules[mod_index].optionIndex = 0;

                                                GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(8, true, Settings.volume);
                                                mod_cooldown = Time.time + 0.35f;
                                            }
                                        }

                                        if (ControllerInput.leftGrip())
                                        {
                                            if (Time.time >= mod_cooldown)
                                            {
                                                if (modules[mod_index].optionIndex > 0)
                                                    modules[mod_index].optionIndex--;
                                                else
                                                    modules[mod_index].optionIndex = modules[mod_index].options.Length - 1;

                                                GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(8, true, Settings.volume);
                                                mod_cooldown = Time.time + 0.35f;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            List<string> categories = new List<string>();
                            for (int i = 0; i < modules.Length; i++)
                                if (!categories.Contains(modules[i].category))
                                    categories.Add(modules[i].category);
                            text.text = "Cronos Cheat Watch" + $"\n<color={CronosColorUtilities.Color32ToHTML(Settings.text_accent)}>{modules.Length} mods\n{categories.Count} categories</color>";
                        }
                    }
                    else
                    {
                        time += Time.deltaTime;
                        text.text = $"Loading...\n<color={CronosColorUtilities.Color32ToHTML(Settings.text_accent)}>({Mathf.FloorToInt(time)} seconds elapsed)</color>";
                    }
                }
            }
        }
    }
}