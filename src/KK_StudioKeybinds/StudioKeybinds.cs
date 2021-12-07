using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using Studio;
using Manager;

namespace StudioKeybinds
{
    [BepInPlugin(GUID, PluginName, Version)]
    [BepInProcess("CharaStudio")]
    public class StudioKeybinds : BaseUnityPlugin
    {
        public const string PluginName = "KK_StudioKeybinds";
        public const string GUID = "org.njaecha.plugins.studiokeybinds";
        public const string Version = "1.0.1";

        internal new static ManualLogSource Logger;

        internal static ConfigEntry<bool> modEnabled;

        internal static ConfigEntry<KeyboardShortcut> SCundo;
        internal static ConfigEntry<KeyboardShortcut> SCundo2;
        internal static ConfigEntry<KeyboardShortcut> SCredo;
        internal static ConfigEntry<KeyboardShortcut> SCredo2;
        internal static ConfigEntry<KeyboardShortcut> SCsave;
        internal static ConfigEntry<KeyboardShortcut> SCexit;

        internal static ConfigEntry<KeyboardShortcut> SCmoveToCam;
        internal static ConfigEntry<KeyboardShortcut> SCduplicate;
        internal static ConfigEntry<KeyboardShortcut> SCdelete;
        internal static ConfigEntry<KeyboardShortcut> SCattach;
        internal static ConfigEntry<KeyboardShortcut> SCdetach;
        internal static ConfigEntry<KeyboardShortcut> SCcopy;

        internal static ConfigEntry<KeyboardShortcut> SCmove;
        internal static ConfigEntry<KeyboardShortcut> SCrotate;
        internal static ConfigEntry<KeyboardShortcut> SCscale;
        internal static ConfigEntry<KeyboardShortcut> SChideTransfrom;
        internal static ConfigEntry<KeyboardShortcut> SCtogglePlanes;
        internal static ConfigEntry<KeyboardShortcut> SCtoggleCenter;
        internal static ConfigEntry<KeyboardShortcut> SCsnap;

        internal static ConfigEntry<KeyboardShortcut> SCopenShortcutPage;
        internal static ConfigEntry<KeyboardShortcut> SCscreenshot;

        internal static ConfigEntry<KeyboardShortcut> XSCcam1;
        internal static ConfigEntry<KeyboardShortcut> XSCcam2;
        internal static ConfigEntry<KeyboardShortcut> XSCcam3;
        internal static ConfigEntry<KeyboardShortcut> XSCcam4;
        internal static ConfigEntry<KeyboardShortcut> XSCcam5;
        internal static ConfigEntry<KeyboardShortcut> XSCcam6;
        internal static ConfigEntry<KeyboardShortcut> XSCcam7;
        internal static ConfigEntry<KeyboardShortcut> XSCcam8;
        internal static ConfigEntry<KeyboardShortcut> XSCcam9;
        internal static ConfigEntry<KeyboardShortcut> XSCcam0;
        internal static ConfigEntry<KeyboardShortcut> XSCnextCam;

        internal static ConfigEntry<KeyboardShortcut> SCfocusObject;
        internal static ConfigEntry<KeyboardShortcut> SCresetAll;
        internal static ConfigEntry<KeyboardShortcut> SCresetRot;
        internal static ConfigEntry<KeyboardShortcut> SCresetRoll;
        internal static ConfigEntry<KeyboardShortcut> SCresetFov;
        internal static ConfigEntry<KeyboardShortcut> SCzoomIn;
        internal static ConfigEntry<KeyboardShortcut> SCzoomOut;
        internal static ConfigEntry<KeyboardShortcut> SCmoveRight;
        internal static ConfigEntry<KeyboardShortcut> SCmoveLeft;
        internal static ConfigEntry<KeyboardShortcut> SCmoveUp;
        internal static ConfigEntry<KeyboardShortcut> SCmoveDown;
        internal static ConfigEntry<KeyboardShortcut> SCmoveForward;
        internal static ConfigEntry<KeyboardShortcut> SCmoveBackwards;
        internal static ConfigEntry<KeyboardShortcut> SCfovPlus;
        internal static ConfigEntry<KeyboardShortcut> SCfovMinus;
        internal static ConfigEntry<KeyboardShortcut> SCrollRight;
        internal static ConfigEntry<KeyboardShortcut> SCrollLeft;
        internal static ConfigEntry<KeyboardShortcut> SCrotateRight;
        internal static ConfigEntry<KeyboardShortcut> SCrotateLeft;
        internal static ConfigEntry<KeyboardShortcut> SCrotateUp;
        internal static ConfigEntry<KeyboardShortcut> SCrotateDown;

