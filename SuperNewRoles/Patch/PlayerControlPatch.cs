﻿using HarmonyLib;
using Hazel;
using SuperNewRoles.CustomOption;
using SuperNewRoles.CustomRPC;
using SuperNewRoles.EndGame;
using SuperNewRoles.Mode;
using SuperNewRoles.Mode.SuperHostRoles;
using static SuperNewRoles.Helpers.DesyncHelpers;
using SuperNewRoles.Patch;
using SuperNewRoles.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SuperNewRoles.Helpers;
using static SuperNewRoles.ModHelpers;
using InnerNet;

namespace SuperNewRoles.Patches
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.Shapeshift))]
    class RpcShapesihftPatch
    {
        public static bool Prefix(PlayerControl __instance, [HarmonyArgument(0)] PlayerControl target, [HarmonyArgument(1)] bool shouldAnimate)
        {
            SyncSetting.CustomSyncSettings();
            if (target.IsBot()) return true;
            if (__instance.PlayerId == target.PlayerId)
            {
                if (__instance.isRole(RoleId.Camouflager) && RoleClass.Camouflager.CamouflageTimer > 0 && RoleClass.Camouflager.Started == __instance.PlayerId && !RoleClass.IsMeeting)
                {
                    PlayerControl bot = null;
                    foreach (PlayerControl p in BotManager.AllBots)
                    {
                        if (!p.isImpostor())
                        {
                            bot = p;
                        }
                    }
                    if (bot == null) return true;
                    new LateTask(() =>
                    {
                        __instance.RpcShapeshift(bot, true);
                    }, 0.1f);
                } else if (ModeHandler.isMode(ModeId.SuperHostRoles) && AmongUsClient.Instance.AmHost)
                {
                    if (__instance.isRole(RoleId.RemoteSheriff))
                    {
                        __instance.RpcProtectPlayer(__instance, 0);
                        new LateTask(() =>
                        {
                            __instance.RpcMurderPlayer(__instance);
                        }, 0.5f);
                    }
                }
                return true;
            }
            if (ModeHandler.isMode(ModeId.SuperHostRoles))
            {
                switch (__instance.getRole())
                {
                    case RoleId.Camouflager:
                        if (AmongUsClient.Instance.AmHost)
                        {
                            if (RoleClass.Camouflager.Started != __instance.PlayerId)
                            {
                                RoleClass.Camouflager.Started = __instance.PlayerId;

                                PlayerControl bot = null;
                                foreach (PlayerControl p in BotManager.AllBots)
                                {
                                    if (!p.isImpostor())
                                    {
                                        bot = p;
                                    }
                                }
                                if (bot == null) return true;

                                new LateTask(() =>
                                {
                                    foreach (PlayerControl p in PlayerControl.AllPlayerControls)
                                    {
                                        SuperNewRolesPlugin.Logger.LogInfo(p.PlayerId + ":" + p.name);
                                        p.RpcShapeshift(bot, __instance.PlayerId == p.PlayerId);
                                    }
                                }, 0f);
                                RoleClass.Camouflager.CamouflageTimer = RoleClass.Camouflager.DurationTime;
                            }
                        }
                        return true;
                    case RoleId.RemoteSheriff:
                        if (AmongUsClient.Instance.AmHost)
                        {
                            if (target.isDead()) return true;
                            if (!RoleClass.RemoteSheriff.KillCount.ContainsKey(__instance.PlayerId) || RoleClass.RemoteSheriff.KillCount[__instance.PlayerId] >= 1)
                            {
                                if (!Sheriff.IsRemoteSheriffKill(target) || target.isRole(RoleId.RemoteSheriff))
                                {
                                    FinalStatusPatch.FinalStatusData.FinalStatuses[__instance.PlayerId] = FinalStatus.SheriffMisFire;
                                    __instance.RpcMurderPlayer(__instance);
                                    return true;
                                }
                                else
                                {
                                    FinalStatusPatch.FinalStatusData.FinalStatuses[target.PlayerId] = FinalStatus.SheriffKill;
                                    if (RoleClass.RemoteSheriff.KillCount.ContainsKey(__instance.PlayerId))
                                    {
                                        RoleClass.RemoteSheriff.KillCount[__instance.PlayerId]--;
                                    }
                                    else
                                    {
                                        RoleClass.RemoteSheriff.KillCount[__instance.PlayerId] = (int)CustomOptions.RemoteSheriffKillMaxCount.getFloat() - 1;
                                    }
                                    if (RoleClass.RemoteSheriff.IsKillTeleport)
                                    {
                                        __instance.RpcMurderPlayer(target);
                                    }
                                    else
                                    {
                                        target.RpcMurderPlayer(target);
                                        __instance.RpcProtectPlayer(__instance, 0);
                                        new LateTask(() =>
                                        {
                                            __instance.RpcMurderPlayer(__instance);
                                        }, 0.5f);
                                    }
                                    return true;
                                }
                            }
                            else
                            {
                                return true;
                            }
                        }
                        return true;
                    case RoleId.SelfBomber:
                        if (AmongUsClient.Instance.AmHost)
                        {
                            foreach (PlayerControl p in PlayerControl.AllPlayerControls)
                            {
                                if (p.isAlive() && p.PlayerId != __instance.PlayerId)
                                {
                                    if (SelfBomber.GetIsBomb(__instance, p))
                                    {
                                        __instance.RpcMurderPlayer(p);
                                    }
                                }
                            }
                            __instance.RpcMurderPlayer(__instance);
                        }
                        return false;
                }
            }
            return true;
        }
    }
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.CheckProtect))]
    class CheckProtectPatch
    {
        public static bool Prefix(PlayerControl __instance,[HarmonyArgument(0)] PlayerControl target)
        {
            if (ModeHandler.isMode(ModeId.SuperHostRoles)) return false;
            return true;
        }
    }

    [HarmonyPatch(typeof(ShapeshifterMinigame), nameof(ShapeshifterMinigame.Shapeshift))]
    class ShapeshifterMinigameShapeshiftPatch
    {
        public static bool Prefix(ShapeshifterMinigame __instance, [HarmonyArgument(0)] PlayerControl player)
        {
            if (player.IsBot()) return false;
            if (PlayerControl.LocalPlayer.inVent)
            {
                __instance.Close();
                return false;
            }
            if (PlayerControl.LocalPlayer.isRole(RoleId.RemoteSheriff)){
                if (RoleClass.RemoteSheriff.KillMaxCount > 0)
                {
                    if (ModeHandler.isMode(ModeId.SuperHostRoles))
                    {
                        new LateTask(() =>
                        {
                            PlayerControl.LocalPlayer.RpcRevertShapeshift(true);
                            new LateTask(() =>
                            {
                                PlayerControl.LocalPlayer.transform.localScale *= 1.4f;
                            }, 1.1f);
                        }, 1.5f);
                        PlayerControl.LocalPlayer.RpcShapeshift(player, true);
                    } else if (ModeHandler.isMode(ModeId.Default))
                    {
                        if (player.isAlive())
                        {
                            var Target = player;
                            var misfire = !Roles.Sheriff.IsRemoteSheriffKill(Target);
                            var TargetID = Target.PlayerId;
                            var LocalID = PlayerControl.LocalPlayer.PlayerId;

                            PlayerControl.LocalPlayer.RpcShapeshift(PlayerControl.LocalPlayer, true);
                            new LateTask(() =>
                            {
                                PlayerControl.LocalPlayer.transform.localScale *= 1.4f;
                            }, 1.1f);

                            CustomRPC.RPCProcedure.SheriffKill(LocalID, TargetID, misfire);

                            MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CustomRPC.SheriffKill, Hazel.SendOption.Reliable, -1);
                            killWriter.Write(LocalID);
                            killWriter.Write(TargetID);
                            killWriter.Write(misfire);
                            AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                            RoleClass.RemoteSheriff.KillMaxCount--;
                        }
                        Sheriff.ResetKillCoolDown();
                    };
                } 
                __instance.Close();
                return false;
            }
            PlayerControl.LocalPlayer.RpcShapeshift(player,true);
            __instance.Close();
            return false;
            
        }
    }
    [HarmonyPatch(typeof(KillButton), nameof(KillButton.DoClick))]
    class KillButtonDoClickPatch
    {
        public static bool Prefix(KillButton __instance)
        {
            if (!ModeHandler.isMode(ModeId.Default)) {
                if (ModeHandler.isMode(ModeId.SuperHostRoles))
                {
                    if (PlayerControl.LocalPlayer.isRole(RoleId.RemoteSheriff))
                    {
                        if (__instance.isActiveAndEnabled && PlayerControl.LocalPlayer.isAlive() && PlayerControl.LocalPlayer.CanMove && !__instance.isCoolingDown && RoleClass.RemoteSheriff.KillMaxCount > 0)
                        {
                            DestroyableSingleton<RoleManager>.Instance.SetRole(PlayerControl.LocalPlayer, RoleTypes.Shapeshifter);
                            foreach (PlayerControl p in PlayerControl.AllPlayerControls)
                            {
                                p.Data.Role.NameColor = Color.white;
                            }
                            PlayerControl.LocalPlayer.Data.Role.TryCast<ShapeshifterRole>().UseAbility();
                            foreach (PlayerControl p in PlayerControl.AllPlayerControls)
                            {
                                if (p.isImpostor())
                                {
                                    p.Data.Role.NameColor = RoleClass.ImpostorRed;
                                }
                            }
                            DestroyableSingleton<RoleManager>.Instance.SetRole(PlayerControl.LocalPlayer, RoleTypes.Crewmate);
                            PlayerControl.LocalPlayer.killTimer = 0.001f;
                        }
                        return false;
                    }
                }
                return true;
            }
            if (__instance.isActiveAndEnabled && __instance.currentTarget && !__instance.isCoolingDown && PlayerControl.LocalPlayer.isAlive() && PlayerControl.LocalPlayer.CanMove)
            {
                if (!(__instance.currentTarget.isRole(CustomRPC.RoleId.Bait) || __instance.currentTarget.isRole(CustomRPC.RoleId.NiceRedRidingHood)) && PlayerControl.LocalPlayer.isRole(CustomRPC.RoleId.Vampire))
                {
                    PlayerControl.LocalPlayer.killTimer = RoleHelpers.getCoolTime(PlayerControl.LocalPlayer);
                    RoleClass.Vampire.target = __instance.currentTarget;
                    RoleClass.Vampire.KillTimer = DateTime.Now;
                    RoleClass.Vampire.Timer = RoleClass.Vampire.KillDelay;
                    return false;
                }
                bool showAnimation = true;
                /*
                if (PlayerControl.LocalPlayer.isRole(RoleType.Ninja) && Ninja.isStealthed(PlayerControl.LocalPlayer))
                {
                    showAnimation = false;
                }
                */

                // Use an unchecked kill command, to allow shorter kill cooldowns etc. without getting kicked
                MurderAttemptResult res = ModHelpers.checkMuderAttemptAndKill(PlayerControl.LocalPlayer, __instance.currentTarget, showAnimation: showAnimation);
                // Handle blank kill
                if (res == MurderAttemptResult.BlankKill)
                {
                    PlayerControl.LocalPlayer.killTimer = RoleHelpers.getCoolTime(PlayerControl.LocalPlayer);
                }
                __instance.SetTarget(null);
            }
            return false;
        }
    }
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.CheckMurder))]
    class CheckMurderPatch
    {
        public static bool isKill = false;
        public static bool Prefix(PlayerControl __instance, [HarmonyArgument(0)] PlayerControl target)
        {
            SuperNewRolesPlugin.Logger.LogInfo("キル:" + __instance.name + "(" + __instance.PlayerId + ")" + " => " + target.name + "(" + target.PlayerId + ")");
            if (__instance.IsBot() || target.IsBot()) return false;

            if (__instance.isDead()) return false;
            if (__instance.PlayerId == target.PlayerId) { __instance.RpcMurderPlayer(target); return false; }
            if (!RoleClass.IsStart && AmongUsClient.Instance.GameMode != GameModes.FreePlay)
                return false;
            if (!AmongUsClient.Instance.AmHost)
            {
                return true;
            }
            SyncSetting.CustomSyncSettings();
            if (ModeHandler.isMode(ModeId.BattleRoyal))
            {
                if (isKill)
                {
                    return false;
                }
                if (Mode.BattleRoyal.main.StartSeconds <= 0)
                {
                    SuperNewRolesPlugin.Logger.LogInfo("キルでした:" + __instance.name + "(" + __instance.PlayerId + ")" + " => " + target.name + "(" + target.PlayerId + ")");
                    if (Mode.BattleRoyal.main.IsTeamBattle)
                    {
                        foreach (List<PlayerControl> teams in Mode.BattleRoyal.main.Teams)
                        {
                            if (teams.Count > 0)
                            {
                                if (teams.IsCheckListPlayerControl(__instance) && teams.IsCheckListPlayerControl(target))
                                {
                                    return false;
                                }
                            }
                        }
                    }
                    if (__instance.PlayerId != 0)
                    {
                        if (__instance.isAlive() && target.isAlive())
                        {
                            __instance.RpcMurderPlayer(target);
                            target.Data.IsDead = true;
                        }
                    } else
                    {
                        SuperNewRolesPlugin.Logger.LogInfo("レートタスク:"+ (AmongUsClient.Instance.Ping / 1000f) * 2f);
                        isKill = true;
                        new LateTask(() => {
                            if (__instance.isAlive() && target.isAlive())
                            {
                                __instance.RpcMurderPlayer(target);
                            }
                            isKill = false;
                            }, (AmongUsClient.Instance.Ping / 1000f)* 1.1f);
                    }
                    return false;
                }
                else
                {
                    return false;
                }
            }
            if (ModeHandler.isMode(ModeId.Zombie)) return false;
            if (ModeHandler.isMode(ModeId.SuperHostRoles))
            {
                if (__instance.isRole(RoleId.RemoteSheriff)) return false;
                if (__instance.isRole(RoleId.FalseCharges))
                {
                    target.RpcMurderPlayer(__instance);
                    RoleClass.FalseCharges.FalseChargePlayers[__instance.PlayerId] = target.PlayerId;
                    RoleClass.FalseCharges.AllTurns[__instance.PlayerId] = RoleClass.FalseCharges.DefaultTurn;
                    return false;
                }
            }
            if (ModeHandler.isMode(ModeId.Detective) && target.PlayerId == Mode.Detective.main.DetectivePlayer.PlayerId) return false;
            if (ModeHandler.isMode(ModeId.SuperHostRoles))
            {
                if (__instance.isRole(RoleId.Egoist) && !RoleClass.Egoist.UseKill)
                {
                    return false;
                }
                else if (__instance.isRole(RoleId.truelover))
                {
                    if (!__instance.IsLovers())
                    {
                        if (target == null || target.IsLovers() || RoleClass.truelover.CreatePlayers.Contains(__instance.PlayerId)) return false;
                        RoleClass.truelover.CreatePlayers.Add(__instance.PlayerId);
                        RoleHelpers.SetLovers(__instance, target);
                        RoleHelpers.SetLoversRPC(__instance, target);
                        //__instance.RpcSetRoleDesync(RoleTypes.GuardianAngel);
                        Mode.SuperHostRoles.FixedUpdate.SetRoleNames();
                    }
                    return false;
                }
                else if (__instance.isRole(RoleId.Sheriff))
                {
                    if (!RoleClass.Sheriff.KillCount.ContainsKey(__instance.PlayerId) || RoleClass.Sheriff.KillCount[__instance.PlayerId] >= 1)
                    {
                        if (!Sheriff.IsSheriffKill(target) || target.isRole(RoleId.Sheriff))
                        {
                            FinalStatusPatch.FinalStatusData.FinalStatuses[__instance.PlayerId] = FinalStatus.SheriffMisFire;
                            __instance.RpcMurderPlayer(__instance);
                            return false;
                        }
                        else
                        {
                            FinalStatusPatch.FinalStatusData.FinalStatuses[target.PlayerId] = FinalStatus.SheriffKill;
                            if (RoleClass.Sheriff.KillCount.ContainsKey(__instance.PlayerId))
                            {
                                RoleClass.Sheriff.KillCount[__instance.PlayerId]--;
                            }
                            else
                            {
                                RoleClass.Sheriff.KillCount[__instance.PlayerId] = (int)CustomOptions.SheriffKillMaxCount.getFloat() - 1;
                            }
                            __instance.RpcMurderPlayer(target);
                            return false;
                        }
                    } else
                    {
                        return false;
                    }
                }
                if (target.isRole(RoleId.StuntMan) && !__instance.isRole(RoleId.OverKiller))
                {
                    if (EvilEraser.IsOKAndTryUse(EvilEraser.BlockTypes.StuntmanGuard, __instance))
                    {
                        if (!RoleClass.StuntMan.GuardCount.ContainsKey(target.PlayerId))
                        {
                            RoleClass.StuntMan.GuardCount[target.PlayerId] = (int)CustomOptions.StuntManMaxGuardCount.getFloat() - 1;
                            target.RpcProtectPlayer(target, 0);
                            new LateTask(() => __instance.RpcMurderPlayer(target), 0.5f);
                            return false;
                        }
                        else
                        {
                            if (!(RoleClass.StuntMan.GuardCount[target.PlayerId] <= 0))
                            {
                                RoleClass.StuntMan.GuardCount[target.PlayerId]--;
                                target.RpcProtectPlayer(target, 0);
                                new LateTask(() => __instance.RpcMurderPlayer(target), 0.5f);
                                return false;
                            }
                        }
                    }
                }
                else if (target.isRole(RoleId.MadStuntMan) && !__instance.isRole(RoleId.OverKiller))
                {
                    if (EvilEraser.IsOKAndTryUse(EvilEraser.BlockTypes.MadStuntmanGuard, __instance))
                    {
                        if (!RoleClass.MadStuntMan.GuardCount.ContainsKey(target.PlayerId))
                        {
                            target.RpcProtectPlayer(target, 0);
                            new LateTask(() => __instance.RpcMurderPlayer(target), 0.5f);
                            return false;
                        }
                        else
                        {
                            if (!(RoleClass.MadStuntMan.GuardCount[target.PlayerId] <= 0))
                            {
                                RoleClass.MadStuntMan.GuardCount[target.PlayerId]--;
                                target.RpcProtectPlayer(target, 0);
                                new LateTask(() => __instance.RpcMurderPlayer(target), 0.5f);
                                return false;
                            }
                        }
                    }
                }
                else if (target.isRole(RoleId.Fox))
                {
                    if (EvilEraser.IsOKAndTryUse(EvilEraser.BlockTypes.FoxGuard, __instance))
                    {
                        if (!RoleClass.Fox.KillGuard.ContainsKey(target.PlayerId))
                        {
                            target.RpcProtectPlayer(target, 0);
                            new LateTask(() => __instance.RpcMurderPlayer(target), 0.5f);
                            return false;
                        }
                        else
                        {
                            if (!(RoleClass.Fox.KillGuard[target.PlayerId] <= 0))
                            {
                                RoleClass.Fox.KillGuard[target.PlayerId]--;
                                target.RpcProtectPlayer(target, 0);
                                new LateTask(() => __instance.RpcMurderPlayer(target), 0.5f);
                                return false;
                            }
                        }
                    }
                }
                else if (__instance.isRole(RoleId.Jackal))
                {
                    __instance.RpcMurderPlayer(target);
                    return false;
                }
            }
            if (__instance.isRole(RoleId.OverKiller))
            {
                __instance.RpcMurderPlayer(target);
                foreach (PlayerControl p in PlayerControl.AllPlayerControls)
                {
                    if (!p.Data.Disconnected && p.PlayerId != target.PlayerId)
                    {
                        if (p.PlayerId != 0)
                        {
                            for (int i = 0; i < RoleClass.OverKiller.KillCount - 1; i++)
                            {
                                __instance.RPCMurderPlayerPrivate(target,p);
                            }
                        } else
                        {
                            for (int i = 0; i < RoleClass.OverKiller.KillCount - 1; i++)
                            {
                                __instance.MurderPlayer(target);
                            }
                        }
                    }
                }
                return false;
            }
            if (!ModeHandler.isMode(ModeId.Default))
            {
                __instance.RpcMurderPlayer(target);
                return false;
            } else
            {
                return true;
            }
        }
    }
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.Die))]
    public static class DiePatch
    {
        public static void Postfix(PlayerControl __instance, [HarmonyArgument(0)] PlayerControl target)
        {
            if (ModeHandler.isMode(ModeId.SuperHostRoles))
            {
                if (target.isRole(RoleId.truelover))
                {
                    target.RpcSetRoleDesync(RoleTypes.GuardianAngel);
                }
            }
            else
            {
            }
        }
    }

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.SetKillTimer))]
    static class PlayerControlSetCoolDownPatch
    {
        public static bool Prefix(PlayerControl __instance, [HarmonyArgument(0)] float time)
        {
            if (PlayerControl.GameOptions.killCooldown == time && !RoleClass.IsCoolTimeSetted)
            {
                __instance.SetKillTimerUnchecked(RoleHelpers.GetEndMeetingKillCoolTime(__instance), RoleHelpers.GetEndMeetingKillCoolTime(__instance));
                RoleClass.IsCoolTimeSetted = true;
                return false;
            }
            return true;
        }
    }
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.MurderPlayer))]
    public static class MurderPlayerPatch
    {
        public static bool resetToCrewmate = false;
        public static bool resetToDead = false;
        public static void Prefix(PlayerControl __instance, PlayerControl target)
        {
            if (ModeHandler.isMode(ModeId.Default))
            {
                target.resetChange();
                if (target.PlayerId == PlayerControl.LocalPlayer.PlayerId)
                {
                    if (PlayerControl.LocalPlayer.isRole(RoleId.SideKiller))
                    {
                        var sideplayer = RoleClass.SideKiller.getSidePlayer(PlayerControl.LocalPlayer);
                        if (sideplayer != null)
                        {
                            if (!RoleClass.SideKiller.IsUpMadKiller)
                            {
                                sideplayer.RPCSetRoleUnchecked(RoleTypes.Impostor);
                                RoleClass.SideKiller.IsUpMadKiller = true;
                            }
                        }
                    }
                } else if(__instance.PlayerId == PlayerControl.LocalPlayer.PlayerId)
                {
                    if (__instance.isRole(RoleId.EvilGambler))
                    {
                        if (RoleClass.EvilGambler.GetSuc())
                        {
                            PlayerControl.LocalPlayer.SetKillTimer(RoleClass.EvilGambler.SucCool);
                        } else
                        {
                            PlayerControl.LocalPlayer.SetKillTimer(RoleClass.EvilGambler.NotSucCool);
                        }
                    }
                }
            }
        }
        public static void Postfix(PlayerControl __instance, [HarmonyArgument(0)] PlayerControl target)
        {
            // SuperNewRolesPlugin.Logger.LogInfo("MurderPlayer発生！元:" + __instance.getDefaultName() + "、ターゲット:" + target.getDefaultName());
            // Collect dead player info
            DeadPlayer deadPlayer = new DeadPlayer(target, DateTime.UtcNow, DeathReason.Kill, __instance);
            DeadPlayer.deadPlayers.Add(deadPlayer);
            FinalStatusPatch.FinalStatusData.FinalStatuses[target.PlayerId] = FinalStatus.Kill;

            SerialKiller.MurderPlayer(__instance,target);

            if (ModeHandler.isMode(ModeId.SuperHostRoles))
            {
                if (AmongUsClient.Instance.AmHost)
                {
                    MurderPlayer.Postfix(__instance, target);
                }
            }
            else if (ModeHandler.isMode(ModeId.Detective))
            {
                Mode.Detective.main.MurderPatch(target);
            }
            else if (ModeHandler.isMode(ModeId.Default))
            {
                Levelinger.MurderPlayer(__instance,target);
                if (RoleClass.Lovers.SameDie && target.IsLovers())
                {
                    if (__instance.PlayerId == PlayerControl.LocalPlayer.PlayerId)
                    {
                        PlayerControl SideLoverPlayer = target.GetOneSideLovers();
                        if (SideLoverPlayer.isAlive())
                        {
                            MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CustomRPC.RPCMurderPlayer, Hazel.SendOption.Reliable, -1);
                            writer.Write(SideLoverPlayer.PlayerId);
                            writer.Write(SideLoverPlayer.PlayerId);
                            writer.Write(byte.MaxValue);
                            AmongUsClient.Instance.FinishRpcImmediately(writer);
                            RPCProcedure.RPCMurderPlayer(SideLoverPlayer.PlayerId, SideLoverPlayer.PlayerId, byte.MaxValue);
                        }
                    }
                }
                if (target.IsQuarreled())
                {
                    if (AmongUsClient.Instance.AmHost)
                    {
                        var Side = RoleHelpers.GetOneSideQuarreled(target);
                        if (Side.isDead())
                        {
                            RPCProcedure.ShareWinner(target.PlayerId);

                            MessageWriter Writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CustomRPC.ShareWinner, Hazel.SendOption.Reliable, -1);
                            Writer.Write(target.PlayerId);
                            AmongUsClient.Instance.FinishRpcImmediately(Writer);
                            Roles.RoleClass.Quarreled.IsQuarreledWin = true;
                            ShipStatus.RpcEndGame((GameOverReason)CustomGameOverReason.QuarreledWin, false);
                        }
                    }
                }
                Minimalist.MurderPatch.Postfix(__instance);
            }
            if (__instance.PlayerId == PlayerControl.LocalPlayer.PlayerId)
            {
                if (__instance.isImpostor() && !__instance.isRole(RoleId.EvilGambler))
                {
                    PlayerControl.LocalPlayer.SetKillTimerUnchecked(RoleHelpers.getCoolTime(__instance),RoleHelpers.getCoolTime(__instance));
                }
            }
        }
    }
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.Exiled))]
    public static class ExilePlayerPatch
    {
        public static void Postfix(PlayerControl __instance)
        {
            // Collect dead player info
            DeadPlayer deadPlayer = new DeadPlayer(__instance, DateTime.UtcNow, DeathReason.Exile, null);
            DeadPlayer.deadPlayers.Add(deadPlayer);
            FinalStatusPatch.FinalStatusData.FinalStatuses[__instance.PlayerId] = FinalStatus.Exiled;
            if (ModeHandler.isMode(ModeId.Default))
            {

                if (RoleClass.Lovers.SameDie && __instance.IsLovers())
                {
                    if (__instance.PlayerId == PlayerControl.LocalPlayer.PlayerId)
                    {
                        PlayerControl SideLoverPlayer = __instance.GetOneSideLovers();
                        if (SideLoverPlayer.isAlive())
                        {
                            MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CustomRPC.ExiledRPC, Hazel.SendOption.Reliable, -1);
                            writer.Write(SideLoverPlayer.PlayerId);
                            AmongUsClient.Instance.FinishRpcImmediately(writer);
                            RPCProcedure.ExiledRPC(SideLoverPlayer.PlayerId);
                        }
                    }
                }
            }
        }
    }
    public static class PlayerControlFixedUpdatePatch
    {
        public static PlayerControl setTarget(bool onlyCrewmates = false, bool targetPlayersInVents = false, List<PlayerControl> untargetablePlayers = null, PlayerControl targetingPlayer = null)
        {
            PlayerControl result = null;
            float num = GameOptionsData.KillDistances[Mathf.Clamp(PlayerControl.GameOptions.KillDistance, 0, 2)];
            if (!ShipStatus.Instance) return result;
            if (targetingPlayer == null) targetingPlayer = PlayerControl.LocalPlayer;
            if (targetingPlayer.Data.IsDead || targetingPlayer.inVent) return result;

            if (untargetablePlayers == null)
            {
                untargetablePlayers = new List<PlayerControl>();
            }

            Vector2 truePosition = targetingPlayer.GetTruePosition();
            Il2CppSystem.Collections.Generic.List<GameData.PlayerInfo> allPlayers = GameData.Instance.AllPlayers;
            for (int i = 0; i < allPlayers.Count; i++)
            {
                GameData.PlayerInfo playerInfo = allPlayers[i];
                if (!playerInfo.Disconnected && playerInfo.PlayerId != targetingPlayer.PlayerId && !playerInfo.IsDead && (!onlyCrewmates || !playerInfo.Role.IsImpostor))
                {
                    PlayerControl @object = playerInfo.Object;
                    if (untargetablePlayers.Any(x => x == @object))
                    {
                        // if that player is not targetable: skip check
                        continue;
                    }

                    if (@object && (!@object.inVent || targetPlayersInVents))
                    {
                        Vector2 vector = @object.GetTruePosition() - truePosition;
                        float magnitude = vector.magnitude;
                        if (magnitude <= num && !PhysicsHelpers.AnyNonTriggersBetween(truePosition, vector.normalized, magnitude, Constants.ShipAndObjectsMask))
                        {
                            result = @object;
                            num = magnitude;
                        }
                    }
                }
            }
            return result;
        }
    }
}