﻿using SuperNewRoles.CustomOption;
using SuperNewRoles.CustomRPC;
using SuperNewRoles.Intro;
using SuperNewRoles.Patch;
using SuperNewRoles.Roles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SuperNewRoles.Mode.SuperHostRoles
{
    public static class RoleSelectHandler
    {
        public static void RoleSelect()
        {
            if (!AmongUsClient.Instance.AmHost) return;
            OneOrNotListSet();
            CrewOrImpostorSet();
            AllRoleSetClass.AllRoleSet();
            SetCustomRoles();
            SyncSetting.CustomSyncSettings();
            ChacheManager.ResetChache();
            FixedUpdate.SetRoleNames();
            main.SendAllRoleChat();
            
            //BotHandler.AddBot(3, "キルされるBot");
            new LateTask(() => {
                if (AmongUsClient.Instance.GameState == AmongUsClient.GameStates.Started)
                {
                    PlayerControl.LocalPlayer.RpcSetName(PlayerControl.LocalPlayer.getDefaultName());
                    PlayerControl.LocalPlayer.RpcSendChat("＊注意(自動送信)＊\nこのMODは、バグ等がたくさん発生します。\nいろいろな重大なバグがあるため、あくまで自己責任でお願いします。");
                    foreach (var pc in PlayerControl.AllPlayerControls)
                    {
                        pc.RpcSetRole(RoleTypes.Shapeshifter);
                        SuperNewRolesPlugin.Logger.LogInfo("シェイプシフターセット！");
                    }
                }
            }, 3f, "SetImpostor");
        }
        public static void SpawnBots()
        {
            if (ModeHandler.isMode(ModeId.SuperHostRoles))
            {

                bool IsJackalSpawned = false;
                //ジャッカルがいるなら
                if (CustomOptions.JackalOption.getSelection() != 0)
                {
                    IsJackalSpawned = true;
                    for (int i = 0; i < (1 * PlayerControl.GameOptions.NumImpostors + 2); i++)
                    {
                        PlayerControl bot = BotManager.Spawn("暗転対策BOT" + (i + 1));
                        if (i == 0)
                        {
                            bot.RpcSetRole(RoleTypes.Impostor);
                        }
                        if (i > 0) {
                            bot.RpcSetRole(RoleTypes.Crewmate);
                        }
                    }
                } else
                {
                bool flag = !IsJackalSpawned && (
                    CustomOptions.EgoistOption.getSelection() != 0 ||
                    CustomOptions.SheriffOption.getSelection() != 0 ||
                    CustomOptions.trueloverOption.getSelection() != 0 ||
                    CustomOptions.FalseChargesOption.getSelection() != 0 ||
                    CustomOptions.RemoteSheriffOption.getSelection() != 0 ||
                    CustomOptions.MadMakerOption.getSelection() != 0 ||
                    CustomOptions.DemonOption.getSelection() != 0 
                    );
                    if (flag)
                    {
                        PlayerControl bot1 = BotManager.Spawn("暗転対策BOT1");
                        bot1.RpcSetRole(RoleTypes.Impostor);

                        PlayerControl bot2 = BotManager.Spawn("暗転対策BOT2");
                        bot2.RpcSetRole(RoleTypes.Crewmate);

                        PlayerControl bot3 = BotManager.Spawn("暗転対策BOT3");
                        bot3.RpcSetRole(RoleTypes.Crewmate);
                    }
                }
                if (CustomOptions.BakeryOption.getSelection() != 0)
                {
                    BotManager.Spawn("パン屋BOT").Exiled();
                }
            }
        }
        public static void SetCustomRoles() {
            List<PlayerControl> DesyncImpostors = new List<PlayerControl>();
            DesyncImpostors.AddRange(RoleClass.Jackal.JackalPlayer);
            DesyncImpostors.AddRange(RoleClass.Sheriff.SheriffPlayer);
            DesyncImpostors.AddRange(RoleClass.Demon.DemonPlayer);
            DesyncImpostors.AddRange(RoleClass.RemoteSheriff.RemoteSheriffPlayer);
            DesyncImpostors.AddRange(RoleClass.truelover.trueloverPlayer);
            DesyncImpostors.AddRange(RoleClass.FalseCharges.FalseChargesPlayer);
            DesyncImpostors.AddRange(RoleClass.MadMaker.MadMakerPlayer);


            List<PlayerControl> SetRoleEngineers = new List<PlayerControl>();
            if (RoleClass.Jester.IsUseVent) SetRoleEngineers.AddRange(RoleClass.Jester.JesterPlayer);
            if (RoleClass.JackalFriends.IsUseVent) SetRoleEngineers.AddRange(RoleClass.JackalFriends.JackalFriendsPlayer);
            if (RoleClass.MadMate.IsUseVent) SetRoleEngineers.AddRange(RoleClass.MadMate.MadMatePlayer);
            if (RoleClass.MadStuntMan.IsUseVent) SetRoleEngineers.AddRange(RoleClass.MadStuntMan.MadStuntManPlayer);
            if (RoleClass.MadJester.IsUseVent) SetRoleEngineers.AddRange(RoleClass.MadJester.MadJesterPlayer);
            if (RoleClass.Fox.IsUseVent) SetRoleEngineers.AddRange(RoleClass.Fox.FoxPlayer);
            SetRoleEngineers.AddRange(RoleClass.Technician.TechnicianPlayer);

            foreach (PlayerControl Player in DesyncImpostors)
            {
                if (!Player.IsMod())
                {
                    Player.RpcSetRoleDesync(RoleTypes.Impostor);
                    foreach (PlayerControl p in PlayerControl.AllPlayerControls)
                    {
                        if (p.PlayerId != Player.PlayerId && p.IsPlayer())
                        {
                            Player.RpcSetRoleDesync(RoleTypes.Scientist, p);
                            p.RpcSetRoleDesync(RoleTypes.Scientist, Player);
                        }
                    }
                }
                else
                {
                    Player.RpcSetRole(RoleTypes.Crewmate);
                }
            }
            foreach (PlayerControl p in RoleClass.Egoist.EgoistPlayer)
            {
                if (!p.IsMod())
                {
                    p.RpcSetRole(RoleTypes.Impostor);
                    foreach (PlayerControl p2 in PlayerControl.AllPlayerControls)
                    {
                        if (p2.PlayerId != p.PlayerId && !p.isRole(RoleId.Sheriff) && !p.isRole(RoleId.truelover) && p.IsPlayer())
                        {
                            p2.RpcSetRoleDesync(RoleTypes.Scientist, p);
                        }
                    }
                }
                else
                {
                    p.RpcSetRoleDesync(RoleTypes.Crewmate);
                    p.RpcSetRole(RoleTypes.Impostor);
                }
                //p.Data.IsDead = true;
            }

            foreach (PlayerControl p in SetRoleEngineers)
            {
                if (!ShareGameVersion.GameStartManagerUpdatePatch.VersionPlayers.ContainsKey(p.getClientId()))
                {
                    p.RpcSetRoleDesync(RoleTypes.Engineer);
                }
            }
            foreach (PlayerControl p in RoleClass.SelfBomber.SelfBomberPlayer)
            {
                p.RpcSetRole(RoleTypes.Shapeshifter);
            }
        }
        public static void CrewOrImpostorSet()
        {
            AllRoleSetClass.CrewMatePlayers = new List<PlayerControl>();
            AllRoleSetClass.ImpostorPlayers = new List<PlayerControl>();
            foreach (PlayerControl Player in PlayerControl.AllPlayerControls)
            {
                if (Player.IsPlayer())
                {
                    if (AllRoleSetClass.impostors.IsCheckListPlayerControl(Player))
                    {
                        AllRoleSetClass.ImpostorPlayers.Add(Player);
                    }
                    else
                    {
                        AllRoleSetClass.CrewMatePlayers.Add(Player);
                    }
                }
            }
        }
        public static void OneOrNotListSet()
        {
            var Impoonepar = new List<RoleId>();
            var Imponotonepar = new List<RoleId>();
            var Neutonepar = new List<RoleId>();
            var Neutnotonepar = new List<RoleId>();
            var Crewonepar = new List<RoleId>();
            var Crewnotonepar = new List<RoleId>();

            foreach (IntroDate intro in IntroDate.IntroDatas)
            {
                if (intro.RoleId != RoleId.DefaultRole)
                {
                    var option = IntroDate.GetOption(intro.RoleId);
                    if (option == null || !option.isSHROn) continue;
                    var selection = option.getSelection();
                    if (selection != 0)
                    {
                        if (selection == 10)
                        {
                            switch (intro.Team)
                            {
                                case TeamRoleType.Crewmate:
                                    Crewonepar.Add(intro.RoleId);
                                    break;
                                case TeamRoleType.Impostor:
                                    Impoonepar.Add(intro.RoleId);
                                    break;
                                case TeamRoleType.Neutral:
                                    Neutonepar.Add(intro.RoleId);
                                    break;
                            }
                        }
                        else
                        {
                            for (int i = 1; i <= selection; i++)
                            {
                                switch (intro.Team)
                                {
                                    case TeamRoleType.Crewmate:
                                        Crewonepar.Add(intro.RoleId);
                                        break;
                                    case TeamRoleType.Impostor:
                                        Impoonepar.Add(intro.RoleId);
                                        break;
                                    case TeamRoleType.Neutral:
                                        Neutonepar.Add(intro.RoleId);
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            if (!(CustomOption.CustomOptions.BakeryOption.getString().Replace("0%", "") == ""))
            {
                int OptionDate = int.Parse(CustomOption.CustomOptions.BakeryOption.getString().Replace("0%", ""));
                RoleId ThisRoleId = RoleId.Bakery;
                if (OptionDate == 10)
                {
                    Crewonepar.Add(ThisRoleId);
                }
                else
                {
                    for (int i = 1; i <= OptionDate; i++)
                    {
                        Crewnotonepar.Add(ThisRoleId);
                    }
                }
            }
            if (!(CustomOption.CustomOptions.SeerOption.getString().Replace("0%", "") == ""))
            {
                int OptionDate = int.Parse(CustomOption.CustomOptions.SeerOption.getString().Replace("0%", ""));
                RoleId ThisRoleId = RoleId.Seer;
                if (OptionDate == 10)
                {
                    Crewonepar.Add(ThisRoleId);
                }
                else
                {
                    for (int i = 1; i <= OptionDate; i++)
                    {
                        Crewnotonepar.Add(ThisRoleId);
                    }
                }
            }
            if (!(CustomOption.CustomOptions.EvilSeerOption.getString().Replace("0%", "") == ""))
            {
                int OptionDate = int.Parse(CustomOption.CustomOptions.EvilSeerOption.getString().Replace("0%", ""));
                RoleId ThisRoleId = RoleId.EvilSeer;
                if (OptionDate == 10)
                {
                    Impoonepar.Add(ThisRoleId);
                }
                else
                {
                    for (int i = 1; i <= OptionDate; i++)
                    {
                        Imponotonepar.Add(ThisRoleId);
                    }
                }
            }
            AllRoleSetClass.Impoonepar = Impoonepar;
            AllRoleSetClass.Imponotonepar = Imponotonepar;
            AllRoleSetClass.Neutonepar = Neutonepar;
            AllRoleSetClass.Neutnotonepar = Neutnotonepar;
            AllRoleSetClass.Crewonepar = Crewonepar;
            AllRoleSetClass.Crewnotonepar = Crewnotonepar;
        }
    }
}