        void Awake()
        {
            StudioKeybinds.Logger = base.Logger;

            modEnabled = Config.Bind("_General_", "Mod Enabled", true, "Enables/Disables the mod. Requires resart to take effect!");

            SCundo = Config.Bind("Basic Shortcuts", "Undo", new KeyboardShortcut(KeyCode.Z, KeyCode.LeftControl), "Undos the last action if possible");
            SCundo2 = Config.Bind("Basic Shortcuts", "Undo (Alternative)", new KeyboardShortcut(KeyCode.Z, KeyCode.RightControl), "Undos the last action if possible [Alternate Key]");
            SCredo = Config.Bind("Basic Shortcuts", "Redo", new KeyboardShortcut(KeyCode.Z, KeyCode.LeftShift, KeyCode.LeftControl), "Redo the last undone action if possible");
            SCredo2 = Config.Bind("Basic Shortcuts", "Redo (Alternative)", new KeyboardShortcut(KeyCode.Z, KeyCode.RightShift, KeyCode.RightControl), "Redo the last undone action if possible [Alternate Key]");
            SCsave = Config.Bind("Basic Shortcuts", "Save", new KeyboardShortcut(KeyCode.S, KeyCode.LeftControl), "Saves the Scene");
            SCexit = Config.Bind("Basic Shortcuts", "Exit", new KeyboardShortcut(KeyCode.Escape), "Shows the quit game dialogue");

            SCmoveToCam = Config.Bind("Object Shortcuts", "Move to Camera", new KeyboardShortcut(KeyCode.C), "Moves the selected object to the camera target");
            SCduplicate = Config.Bind("Object Shortcuts", "Duplicate", new KeyboardShortcut(KeyCode.D, KeyCode.LeftControl), "Duplicates the selected objects");
            SCdelete = Config.Bind("Object Shortcuts", "Delete", new KeyboardShortcut(KeyCode.Delete), "Deletes the selected objects");
            SCattach = Config.Bind("Object Shortcuts", "Attach", new KeyboardShortcut(KeyCode.P), "Modifier key to press while selecting a node in the workspace treeview to attach it to the previously selected node");
            SCdetach = Config.Bind("Object Shortcuts", "Detach", new KeyboardShortcut(KeyCode.O), "Modifier key to press while selecting a node in the workspace treeview to detach it from its parent");
            SCcopy = Config.Bind("Object Shortcuts", "Copy (Broken)", new KeyboardShortcut(KeyCode.None), "This should copy the Object with Ctrl+X, but for some reason it's broken and I have no idea why...");

            SCmove = Config.Bind("Transfrom Shortcuts", "Move", new KeyboardShortcut(KeyCode.W), "Sets the transfrom mode to Move");
            SCrotate = Config.Bind("Transfrom Shortcuts", "Rotate", new KeyboardShortcut(KeyCode.E), "Sets the transfrom mode to Rotate");
            SCscale = Config.Bind("Transfrom Shortcuts", "Scale", new KeyboardShortcut(KeyCode.R), "Sets the transfrom mode to Scale");
            SChideTransfrom = Config.Bind("Transfrom Shortcuts", "Hide", new KeyboardShortcut(KeyCode.Q), "Hides the transfrom node (Guide Object)");
            SCtogglePlanes = Config.Bind("Transfrom Shortcuts", "Toggle Planar Movement Control", new KeyboardShortcut(KeyCode.J), "Toggles the planar movement control panels");
            SCtoggleCenter = Config.Bind("Transfrom Shortcuts", "Toggle Center Point", new KeyboardShortcut(KeyCode.K), "Toggles the yellow dot at the center of the transfrom node");
            SCsnap = Config.Bind("Transfrom Shortcuts", "Snap Movement", new KeyboardShortcut(KeyCode.V), "Modiefier key to press while moving the transfrom node to activate snapping");

            SCopenShortcutPage = Config.Bind("Miscellaneous Shortcuts", "Shortcut Page", new KeyboardShortcut(KeyCode.F2), "Opens the Image that shows all the default shorcuts, pretty much useless");
            SCscreenshot = Config.Bind("Miscellaneous Shortcuts", "Screenshot", new KeyboardShortcut(KeyCode.F11), "WARNING: this setting is REDUNDANT if you use ScreenshotManager! Use its setting to change the Keybind.\n\nRenders a Screenshot");

            XSCcam1 = Config.Bind("Switch Cam Shortcuts", "Cam 1", new KeyboardShortcut(KeyCode.Alpha1), "Switches to Cam 1");
            XSCcam2 = Config.Bind("Switch Cam Shortcuts", "Cam 2", new KeyboardShortcut(KeyCode.Alpha2), "Switches to Cam 2");
            XSCcam3 = Config.Bind("Switch Cam Shortcuts", "Cam 3", new KeyboardShortcut(KeyCode.Alpha3), "Switches to Cam 3");
            XSCcam4 = Config.Bind("Switch Cam Shortcuts", "Cam 4", new KeyboardShortcut(KeyCode.Alpha4), "Switches to Cam 4");
            XSCcam5 = Config.Bind("Switch Cam Shortcuts", "Cam 5", new KeyboardShortcut(KeyCode.Alpha5), "Switches to Cam 5");
            XSCcam6 = Config.Bind("Switch Cam Shortcuts", "Cam 6", new KeyboardShortcut(KeyCode.Alpha6), "Switches to Cam 6");
            XSCcam7 = Config.Bind("Switch Cam Shortcuts", "Cam 7", new KeyboardShortcut(KeyCode.Alpha7), "Switches to Cam 7");
            XSCcam8 = Config.Bind("Switch Cam Shortcuts", "Cam 8", new KeyboardShortcut(KeyCode.Alpha8), "Switches to Cam 8");
            XSCcam9 = Config.Bind("Switch Cam Shortcuts", "Cam 9", new KeyboardShortcut(KeyCode.Alpha9), "Switches to Cam 9");
            XSCcam0 = Config.Bind("Switch Cam Shortcuts", "Cam 0", new KeyboardShortcut(KeyCode.Alpha0), "Switches to Cam 0");
            XSCnextCam = Config.Bind("Switch Cam Shortcuts", "Next Camera", new KeyboardShortcut(KeyCode.H), "Switches to the next camera");

            SCfocusObject = Config.Bind("Camera Base Shortcuts", "Focus Selected Object", new KeyboardShortcut(KeyCode.F), "Moves the camera target to the origin of the selected object");
            SCresetAll = Config.Bind("Camera Base Shortcuts", "Reset All", new KeyboardShortcut(KeyCode.A), "Resets the entire camera");
            SCresetRot = Config.Bind("Camera Rotate Shortcuts", "Reset Rotation", new KeyboardShortcut(KeyCode.Keypad5), "Resets the camera rotation (X and Y)");
            SCresetRoll = Config.Bind("Camera Rotate Shortcuts", "Reset Roll", new KeyboardShortcut(KeyCode.Slash), "Resets the camera roll (Z)");
            SCresetFov = Config.Bind("Camera FOV Shortcuts", "Reset FOV", new KeyboardShortcut(KeyCode.Semicolon), "Resets the camera FOV");

            SCzoomIn = Config.Bind("Camera Base Shortcuts", "Zoom In", new KeyboardShortcut(KeyCode.Home), "Zooms the camera in.\n ATTENTION: This Shortcut can only hold a single key!");
            SCzoomIn.SettingChanged += trimModifier;
            SCzoomOut = Config.Bind("Camera Base Shortcuts", "Zoom Out", new KeyboardShortcut(KeyCode.End), "Zooms the camera out.\n ATTENTION: This Shortcut can only hold a single key!");
            SCzoomOut.SettingChanged += trimModifier;
            SCmoveRight = Config.Bind("Camera Move Shortcuts", "Move Right", new KeyboardShortcut(KeyCode.RightArrow), "Moves the camera target right.\n ATTENTION: This Shortcut can only hold a single key!");
            SCmoveRight.SettingChanged += trimModifier;
            SCmoveLeft = Config.Bind("Camera Move Shortcuts", "Move Left", new KeyboardShortcut(KeyCode.LeftArrow), "Moves the camera target left.\n ATTENTION: This Shortcut can only hold a single key!");
            SCmoveLeft.SettingChanged += trimModifier;
            SCmoveForward = Config.Bind("Camera Move Shortcuts", "Move Forward", new KeyboardShortcut(KeyCode.UpArrow), "Moves the camera target forward.\n ATTENTION: This Shortcut can only hold a single key!");
            SCmoveForward.SettingChanged += trimModifier;
            SCmoveBackwards = Config.Bind("Camera Move Shortcuts", "Move Backwards", new KeyboardShortcut(KeyCode.DownArrow), "Moves the camera target backwards.\n ATTENTION: This Shortcut can only hold a single key!");
            SCmoveBackwards.SettingChanged += trimModifier;
            SCmoveUp = Config.Bind("Camera Move Shortcuts", "Move Up", new KeyboardShortcut(KeyCode.PageUp), "Moves the camera target up.\n ATTENTION: This Shortcut can only hold a single key!");
            SCmoveUp.SettingChanged += trimModifier;
            SCmoveDown = Config.Bind("Camera Move Shortcuts", "Move Down", new KeyboardShortcut(KeyCode.PageDown), "Moves the camera target down.\n ATTENTION: This Shortcut can only hold a single key!");
            SCmoveDown.SettingChanged += trimModifier;
            SCfovPlus = Config.Bind("Camera FOV Shortcuts", "Increase FOV", new KeyboardShortcut(KeyCode.Equals), "Increases the camera FOV.\n ATTENTION: This Shortcut can only hold a single key!");
            SCfovPlus.SettingChanged += trimModifier;
            SCfovMinus = Config.Bind("Camera FOV Shortcuts", "Decrease FOV", new KeyboardShortcut(KeyCode.RightBracket), "Decreases the camera FOV.\n ATTENTION: This Shortcut can only hold a single key!");
            SCfovMinus.SettingChanged += trimModifier;
            SCrollRight = Config.Bind("Camera Rotate Shortcuts", "Roll Right", new KeyboardShortcut(KeyCode.Period), "Rolls the camera right (rotate on Z).\n ATTENTION: This Shortcut can only hold a single key!");
            SCrollRight.SettingChanged += trimModifier;
            SCrollLeft = Config.Bind("Camera Rotate Shortcuts", "Roll Left", new KeyboardShortcut(KeyCode.Backslash), "Rolls the camera left (rotate on Z).\n ATTENTION: This Shortcut can only hold a single key!");
            SCrollLeft.SettingChanged += trimModifier;
            SCrotateRight = Config.Bind("Camera Rotate Shortcuts", "Rotate Right", new KeyboardShortcut(KeyCode.Keypad6), "Rotates the camera right.\n ATTENTION: This Shortcut can only hold a single key!");
            SCrotateRight.SettingChanged += trimModifier;
            SCrotateLeft = Config.Bind("Camera Rotate Shortcuts", "Rotate Left", new KeyboardShortcut(KeyCode.Keypad4), "Rotates the camera left.\n ATTENTION: This Shortcut can only hold a single key!");
            SCrotateLeft.SettingChanged += trimModifier;
            SCrotateUp = Config.Bind("Camera Rotate Shortcuts", "Rotate Up", new KeyboardShortcut(KeyCode.Keypad8), "Rotates the camera up.\n ATTENTION: This Shortcut can only hold a single key!");
            SCrotateUp.SettingChanged += trimModifier;
            SCrotateDown = Config.Bind("Camera Rotate Shortcuts", "Rotate Down", new KeyboardShortcut(KeyCode.Keypad2), "Rotates the camera down.\n ATTENTION: This Shortcut can only hold a single key!");
            SCrotateDown.SettingChanged += trimModifier;

            var harmony = new Harmony(GUID);
            if (modEnabled.Value)
                harmony.PatchAll();

        }

