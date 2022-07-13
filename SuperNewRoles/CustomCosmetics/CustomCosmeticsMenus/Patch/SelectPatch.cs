using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using static SuperNewRoles.CustomCosmetics.CustomCosmeticsMenus.Patch.ObjectData;

namespace SuperNewRoles.CustomCosmetics.CustomCosmeticsMenus.Patch
{
    class SelectPatch
    {
        [HarmonyPatch(typeof(SaveManager), nameof(SaveManager.BodyColor), MethodType.Setter)]
        public static class SelectColor
        {
            public static void Postfix(ref byte value)
            {
                if (GetData().BodyColor.Value != value)
                {
                    GetData().BodyColor.Value = value;
                }
            }
        }
        [HarmonyPatch(typeof(SaveManager), nameof(SaveManager.LastVisor), MethodType.Setter)]
        public static class SelectVisor
        {
            public static void Postfix(ref string value)
            {
                if (GetData().Visor.Value != value)
                {
                    GetData().Visor.Value = value;
                }
            }
        }
        [HarmonyPatch(typeof(SaveManager), nameof(SaveManager.LastHat), MethodType.Setter)]
        public static class SelectHat
        {
            public static void Postfix(ref string value)
            {
                if (GetData().Hat.Value != value)
                {
                    GetData().Hat.Value = value;
                }
            }
        }
        [HarmonyPatch(typeof(SaveManager), nameof(SaveManager.LastSkin), MethodType.Setter)]
        public static class SelectSkin
        {
            public static void Postfix(ref string value)
            {
                if (GetData().Skin.Value != value)
                {
                    GetData().Skin.Value = value;
                }
            }
        }
        [HarmonyPatch(typeof(SaveManager), nameof(SaveManager.LastNamePlate), MethodType.Setter)]
        public static class SelectNamePlate
        {
            public static void Postfix(ref string value)
            {
                if (GetData().NamePlate.Value != value)
                {
                    GetData().NamePlate.Value = value;
                }
            }
        }
        [HarmonyPatch(typeof(SaveManager), nameof(SaveManager.LastPet), MethodType.Setter)]
        public static class SelectPet
        {
            public static void Postfix(ref string value)
            {
                if (GetData().Pet.Value != value)
                {
                    GetData().Pet.Value = value;
                }
            }
        }
        public static ClosetPresetData GetData(int index = -1)
        {
            if (index == -1) index = SelectedPreset.Value;
            ClosetPresetData data = null;
            if (!ClosetPresetDatas.ContainsKey(index))
            {
                data = new();
                data.BodyColor = SuperNewRolesPlugin.Instance.Config.Bind("ClosetPreset_" + index.ToString(), "BodyColor", (byte)0);
                data.Hat = SuperNewRolesPlugin.Instance.Config.Bind("ClosetPreset_" + index.ToString(), "Hat", "");
                data.Visor = SuperNewRolesPlugin.Instance.Config.Bind("ClosetPreset_" + index.ToString(), "Visor", "");
                data.Skin = SuperNewRolesPlugin.Instance.Config.Bind("ClosetPreset_" + index.ToString(), "Skin", "");
                data.NamePlate = SuperNewRolesPlugin.Instance.Config.Bind("ClosetPreset_" + index.ToString(), "NamePlate", "");
                data.Pet = SuperNewRolesPlugin.Instance.Config.Bind("ClosetPreset_" + index.ToString(), "Pet", "");
            }
            else
            {
                data = ClosetPresetDatas[index];
            }
            return data;
        }
    }
}
