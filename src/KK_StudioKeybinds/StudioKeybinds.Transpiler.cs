using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using Studio;
using UnityEngine;

namespace StudioKeybinds
{
    [HarmonyPatch(typeof(ShortcutKeyCtrl), "Update")]
    class MainShortcutPatch
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var code = new List<CodeInstruction>(instructions);

            for (int i = 0; i < code.Count -1; i++)
            {

                if (code[i].opcode == OpCodes.Ldc_I4_S && (sbyte)code[i].operand == (sbyte)122 && code[i - 1].opcode == OpCodes.Ldloc_1)
                {
                    MethodInfo info = AccessTools.Method(typeof(StudioKeybinds), nameof(StudioKeybinds.IsRedo), new Type[] { typeof(int) });
                    code[i + 1] = new CodeInstruction(OpCodes.Call, info);
                    code.RemoveAt(i + 2);
                    code.RemoveAt(i - 1);
                }
                if (code[i].opcode == OpCodes.Ldc_I4_S && (sbyte)code[i].operand == (sbyte)122 && code[i - 1].opcode != OpCodes.Ldloc_1)
                {
                    MethodInfo info = AccessTools.Method(typeof(StudioKeybinds), nameof(StudioKeybinds.IsUndo), new Type[] { typeof(int) });
                    code[i + 1] = new CodeInstruction(OpCodes.Call, info);
                }
                if (code[i].opcode == OpCodes.Ldc_I4_S && (sbyte)code[i].operand == (sbyte)102)
                {
                    MethodInfo info = AccessTools.Method(typeof(StudioKeybinds), nameof(StudioKeybinds.IsFocusObject), new Type[]{ typeof(int)});
                    code[i+1] = new CodeInstruction(OpCodes.Call, info);
                }
                if (code[i].opcode == OpCodes.Ldc_I4_S && (sbyte)code[i].operand == (sbyte)99)
                {
                    MethodInfo info = AccessTools.Method(typeof(StudioKeybinds), nameof(StudioKeybinds.IsMoveToCam), new Type[]{ typeof(int)});
                    code[i+1] = new CodeInstruction(OpCodes.Call, info);
                }
                if (code[i].opcode == OpCodes.Ldc_I4_S && (sbyte)code[i].operand == (sbyte)115)
                {
                    MethodInfo info = AccessTools.Method(typeof(StudioKeybinds), nameof(StudioKeybinds.IsSave), new Type[]{ typeof(int)});
                    code[i+1] = new CodeInstruction(OpCodes.Call, info);
                    code.RemoveAt(i - 1);
                    code.RemoveAt(i - 1);
                }
                if (code[i].opcode == OpCodes.Ldc_I4_S && (sbyte)code[i].operand == (sbyte)100)
                {
                    MethodInfo info = AccessTools.Method(typeof(StudioKeybinds), nameof(StudioKeybinds.IsDuplicate), new Type[]{ typeof(int)});
                    code[i+1] = new CodeInstruction(OpCodes.Call, info);
                    code.RemoveAt(i - 1);
                    code.RemoveAt(i - 1);
                }
                if (code[i].opcode == OpCodes.Ldc_I4_S && (sbyte)code[i].operand == (sbyte)127)
                {
                    MethodInfo info = AccessTools.Method(typeof(StudioKeybinds), nameof(StudioKeybinds.IsDelete), new Type[]{ typeof(int)});
                    code[i+1] = new CodeInstruction(OpCodes.Call, info);
                }
                if (code[i].opcode == OpCodes.Ldc_I4_S && (sbyte)code[i].operand == (sbyte)119)
                {
                    MethodInfo info = AccessTools.Method(typeof(StudioKeybinds), nameof(StudioKeybinds.IsMove), new Type[] { typeof(int) });
                    code[i + 1] = new CodeInstruction(OpCodes.Call, info);
                }
                if (code[i].opcode == OpCodes.Ldc_I4_S && (sbyte)code[i].operand == (sbyte)101)
                {
                    MethodInfo info = AccessTools.Method(typeof(StudioKeybinds), nameof(StudioKeybinds.IsRotate), new Type[]{ typeof(int)});
                    code[i+1] = new CodeInstruction(OpCodes.Call, info);
                }
                if (code[i].opcode == OpCodes.Ldc_I4_S && (sbyte)code[i].operand == (sbyte)114)
                {
                    MethodInfo info = AccessTools.Method(typeof(StudioKeybinds), nameof(StudioKeybinds.IsScale), new Type[]{ typeof(int)});
                    code[i+1] = new CodeInstruction(OpCodes.Call, info);
                }
                if (code[i].opcode == OpCodes.Ldc_I4_S && (sbyte)code[i].operand == (sbyte)113)
                {
                    MethodInfo info = AccessTools.Method(typeof(StudioKeybinds), nameof(StudioKeybinds.IsHideTransfrom), new Type[]{ typeof(int)});
                    code[i+1] = new CodeInstruction(OpCodes.Call, info);
                }
                if (code[i].opcode == OpCodes.Ldc_I4_S && (sbyte)code[i].operand == (sbyte)106)
                {
                    MethodInfo info = AccessTools.Method(typeof(StudioKeybinds), nameof(StudioKeybinds.IsTogglePlanes), new Type[] { typeof(int) });
                    code[i + 1] = new CodeInstruction(OpCodes.Call, info);
                }
                if (code[i].opcode == OpCodes.Ldc_I4_S && (sbyte)code[i].operand == (sbyte)107)
                {
                    MethodInfo info = AccessTools.Method(typeof(StudioKeybinds), nameof(StudioKeybinds.IsToggleCenter), new Type[] { typeof(int) });
                    code[i + 1] = new CodeInstruction(OpCodes.Call, info);
                }
                if (code[i].opcode == OpCodes.Ldc_I4 && (int)code[i].operand == 292)
                {
                    MethodInfo info = AccessTools.Method(typeof(StudioKeybinds), nameof(StudioKeybinds.IsScreenshot), new Type[]{ typeof(int)});
                    code[i+1] = new CodeInstruction(OpCodes.Call, info);
                }
                if (code[i].opcode == OpCodes.Ldc_I4 && (int)code[i].operand == 283)
                {
                    MethodInfo info = AccessTools.Method(typeof(StudioKeybinds), nameof(StudioKeybinds.IsOpenShortcutPage), new Type[]{ typeof(int)});
                    code[i+1] = new CodeInstruction(OpCodes.Call, info);
                }
                if (code[i].opcode == OpCodes.Ldc_I4_S && (sbyte)code[i].operand == (sbyte)27)
                {
                    MethodInfo info = AccessTools.Method(typeof(StudioKeybinds), nameof(StudioKeybinds.IsExit), new Type[]{ typeof(int)});
                    code[i+1] = new CodeInstruction(OpCodes.Call, info);
                }

                //here would be the bool changer for the GameEnd method, but in KK it's correctly set to true by default

                MethodInfo getKeyDownInfo = AccessTools.Method(typeof(Input), nameof(Input.GetKeyDown), new Type[] { typeof(KeyCode) });
                if (code[i].opcode == OpCodes.Ldelem_I4 && code[i + 1].opcode == OpCodes.Call && (MethodInfo)code[i + 1].operand == getKeyDownInfo)
                {
                    MethodInfo info = AccessTools.Method(typeof(StudioKeybinds), nameof(StudioKeybinds.IsSwitchCamera), new Type[] { typeof(int) });
                    code[i + 1] = new CodeInstruction(OpCodes.Call, info);
                }

                if (code[i].opcode == OpCodes.Ldc_I4_S && (sbyte)code[i].operand == (sbyte)104)
                {
                    MethodInfo info = AccessTools.Method(typeof(StudioKeybinds), nameof(StudioKeybinds.IsNextCam), new Type[] { typeof(int) });
                    code[i + 1] = new CodeInstruction(OpCodes.Call, info);
                }

            }

