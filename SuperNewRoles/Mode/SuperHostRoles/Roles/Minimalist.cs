using HarmonyLib;
using SuperNewRoles.Roles;
using SuperNewRoles.CustomRPC;

namespace SuperNewRoles.Mode.SuperHostRoles.Roles
{
    class Minimalist
    {
        [HarmonyPatch(typeof(HudManager), nameof(HudManager.SetHudActive))]
        class SetHudActivePatch
        {
            public static void Postfix(HudManager __instance, [HarmonyArgument(0)] bool isActive)
            {
                if (!AmongUsClient.Instance.AmHost) return;
                if (PlayerControl.LocalPlayer.isRole(RoleId.Minimalist))
                {
                    __instance.ReportButton.ToggleVisible(visible: RoleClass.Minimalist.UseReport);
                    __instance.SabotageButton.ToggleVisible(visible: RoleClass.Minimalist.UseSabo);
                    __instance.ImpostorVentButton.ToggleVisible(visible: RoleClass.Minimalist.UseVent);
                }
            }
        }
    }
}