        private void trimModifier(object sender, EventArgs e)
        {
            ConfigEntry<KeyboardShortcut> entry = (ConfigEntry<KeyboardShortcut>)sender;
            entry.Value = new KeyboardShortcut(entry.Value.MainKey);
        }

        public static bool IsAlwaysTrue(int i)
        {
            return true;
        }

        public static bool IsTogglePlanes(int i)
        {
            return SCtogglePlanes.Value.IsDown();
        }
        public static bool IsRedo(int i)
        {
            return SCredo.Value.IsDown() || SCredo2.Value.IsDown();
        }
        public static bool IsUndo(int i)
        {
            return SCundo.Value.IsDown() || SCundo2.Value.IsDown();
        }
        public static bool IsFocusObject(int i)
        {
            return SCfocusObject.Value.IsDown();
        }
        public static bool IsMoveToCam(int i)
        {
            return SCmoveToCam.Value.IsDown();
        }
        public static bool IsSave(int i)
        {
            return SCsave.Value.IsDown();
        }
        public static bool IsDuplicate(int i)
        {
            return SCduplicate.Value.IsDown();
        }
        public static bool IsDelete(int i)
        {
            return SCdelete.Value.IsDown();
        }
        public static bool IsMove(int i)
        {
            return SCmove.Value.IsDown();
        }
        public static bool IsRotate(int i)
        {
            return SCrotate.Value.IsDown();
        }
        public static bool IsScale(int i)
        {
            return SCscale.Value.IsDown();
        }
        public static bool IsHideTransfrom(int i)
        {
            return SChideTransfrom.Value.IsDown();
        }
        public static bool IsToggleCenter(int i)
        {
            return SCtoggleCenter.Value.IsDown();
        }
        public static bool IsScreenshot(int i)
        {
            return SCscreenshot.Value.IsDown();
        }
        public static bool IsOpenShortcutPage(int i)
        {
            return SCopenShortcutPage.Value.IsDown();
        }
        public static bool IsExit(int i)
        {
            return SCexit.Value.IsDown();
        }
        public static bool IsSwitchCamera(int keycode)
        {
            switch (keycode)
            {
                case 49:
                    return XSCcam1.Value.IsDown();
                case 50:
                    return XSCcam2.Value.IsDown();
                case 51:
                    return XSCcam3.Value.IsDown();
                case 52:
                    return XSCcam4.Value.IsDown();
                case 53:
                    return XSCcam5.Value.IsDown();
                case 54:
                    return XSCcam6.Value.IsDown();
                case 55:
                    return XSCcam7.Value.IsDown();
                case 56:
                    return XSCcam8.Value.IsDown();
                case 57:
                    return XSCcam9.Value.IsDown();
                case 48:
                    return XSCcam0.Value.IsDown();
                default:
                    return false;
            }
        }
        public static bool IsNextCam(int i)
        {
            return XSCnextCam.Value.IsDown();
        }
        public static bool IsResetAll(int i)
        {
            return SCresetAll.Value.IsDown();
        }
        public static bool IsResetRot(int i)
        {
            return SCresetRot.Value.IsDown();
        }
        public static bool IsResetRoll(int i)
        {
            return SCresetRoll.Value.IsDown();
        }
        public static bool IsResetFOV(int i)
        {
            return SCresetFov.Value.IsDown();
        }
        public static bool IsZoomIn(int i)
        {
            return Input.GetKey(SCzoomIn.Value.MainKey);
        }
        public static bool IsZoomOut(int i)
        {
            return Input.GetKey(SCzoomOut.Value.MainKey);
        }
        public static bool IsMoveRight(int i)
        {
            return Input.GetKey(SCmoveRight.Value.MainKey);
        }
        public static bool IsMoveLeft(int i)
        {
            return Input.GetKey(SCmoveLeft.Value.MainKey);
        }
        public static bool IsMoveForward(int i)
        {
            return Input.GetKey(SCmoveForward.Value.MainKey);
        }
        public static bool IsMoveBackwards(int i)
        {
            return Input.GetKey(SCmoveBackwards.Value.MainKey);
        }
        public static bool IsMoveUp(int i)
        {
            return Input.GetKey(SCmoveUp.Value.MainKey);
        }
        public static bool IsMoveDown(int i)
        {
            return Input.GetKey(SCmoveDown.Value.MainKey);
        }
        public static bool IsRollRight(int i)
        {
            return Input.GetKey(SCrollRight.Value.MainKey);
        }
        public static bool IsRollLeft(int i)
        {
            return Input.GetKey(SCrollLeft.Value.MainKey);
        }
        public static bool IsRotateRight(int i)
        {
            return Input.GetKey(SCrotateRight.Value.MainKey);
        }
        public static bool IsRotateLeft(int i)
        {
            return Input.GetKey(SCrotateLeft.Value.MainKey);
        }
        public static bool IsRotateUp(int i)
        {
            return Input.GetKey(SCrotateUp.Value.MainKey);
        }
        public static bool IsRotateDown(int i)
        {
            return Input.GetKey(SCrotateDown.Value.MainKey);
        }
        public static bool IsFovMinus(int i)
        {
            return Input.GetKey(SCfovMinus.Value.MainKey);
        }
        public static bool IsFovPlus(int i)
        {
            return Input.GetKey(SCfovPlus.Value.MainKey);
        }
        public static bool IsSnap(int i)
        {
            return SCsnap.Value.IsPressed();
        }
        public static bool IsAttach(int i)
        {
            return SCattach.Value.IsPressed();
        }
        public static bool IsDetach(int i)
        {
            return SCdetach.Value.IsPressed();
        }
        public static bool IsCopy(int i)
        {
            return SCcopy.Value.IsPressed();
        }
    }
}
