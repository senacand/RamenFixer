using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using System.Reflection;
using IPA.Loader;
using UnityEngine;

namespace RamenFixer.HarmonyPatches
{
    [HarmonyPatch]
    class VMCAvatarMovePatch
    {
        private static MethodBase TargetMethod()
        {
            PluginMetadata vmc = PluginManager.GetPluginFromId("VMCAvatar");
            if (vmc != null)
            {
                return vmc.Assembly.GetType("VMCAvatar.Plugin").GetMethod("OnAvatarMove", BindingFlags.Instance | BindingFlags.NonPublic);
            }
            else
            {
                return null;
            }
        }
        static void Prefix(ref Vector3 rootPosition, ref Quaternion rootRotation, ref Vector3[] bonePositions, ref Quaternion[] boneRotations)
        {
            RamenLogic.Instance.OnAvatarMove(ref rootPosition, ref rootRotation, ref bonePositions, ref boneRotations);
        }
    }

    [HarmonyPatch]
    class VMCAvatarScalePatch
    {
        private static MethodBase TargetMethod()
        {
            PluginMetadata vmc = PluginManager.GetPluginFromId("VMCAvatar");
            if (vmc != null)
            {
                return vmc.Assembly.GetType("VMCAvatar.Plugin").GetMethod("OnAvatarScaleChanged", BindingFlags.Instance | BindingFlags.NonPublic);
            }
            else
            {
                return null;
            }
        }
        static void Postfix(ref Vector3 scale, ref Vector3 offset)
        {
            RamenLogic.Instance.AvatarScale = scale;
        }
    }
}