            StudioKeybinds.Logger.LogInfo("patched ShortcutKeyCtrl.Update");
            return code.AsEnumerable();
        }
        
    }

    [HarmonyPatch(typeof(Studio.CameraControl))]
    [HarmonyPatch("InputKeyProc")]
    class CameraShortcutPatch
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var code = new List<CodeInstruction>(instructions);

            for (int i = 0; i < code.Count - 1; i++)
            {

                MethodInfo getKeyDownInfo = AccessTools.Method(typeof(Input), nameof(Input.GetKeyDown), new Type[] { typeof(KeyCode) });
                if (code[i].opcode == OpCodes.Ldc_I4_S && (sbyte)code[i].operand == (sbyte)97 && code[i+1].opcode == OpCodes.Call && (MethodInfo)code[i+1].operand == getKeyDownInfo)
                {
                    MethodInfo info = AccessTools.Method(typeof(StudioKeybinds), nameof(StudioKeybinds.IsResetAll), new Type[] { typeof(int) });
                    code[i + 1] = new CodeInstruction(OpCodes.Call, info);
                }
                if (code[i].opcode == OpCodes.Ldc_I4 && (int)code[i].operand == 261)
                {
                    MethodInfo info = AccessTools.Method(typeof(StudioKeybinds), nameof(StudioKeybinds.IsResetRot), new Type[] { typeof(int) });
                    code[i + 1] = new CodeInstruction(OpCodes.Call, info);
                }
                if (code[i].opcode == OpCodes.Ldc_I4_S && (sbyte)code[i].operand == (sbyte)47)
                {
                    MethodInfo info = AccessTools.Method(typeof(StudioKeybinds), nameof(StudioKeybinds.IsResetRoll), new Type[] { typeof(int) });
                    code[i + 1] = new CodeInstruction(OpCodes.Call, info);
                }
                if (code[i].opcode == OpCodes.Ldc_I4_S && (sbyte)code[i].operand == (sbyte)59)
                {
                    MethodInfo info = AccessTools.Method(typeof(StudioKeybinds), nameof(StudioKeybinds.IsResetFOV), new Type[] { typeof(int) });
                    code[i + 1] = new CodeInstruction(OpCodes.Call, info);
                }
                if (code[i].opcode == OpCodes.Ldc_I4 && (int)code[i].operand == 278)
                {
                    MethodInfo info = AccessTools.Method(typeof(StudioKeybinds), nameof(StudioKeybinds.IsZoomIn), new Type[] { typeof(int) });
                    code[i + 1] = new CodeInstruction(OpCodes.Call, info);
                }
                if (code[i].opcode == OpCodes.Ldc_I4 && (int)code[i].operand == 279)
                {
                    MethodInfo info = AccessTools.Method(typeof(StudioKeybinds), nameof(StudioKeybinds.IsZoomOut), new Type[] { typeof(int) });
                    code[i + 1] = new CodeInstruction(OpCodes.Call, info);
                }
                if (code[i].opcode == OpCodes.Ldc_I4 && (int)code[i].operand == 275)
                {
                    MethodInfo info = AccessTools.Method(typeof(StudioKeybinds), nameof(StudioKeybinds.IsMoveRight), new Type[] { typeof(int) });
                    code[i + 1] = new CodeInstruction(OpCodes.Call, info);
                }
                if (code[i].opcode == OpCodes.Ldc_I4 && (int)code[i].operand == 276)
                {
                    MethodInfo info = AccessTools.Method(typeof(StudioKeybinds), nameof(StudioKeybinds.IsMoveLeft), new Type[] { typeof(int) });
                    code[i + 1] = new CodeInstruction(OpCodes.Call, info);
                }
                if (code[i].opcode == OpCodes.Ldc_I4 && (int)code[i].operand == 273)
                {
                    MethodInfo info = AccessTools.Method(typeof(StudioKeybinds), nameof(StudioKeybinds.IsMoveForward), new Type[] { typeof(int) });
                    code[i + 1] = new CodeInstruction(OpCodes.Call, info);
                }
                if (code[i].opcode == OpCodes.Ldc_I4 && (int)code[i].operand == 274)
                {
                    MethodInfo info = AccessTools.Method(typeof(StudioKeybinds), nameof(StudioKeybinds.IsMoveBackwards), new Type[] { typeof(int) });
                    code[i + 1] = new CodeInstruction(OpCodes.Call, info);
                }
                if (code[i].opcode == OpCodes.Ldc_I4 && (int)code[i].operand == 280)
                {
                    MethodInfo info = AccessTools.Method(typeof(StudioKeybinds), nameof(StudioKeybinds.IsMoveUp), new Type[] { typeof(int) });
                    code[i + 1] = new CodeInstruction(OpCodes.Call, info);
                }
                if (code[i].opcode == OpCodes.Ldc_I4 && (int)code[i].operand == 281)
                {
                    MethodInfo info = AccessTools.Method(typeof(StudioKeybinds), nameof(StudioKeybinds.IsMoveDown), new Type[] { typeof(int) });
                    code[i + 1] = new CodeInstruction(OpCodes.Call, info);
                }
                if (code[i].opcode == OpCodes.Ldc_I4_S && (sbyte)code[i].operand == (sbyte)46)
                {
                    MethodInfo info = AccessTools.Method(typeof(StudioKeybinds), nameof(StudioKeybinds.IsRollRight), new Type[] { typeof(int) });
                    code[i + 1] = new CodeInstruction(OpCodes.Call, info);
                }
                if (code[i].opcode == OpCodes.Ldc_I4_S && (sbyte)code[i].operand == (sbyte)92)
                {
                    MethodInfo info = AccessTools.Method(typeof(StudioKeybinds), nameof(StudioKeybinds.IsRollLeft), new Type[] { typeof(int) });
                    code[i + 1] = new CodeInstruction(OpCodes.Call, info);
                }
                if (code[i].opcode == OpCodes.Ldc_I4 && (int)code[i].operand == 258)
                {
                    MethodInfo info = AccessTools.Method(typeof(StudioKeybinds), nameof(StudioKeybinds.IsRotateDown), new Type[] { typeof(int) });
                    code[i + 1] = new CodeInstruction(OpCodes.Call, info);
                }
                if (code[i].opcode == OpCodes.Ldc_I4 && (int)code[i].operand == 264)
                {
                    MethodInfo info = AccessTools.Method(typeof(StudioKeybinds), nameof(StudioKeybinds.IsRotateUp), new Type[] { typeof(int) });
                    code[i + 1] = new CodeInstruction(OpCodes.Call, info);
                }
                if (code[i].opcode == OpCodes.Ldc_I4 && (int)code[i].operand == 260)
                {
                    MethodInfo info = AccessTools.Method(typeof(StudioKeybinds), nameof(StudioKeybinds.IsRotateLeft), new Type[] { typeof(int) });
                    code[i + 1] = new CodeInstruction(OpCodes.Call, info);
                }
                if (code[i].opcode == OpCodes.Ldc_I4 && (int)code[i].operand == 262)
                {
                    MethodInfo info = AccessTools.Method(typeof(StudioKeybinds), nameof(StudioKeybinds.IsRotateRight), new Type[] { typeof(int) });
                    code[i + 1] = new CodeInstruction(OpCodes.Call, info);
                }
                if (code[i].opcode == OpCodes.Ldc_I4_S && (sbyte)code[i].operand == (sbyte)61)
                {
                    MethodInfo info = AccessTools.Method(typeof(StudioKeybinds), nameof(StudioKeybinds.IsFovMinus), new Type[] { typeof(int) });
                    code[i + 1] = new CodeInstruction(OpCodes.Call, info);
                }
                if (code[i].opcode == OpCodes.Ldc_I4_S && (sbyte)code[i].operand == (sbyte)93)
                {
                    MethodInfo info = AccessTools.Method(typeof(StudioKeybinds), nameof(StudioKeybinds.IsFovPlus), new Type[] { typeof(int) });
                    code[i + 1] = new CodeInstruction(OpCodes.Call, info);
                }

            }


            StudioKeybinds.Logger.LogInfo("patched CameraControl.InputKeyProc");
            return code.AsEnumerable();
        }
    }

    [HarmonyPatch(typeof(Studio.GuideMove), "AxisMove")]
    class GuideMovePatch
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var code = new List<CodeInstruction>(instructions);
            for (int i = 0; i < code.Count - 1; i++)
            {
                if (code[i].opcode == OpCodes.Ldc_I4_S && (sbyte)code[i].operand == (sbyte)118)
                {
                    MethodInfo info = AccessTools.Method(typeof(StudioKeybinds), nameof(StudioKeybinds.IsSnap), new Type[] { typeof(int) });
                    code[i + 1] = new CodeInstruction(OpCodes.Call, info);
                }
            }


            StudioKeybinds.Logger.LogInfo("patched GuideMove.AxisMove");
            return code.AsEnumerable();
        }
    }


    [HarmonyPatch(typeof(Studio.TreeNodeCtrl), "SetSelectNode")]
    class TreeNodePatch
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var code = new List<CodeInstruction>(instructions);
            for (int i = 0; i < code.Count - 1; i++)
            {
                if (code[i].opcode == OpCodes.Ldc_I4_S && (sbyte)code[i].operand == (sbyte)112)
                {
                    MethodInfo info = AccessTools.Method(typeof(StudioKeybinds), nameof(StudioKeybinds.IsAttach), new Type[] { typeof(int) });
                    code[i + 1] = new CodeInstruction(OpCodes.Call, info);
                }
                if (code[i].opcode == OpCodes.Ldc_I4_S && (sbyte)code[i].operand == (sbyte)111)
                {
                    MethodInfo info = AccessTools.Method(typeof(StudioKeybinds), nameof(StudioKeybinds.IsDetach), new Type[] { typeof(int) });
                    code[i + 1] = new CodeInstruction(OpCodes.Call, info);
                }
                if (code[i].opcode == OpCodes.Ldc_I4_S && (sbyte)code[i].operand == (sbyte)120)
                {
                    MethodInfo info = AccessTools.Method(typeof(StudioKeybinds), nameof(StudioKeybinds.IsCopy), new Type[] { typeof(int) });
                    code[i + 1] = new CodeInstruction(OpCodes.Call, info);
                }
            }


            StudioKeybinds.Logger.LogInfo("patched TreeNodeCtrl.SetSelectNode");
            return code.AsEnumerable();
        }
    }
}
