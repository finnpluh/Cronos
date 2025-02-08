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
using System.Linq;

namespace Cronos.Menu.Management.Watch
{
    public class Cronos
    {
        public static Color theme;
        public static GameObject watch = null;
        public static GameObject screen = null;
        public static Text text = null;

        private static float mod_cooldown = 0f;
        private static float scroll_cooldown = 0f;
        private static float toggle_cooldown = 0f;
        private static float rate = 0.35f;
        private static float time = 0f;

        private static int page_index = 0;
        private static int mod_index = 0;
        private static int scroll_index = 0;

        private static bool authenticated = false;
        private static bool initialized = false;
        private static bool waked = false;
        private static bool toggled = true;
        private static bool tooltip = false;

        public static List<Button[]> pages = new List<Button[]>();

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

        private static void UpdateCurrentPage(int page)
        {
            if (page >= 0)
            {
                if (page < pages.Count)
                {
                    page_index = page;

                    if (mod_index != 0)
                        mod_index = 0;
                }
            }
        }

        public static void Start()
        {
            GameObject manager = new GameObject("Manager - Cronos");
            manager.AddComponent<GhostRigManager>();
            UnityEngine.Object.DontDestroyOnLoad(manager);

            pages.Add(new Button[]
            {
                new Button { title = "Settings", status = Button.Statuses.Functional, toggled = false, isToggleable = false, blatant = false, tooltip = "Enters the Settings page", action = () => UpdateCurrentPage(1) },
                new Button { title = "Movement", status = Button.Statuses.Functional, toggled = false, isToggleable = false, blatant = false, tooltip = "Enters the Movement page", action = () => UpdateCurrentPage(2) },
                new Button { title = "Safety", status = Button.Statuses.Functional, toggled = false, isToggleable = false, blatant = false, tooltip = "Enters the Safety page", action = () => UpdateCurrentPage(3) },
                new Button { title = "Visual", status = Button.Statuses.Functional, toggled = false, isToggleable = false, blatant = false, tooltip = "Enters the Visual page", action = () => UpdateCurrentPage(4) },
                new Button { title = "Player", status = Button.Statuses.Functional, toggled = false, isToggleable = false, blatant = false, tooltip = "Enters the Player page", action = () => UpdateCurrentPage(5) },
                new Button { title = "Modders", status = Button.Statuses.Functional, toggled = false, isToggleable = false, blatant = false, tooltip = "Enters the Modders page", action = () => UpdateCurrentPage(6) },
                new Button { title = "World", status = Button.Statuses.Functional, toggled = false, isToggleable = false, blatant = false, tooltip = "Enters the World page", action = () => UpdateCurrentPage(7) },
                new Button { title = "Self", status = Button.Statuses.Functional, toggled = false, isToggleable = false, blatant = false, tooltip = "Enters the Self page", action = () => UpdateCurrentPage(8) },
                new Button { title = "Room", status = Button.Statuses.Functional, toggled = false, isToggleable = false, blatant = false, tooltip = "Enters the Room page", action = () => UpdateCurrentPage(9) },
            });

            pages.Add(new Button[]
            {
                new Button { title = "Configuration", status = Button.Statuses.Functional, toggled = false, isToggleable = false, isAdjustable = true, optionIndex = 0, optionTitle = "Mode", options = new string[] { "Load", "Save" }, blatant = false, tooltip = "Saves/loads your module configuration", action = () => Configuration.Update() },
                new Button { title = "Notifications", status = Button.Statuses.Functional, toggled = true, isToggleable = true, blatant = false, tooltip = "Alerts you of various events (Joins, reports, etc)", action = () => Notifications.Run(), disableAction = () => Notifications.Cleanup() },
                new Button { title = "Volume", status = Button.Statuses.Functional, toggled = false, isToggleable = false, isAdjustable = true, optionIndex = 5, optionTitle = "Mode", options = new string[] { "0%", "10%", "20%", "30%", "40%", "50%", "60%", "70%", "80%", "90%", "100%", }, blatant = false, tooltip = "Adjusts the volume of the watch sounds", action = () => Volume.Run() },
                new Button { title = "Change Theme", status = Button.Statuses.Functional, toggled = false, isToggleable = false, isAdjustable = true, optionIndex = 0, optionTitle = "Style", options = new string[] { "Stock", "Red", "Amber", "Honey", "Lime", "Cyan", "Blue", "Mauve", "Pink", "Grey" }, blatant = false, tooltip = "Adjusts the color of your watch", action = () => ChangeTheme.Run() },
                new Button { title = "Follow Theme", status = Button.Statuses.Functional, toggled = false, isToggleable = true, isAdjustable = false, blatant = false, tooltip = "Makes visual mods follow your color theme", action = () => FollowTheme.Update(true), disableAction = () => FollowTheme.Update(false) },
                new Button { title = "Ghost Rig", status = Button.Statuses.Functional, toggled = true, isToggleable = true, isAdjustable = false, blatant = true, tooltip = "Adds a client-side rig when using rig mods", action = () => GhostRig.Update(true), disableAction = () => GhostRig.Update(false) },
                new Button { title = "Back", status = Button.Statuses.Functional, toggled = false, isToggleable = false, blatant = false, tooltip = "Returns to the home page", action = () => UpdateCurrentPage(0) }
            });

            pages.Add(new Button[]
            {
                new Button { title = "Fly", status = Button.Statuses.Functional, toggled = false, isToggleable = true, isAdjustable = true, optionIndex = 0, optionTitle = "Mode", options = new string[] { "Sloth", "Slow", "Stock", "Swift", "Fast" }, blatant = true, tooltip = "Fly forward with your heads rotation", action = () => Fly.Run() },
                new Button { title = "Slingshot", status = Button.Statuses.Functional, toggled = false, isToggleable = true, isAdjustable = true, optionIndex = 0, optionTitle = "Mode", options = new string[] { "Sloth", "Slow", "Stock", "Swift", "Fast" }, blatant = true, tooltip = "Slingshot yourself forward with left trigger", action = () => Menu.Mods.Movement.Slingshot.Run() },
                new Button { title = "Scale", status = Button.Statuses.Functional, toggled = false, isToggleable = true, isAdjustable = true, optionIndex = 0, optionTitle = "Length", options = new string[] { "0.2", "0.4", "0.6", "0.8", "1.2", "1.4", "1.6", "1.8", "2.0", "2.2", "2.4", "2.6", "2.8", "3.0" }, blatant = true, tooltip = "Adjusts your player scale bigger/smaller", action = () => Scale.Run(), disableAction = () => Scale.Cleanup() },
                new Button { title = "Speed", status = Button.Statuses.Functional, toggled = false, isToggleable = true, isAdjustable = true, optionIndex = 0, optionTitle = "Boost", options = new string[] { "6.6", "6.7", "6.8", "6.9", "7.0", "7.1", "7.2", "7.3", "7.4", "7.5", "7.6", "7.7", "7.8", "7.9", "8.0", "8.1", "8.2", "8.3", "8.4", "8.5", "8.6" }, blatant = false, tooltip = "Adjusts your speed amount bigger", action = () => Speed.Run(), disableAction = () => Speed.Cleanup() },
                new Button { title = "Dash", status = Button.Statuses.Functional, toggled = false, isToggleable = true, isAdjustable = true, optionIndex = 0, optionTitle = "Mode", options = new string[] { "Sloth", "Slow", "Stock", "Swift", "Fast" }, blatant = true, tooltip = "Click X to dash forward", action = () => Dash.Run() },
                new Button { title = "Wall Walk", status = Button.Statuses.Functional, toggled = false, isToggleable = true, isAdjustable = true, optionIndex = 0, optionTitle = "Mode", options = new string[] { "Weak", "Soft", "Stock", "Hard", "Harsh" }, blatant = true, tooltip = "Hold any grip to stick to walls", action = () => WallWalk.Run() },
                new Button { title = "Gravity", status = Button.Statuses.Functional, toggled = false, isToggleable = true, isAdjustable = true, optionIndex = 0, optionTitle = "Mode", options = new string[] { "Lower", "Low", "High", "Higher" }, blatant = true, tooltip = "Adjusts your player gravity", action = () => Gravity.Run() },
                new Button { title = "Platforms", status = Button.Statuses.Functional, toggled = false, isToggleable = true, blatant = true, tooltip = "Both grips to spawn climable platforms", action = () => Platforms.Run(), disableAction = () => Platforms.Cleanup() },
                new Button { title = "No Freeze", status = Button.Statuses.Functional, toggled = false, isToggleable = true, blatant = true, tooltip = "Removes the ability to get frozen", action = () => NoFreeze.Run() },
                new Button { title = "Slip Control", status = Button.Statuses.Functional, toggled = false, isToggleable = true, isAdjustable = true, optionIndex = 0, optionTitle = "Mode", options = new string[] { "Weak", "Soft", "Stock", "Hard", "Harsh" }, blatant = false, tooltip = "Adds more control to slippery objects", action = () => SlipControl.Run(), disableAction = () => SlipControl.Cleanup() },
                new Button { title = "Teleport Gun", status = Button.Statuses.Functional, toggled = false, isToggleable = true, blatant = true, tooltip = "Teleports to where you shoot at", action = () => TeleportGun.Run(), disableAction = () => TeleportGun.Cleanup() },
                new Button { title = "Long Arms", status = Button.Statuses.Functional, toggled = false, isToggleable = true, isAdjustable = true, optionIndex = 0, optionTitle = "Length", options = new string[] { "1.02", "1.04", "1.05", "1.1", "1.15", "1.2", "1.25", "1.3", "1.35", "1.4", "1.45", "1.5" }, blatant = false, tooltip = "Adjusts your arm length longer", action = () => LongArms.Run(), disableAction = () => LongArms.Cleanup() },
                new Button { title = "Back", status = Button.Statuses.Functional, toggled = false, isToggleable = false, blatant = false, tooltip = "Returns to the home page", action = () => UpdateCurrentPage(0) }
            });

            pages.Add(new Button[]
            {
                new Button { title = "Full Ghost Mode", status = Button.Statuses.Functional, toggled = false, isToggleable = true, blatant = false, tooltip = "Untoggles blatent mods & toggles ghost visuals", action = () => FullGhostMode.Run(), disableAction = () => FullGhostMode.Cleanup() },
                new Button { title = "Anti Report", status = Button.Statuses.Functional, toggled = false, isToggleable = true, blatant = true, tooltip = "Disconnects you before getting reported", action = () => AntiReport.Run() },
                new Button { title = "Anti Moderator", status = Button.Statuses.Functional, toggled = false, isToggleable = true, blatant = true, tooltip = "Disconnects you when a moderator is in your room", action = () => AntiModerator.Run() },
                new Button { title = "Pc Igloo Bypass", status = Button.Statuses.Functional, toggled = false, isToggleable = true, blatant = false, tooltip = "Simulates the Quest igloo colliders", action = () => PcIglooBypass.Run(), disableAction = () => PcIglooBypass.Cleanup() },
                new Button { title = "RPC Bypass", status = Button.Statuses.Unsafe, toggled = true, isToggleable = true, blatant = false, tooltip = "Prevents you from getting RPC reports" },
                new Button { title = "RPC Uncapper", status = Button.Statuses.Unsafe, toggled = true, isToggleable = true, blatant = false, tooltip = "Uncaps your RPC limit", action = () => RPCUncapper.Run() },
                new Button { title = "Back", status = Button.Statuses.Functional, toggled = false, isToggleable = false, blatant = false, tooltip = "Returns to the home page", action = () => UpdateCurrentPage(0) }
            });

            pages.Add(new Button[]
            {
                new Button { title = "Rainbow Sky", status = Button.Statuses.Functional, toggled = false, isToggleable = true, blatant = true, tooltip = "Makes the skybox color rainbow", action = () => RainbowSky.Run(), disableAction = () => RainbowSky.Cleanup() },
                new Button { title = "Time", status = Button.Statuses.Functional, toggled = false, isToggleable = false, isAdjustable = true, optionIndex = 0, optionTitle = "Mode", options = new string[] { "Night", "Rise", "Day", "Dawn" }, blatant = false, tooltip = "Sets the time of day to your preference", action = () => _Time.Run() },
                new Button { title = "Graphics", status = Button.Statuses.Functional, toggled = false, isToggleable = true, isAdjustable = true, optionIndex = 0, optionTitle = "Mode", options = new string[] { "Poor", "Bad", "Awful", "Worst" }, blatant = true, tooltip = "Sets the graphics quality to your preference", action = () => _Graphics.Run(), disableAction = () => _Graphics.Cleanup() },
                new Button { title = "Boards", status = Button.Statuses.Functional, toggled = true, isToggleable = true, blatant = true, tooltip = "Edits stump boards to provide information", action = () => Boards.Change(), disableAction = () => Boards.Cleanup() },
                new Button { title = "ESP", status = Button.Statuses.Functional, toggled = false, isToggleable = true, blatant = false, tooltip = "Makes see-through boxes around all players", action = () => ESP.Run(), disableAction = () => ESP.Cleanup() },
                new Button { title = "Chams", status = Button.Statuses.Functional, toggled = false, isToggleable = true, blatant = true, tooltip = "Makes all players see through", action = () => Chams.Run(), disableAction = () => Chams.Cleanup() },
                new Button { title = "Nametags", status = Button.Statuses.Functional, toggled = false, isToggleable = true, blatant = false, tooltip = "Creates nametags above all players", action = () => Nametags.Run(), disableAction = () => Nametags.Cleanup() },
                new Button { title = "Tracers", status = Button.Statuses.Functional, toggled = false, isToggleable = true, isAdjustable = true, optionIndex = 0, optionTitle = "Width", options = new string[] { "0.002", "0.004", "0.006", "0.008", "0.01", "0.012", "0.014", "0.016", "0.018", "0.02" }, blatant = false, tooltip = "Creates tracers to all players", action = () => Tracers.Run(), disableAction = () => Tracers.Cleanup() },
                new Button { title = "Back", status = Button.Statuses.Functional, toggled = false, isToggleable = false, blatant = false, tooltip = "Returns to the home page", action = () => UpdateCurrentPage(0) }
            });

            pages.Add(new Button[]
            {
                new Button { title = "Reach", status = Button.Statuses.Functional, toggled = false, isToggleable = true, isAdjustable = true, optionIndex = 0, optionTitle = "Amount", options = new string[] { "0.3", "0.4", "0.5", "0.6", "0.7", "0.8" }, blatant = false, tooltip = "Tag people from farther away" },
                new Button { title = "Near Distance", status = Button.Statuses.Functional, toggled = false, isToggleable = true, isAdjustable = true, optionIndex = 0, optionTitle = "Hand", options = new string[] { "Left", "Right" }, blatant = false, tooltip = "Alerts you when tagged players are nearby", action = () => NearDistance.Run(), disableAction = () => NearDistance.Cleanup() },
                new Button { title = "Log Player Gun", status = Button.Statuses.Functional, toggled = false, isToggleable = true, isAdjustable = true, optionIndex = 0, optionTitle = "Mode", options = new string[] { "ID", "Color", "Items" }, blatant = true, tooltip = "Displays other players information", action = () => LogPlayerGun.Run(), disableAction = () => LogPlayerGun.Cleanup() },
                new Button { title = "Back", status = Button.Statuses.Functional, toggled = false, isToggleable = false, blatant = false, tooltip = "Returns to the home page", action = () => UpdateCurrentPage(0) }
            });

            pages.Add(new Button[]
            {
                new Button { title = "Mod Checker", status = Button.Statuses.Functional, toggled = false, isToggleable = false, blatant = true, tooltip = "Notifies you for players using specific mods", action = () => ModChecker.Run() },
                new Button { title = "Cronos Sense", status = Button.Statuses.Functional, toggled = true, isToggleable = true, blatant = true, tooltip = "Adds a tag to you & everyone using Cronos", action = () => CronosSense.Run(), disableAction = () => CronosSense.Cleanup() },
                new Button { title = "Break Nametags", status = Button.Statuses.Functional, toggled = false, isToggleable = true, blatant = true, tooltip = "Breaks your nametag for prople using Nametags", action = () => BreakNametags.Run(), disableAction = () => BreakNametags.Cleanup() },
                new Button { title = "Back", status = Button.Statuses.Functional, toggled = false, isToggleable = false, blatant = false, tooltip = "Returns to the home page", action = () => UpdateCurrentPage(0) }
            });

            pages.Add(new Button[]
            {
                new Button { title = "Button Gun", status = Button.Statuses.Functional, toggled = false, isToggleable = true, blatant = true, tooltip = "Clicks or toggles the button you shoot at", action = () => ButtonGun.Run(), disableAction = () => ButtonGun.Cleanup() },
                new Button { title = "Animal Gun", status = Button.Statuses.Unsafe, toggled = false, isToggleable = true, isAdjustable = true, optionIndex = 0, optionTitle = "Mode", options = new string[] { "Move", "Spaz", "Spawn" }, blatant = true, tooltip = "Move, spaz, or spawn the animal you shoot", action = () => AnimalGun.Run(), disableAction = () => AnimalGun.Cleanup() },
                new Button { title = "Animal All", status = Button.Statuses.Unsafe, toggled = false, isToggleable = false, isAdjustable = true, optionIndex = 0, optionTitle = "Mode", options = new string[] { "Bring", "Spawn" }, blatant = true, tooltip = "Bring or spawn all animals", action = () => AnimalAll.Run() },
                new Button { title = "Glider Gun", status = Button.Statuses.Unsafe, toggled = false, isToggleable = true, isAdjustable = true, optionIndex = 0, optionTitle = "Mode", options = new string[] { "Move", "Spaz", "Spawn" }, blatant = true, tooltip = "Move, spaz, or spawn the glider you shoot", action = () => GliderGun.Run(), disableAction = () => GliderGun.Cleanup() },
                new Button { title = "Glider All", status = Button.Statuses.Unsafe, toggled = false, isToggleable = false, isAdjustable = true, optionIndex = 0, optionTitle = "Mode", options = new string[] { "Bring", "Spawn" }, blatant = true, tooltip = "Bring or spawn all gliders", action = () => GliderAll.Run() },
                new Button { title = "Balloon Gun", status = Button.Statuses.Unsafe, toggled = false, isToggleable = true, isAdjustable = true, optionIndex = 0, optionTitle = "Mode", options = new string[] { "Move", "Spaz", "Kill" }, blatant = true, tooltip = "Move, spaz, or kill the balloon you shoot", action = () => BalloonGun.Run(), disableAction = () => BalloonGun.Cleanup() },
                new Button { title = "Balloon All", status = Button.Statuses.Unsafe, toggled = false, isToggleable = false, isAdjustable = true, optionIndex = 0, optionTitle = "Mode", options = new string[] { "Bring", "Kill" }, blatant = true, tooltip = "Bring or kill all balloons", action = () => BalloonAll.Run() },
                new Button { title = "Monkeye Gun", status = Button.Statuses.Unsafe, toggled = false, isToggleable = true, isAdjustable = true, optionIndex = 0, optionTitle = "Mode", options = new string[] { "Move", "Spaz", "Kill" }, blatant = true, tooltip = "Bring or kill the basement monster you shoot", action = () => MonkeyeGun.Run(), disableAction = () => MonkeyeGun.Cleanup() },
                new Button { title = "Monkeye All", status = Button.Statuses.Unsafe, toggled = false, isToggleable = false, isAdjustable = true, optionIndex = 0, optionTitle = "Mode", options = new string[] { "Bring", "Kill" }, blatant = true, tooltip = "Bring or kill all basement monsters", action = () => MonkeyeAll.Run() },
                new Button { title = "Fling Rope Gun", status = Button.Statuses.Unsafe, toggled = false, isToggleable = true, blatant = true, tooltip = "Flings the rope you shoot at", action = () => FlingRopeGun.Run(), disableAction = () => FlingRopeGun.Cleanup() },
                new Button { title = "Fling Ropes All", status = Button.Statuses.Unsafe, toggled = false, isToggleable = false, blatant = true, tooltip = "Flings all ropes", action = () => FlingRopesAll.Run() },
                new Button { title = "Reset Soccer Game", status = Button.Statuses.Unsafe, toggled = false, isToggleable = false, blatant = true, tooltip = "Resets the soccer score", action = () => ResetSoccerGame.Run() },
                new Button { title = "Set Soccer Score", status = Button.Statuses.Unsafe, toggled = false, isToggleable = false, isAdjustable = true, optionIndex = 0, optionTitle = "Team", options = new string[] { "Red", "Blue" }, blatant = true, tooltip = "Adds a point to your specified team", action = () => SetSoccerScore.Run() },
                new Button { title = "Back", status = Button.Statuses.Functional, toggled = false, isToggleable = false, blatant = false, tooltip = "Returns to the home page", action = () => UpdateCurrentPage(0) }
            });

            pages.Add(new Button[]
            {
                new Button { title = "Spoof Monke Biz", status = Button.Statuses.Functional, toggled = false, isToggleable = false, isAdjustable = true, optionIndex = 0, optionTitle = "Level", options = new string[] { "200", "400", "600", "800", "1000", "5000", "7500", "10000", "99999" }, blatant = true, tooltip = "Spoofs the quest badge level", action = () => SpoofMonkeBiz.Run() },
                new Button { title = "Set Team Color", status = Button.Statuses.Unsafe, toggled = false, isToggleable = false, isAdjustable = true, optionIndex = 0, optionTitle = "Team", options = new string[] { "Red", "Blue" }, blatant = true, tooltip = "Spams your team in the soccer game mode", action = () => SetTeamColor.Run() },
                new Button { title = "Anti AFK", status = Button.Statuses.Functional, toggled = false, isToggleable = true, blatant = true, tooltip = "Prevents you from getting AFK kicked", action = () => AntiAFK.Run(), disableAction = () => AntiAFK.Cleanup() },
                new Button { title = "Ghost", status = Button.Statuses.Functional, toggled = false, isToggleable = true, blatant = true, tooltip = "Click A to walk around outside of your body", action = () => Ghost.Run(), disableAction = () => Ghost.Cleanup() },
                new Button { title = "Invisibility", status = Button.Statuses.Functional, toggled = false, isToggleable = true, blatant = true, tooltip = "Click X to turn invisible", action = () => Invisibility.Run(), disableAction = () => Invisibility.Cleanup() },
                new Button { title = "Rig Gun", status = Button.Statuses.Functional, toggled = false, isToggleable = true, blatant = true, tooltip = "Carries your rig to where you shoot", action = () => RigGun.Run(), disableAction = () => RigGun.Cleanup() },
                new Button { title = "Hold Rig", status = Button.Statuses.Functional, toggled = false, isToggleable = true, blatant = true, tooltip = "Left or right grip to hold your player", action = () => HoldRig.Run(), disableAction = () => HoldRig.Cleanup() },
                new Button { title = "Unlock Competitive", status = Button.Statuses.Functional, toggled = false, isToggleable = false, blatant = false, tooltip = "Unlocks the competitve queue", action = () => UnlockCompetitive.Run() },
                new Button { title = "Give All Cosmetics", status = Button.Statuses.Functional, toggled = false, isToggleable = false, blatant = false, tooltip = "Unlocks every cosmetic you don't already own", action = () => GiveAllCosmetics.Run() },
                new Button { title = "Back", status = Button.Statuses.Functional, toggled = false, isToggleable = false, blatant = false, tooltip = "Returns to the home page", action = () => UpdateCurrentPage(0) }
            });

            pages.Add(new Button[]
            {
                new Button { title = "Disconnect", status = Button.Statuses.Functional, toggled = false, isToggleable = false, blatant = false, tooltip = "Remotely leaves your current room", action = () => NetworkSystem.Instance.ReturnToSinglePlayer() },
                new Button { title = "Back", status = Button.Statuses.Functional, toggled = false, isToggleable = false, blatant = false, tooltip = "Returns to the home page", action = () => UpdateCurrentPage(0) }
            });
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

                if (!initialized)
                {
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
                        foreach (Button[] modules in pages)
                            foreach (Button module in modules)
                                if (module.isToggleable)
                                    if (module.toggled)
                                        if (module.status != Button.Statuses.Broken)
                                            if (module.action != null)
                                                module.action();

                        Button[] buttons = pages[page_index];
                        if (waked)
                        {
                            GameObject material = watch.transform.Find("HuntWatch_ScreenLocal/Canvas/Anchor").Find("Material").gameObject;

                            if (material.activeSelf != !tooltip)
                                material.SetActive(!tooltip);

                            if (tooltip)
                                text.text = buttons[mod_index].tooltip;
                            else
                            {
                                string display = Readjust(buttons[mod_index].title, 11, 3) +
                                    $"{(buttons[mod_index].isAdjustable ? $"\n<color={CronosColorUtilities.Color32ToHTML(Settings.text_accent)}>({buttons[mod_index].optionTitle}: {buttons[mod_index].options[buttons[mod_index].optionIndex]})</color>" : null)}" +
                                    $"{$"{(buttons[mod_index].isAdjustable ? "\n" : "\n\n")}{new string(' ', 5)}({mod_index + 1}/{buttons.Length})"}";
                                Color indicator = buttons[mod_index].status != Button.Statuses.Broken ? buttons[mod_index].isToggleable ? (buttons[mod_index].toggled ? Settings.toggles[0] : Settings.toggles[1]) : Settings.toggles[2] : Color.black;

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
                                                if (mod_index + 1 != buttons.Length)
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
                                                    mod_index = buttons.Length - 1;

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
                                                if (buttons[mod_index].status != Button.Statuses.Broken)
                                                {
                                                    if (buttons[mod_index].status == Button.Statuses.Unsafe)
                                                        Notifications.Send("<color=blue>Notice</color>", $"{buttons[mod_index].title} modifies networking components, use at your own risk");

                                                    if (buttons[mod_index].isToggleable)
                                                    {
                                                        buttons[mod_index].toggled = !buttons[mod_index].toggled;
                                                        Notifications.Send($"<color={(buttons[mod_index].toggled ? "green" : "red")}>Toggled Mod</color>", $"{buttons[mod_index].title} toggled {(buttons[mod_index].toggled ? "On" : "Off")}");

                                                        if (!buttons[mod_index].toggled)
                                                            if (buttons[mod_index].disableAction != null)
                                                                buttons[mod_index].disableAction();
                                                    }
                                                    else
                                                    {
                                                        if (Settings.ghost_mode)
                                                        {
                                                            if (buttons[mod_index].blatant)
                                                                Notifications.Send("<color=blue>Notice</color>", $"Turn off Full Ghost Mode to use this mod");
                                                            else
                                                                buttons[mod_index].action();
                                                        }
                                                        else
                                                            buttons[mod_index].action();
                                                    }
                                                }
                                                else
                                                    Notifications.Send("<color=blue>Notice</color>", $"Cannot use {buttons[mod_index].title}, this mod is currently broken");

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

                                if (buttons[mod_index].isAdjustable)
                                {
                                    if (ControllerInput.rightStick())
                                    {
                                        if (ControllerInput.leftTrigger())
                                        {
                                            if (Time.time >= mod_cooldown)
                                            {
                                                if (buttons[mod_index].optionIndex + 1 != buttons[mod_index].options.Length)
                                                    buttons[mod_index].optionIndex++;
                                                else
                                                    buttons[mod_index].optionIndex = 0;

                                                GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(8, true, Settings.volume);
                                                mod_cooldown = Time.time + 0.35f;
                                            }
                                        }

                                        if (ControllerInput.leftGrip())
                                        {
                                            if (Time.time >= mod_cooldown)
                                            {
                                                if (buttons[mod_index].optionIndex > 0)
                                                    buttons[mod_index].optionIndex--;
                                                else
                                                    buttons[mod_index].optionIndex = buttons[mod_index].options.Length - 1;

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
                            text.text = "Cronos Cheat Watch" + $"\n<color={CronosColorUtilities.Color32ToHTML(Settings.text_accent)}>{pages[0].Length - 1} categories\n{pages.Sum(page => page.Length)} mods</color>";
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