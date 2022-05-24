﻿using BepInEx.IL2CPP.Utils;
using HarmonyLib;
using PowerTools;
using SuperNewRoles.Map.Agartha.Patch.Task;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SuperNewRoles.Map.Agartha.Patch
{
    public static class SetTasksClass
    {
        public static List<Console> AddConsoles;
        public static void SetTasks(this Transform MiraShip)
        {
            AddConsoles = new List<Console>();
            Transform FixWiring = MiraShip.FindChild("LabHall").FindChild("FixWiringConsole");
            FixWiring.gameObject.SetActive(true);
            FixWiring.position = new Vector3(-2.2f, 20.7f, 0.1f);
            FixWiring.localScale *= 0.8f;
            FixWiring.Rotate(new Vector3(75, 0, 0));
            FixWiring.GetComponent<SpriteRenderer>().sprite = ImageManager.Task_FixWiring1;

            Transform FixWiring1 = MiraShip.FindChild("Garden").FindChild("FixWiringConsole");
            //FixWiring1.gameObject.SetActive(true);
            FixWiring1.position = new Vector3(-0.9f, 1.65f, 0.1f);
            FixWiring1.localScale *= 0.8f;
            FixWiring1.GetComponent<SpriteRenderer>().sprite = ImageManager.Task_FixWiring1;

            //研究室
            Transform FixWiring2 = MiraShip.FindChild("SkyBridge").FindChild("FixWiringConsole (2)");
            FixWiring2.gameObject.SetActive(true);
            FixWiring2.position = new Vector3(20.5f, 2.5f, 0.1f);
            FixWiring2.localScale *= 0.8f;
            FixWiring2.Rotate(new Vector3(75, 0, 0));
            FixWiring2.GetComponent<SpriteRenderer>().sprite = ImageManager.Task_FixWiring1;

            Transform FixWiring3 = MiraShip.FindChild("Cafe").FindChild("FixWiringConsole (3)");
            FixWiring3.gameObject.SetActive(true);
            FixWiring3.localPosition = new Vector3(-4.5f, 0f, 0.1f);
            FixWiring3.localScale *= 0.8f;
            FixWiring3.GetComponent<SpriteRenderer>().sprite = ImageManager.Task_FixWiring1;

            Transform FixWiring4 = MiraShip.FindChild("Locker").FindChild("FixWiringConsole (4)");
            FixWiring4.gameObject.SetActive(true);
            FixWiring4.position = new Vector3(22.6f, 24f, 0.1f);
            FixWiring4.localScale *= 0.8f;
            FixWiring4.GetComponent<SpriteRenderer>().sprite = ImageManager.Task_FixWiring1;

            Transform FixWiring5 = GameObject.Instantiate(MiraShip.FindChild("Locker").FindChild("FixWiringConsole (4)"), SetPosition.miraship);
            FixWiring5.name = "FixWiringConsole (5)";
            FixWiring5.GetComponent<Console>().ConsoleId = 5;
            //FixWiring5.gameObject.SetActive(true);
            FixWiring5.position = new Vector3(-12.8f, 4f, 0.1f);
            FixWiring5.localScale *= 0.8f;
            FixWiring5.Rotate(new Vector3(0, 60, 90));
            FixWiring5.GetComponent<SpriteRenderer>().sprite = ImageManager.Task_FixWiring1;

            Transform MedScanner = MiraShip.FindChild("MedBay").FindChild("MedScanner");
            MedScanner.position = new Vector3(-2.2f, 13.1f, 0.1f);

            Transform MedBayConsole = GameObject.Instantiate(MapLoader.SkeldObject.transform.FindChild("Medical").FindChild("Ground").FindChild("MedBayConsole"), SetPosition.miraship);
            MedBayConsole.gameObject.SetActive(true);
            MedBayConsole.position = new Vector3(2.2f, 14.4f, 0.1f);
            AddConsoles.Add(MedBayConsole.GetComponent<Console>());

            Transform Upload1 = GameObject.Instantiate(MapLoader.Skeld.transform.FindChild("Admin").FindChild("Ground").FindChild("admin_walls").FindChild("UploadDataConsole"), SetPosition.miraship);
            Upload1.GetComponent<Console>().ConsoleId = 0;
            Upload1.position = new Vector3(15.1f, 20.4f, 4f);
            AddConsoles.Add(Upload1.GetComponent<Console>());

            Transform Download_Aisle1 = GameObject.Instantiate(MapLoader.Skeld.transform.FindChild("Cockpit").FindChild("Ground").FindChild("UploadDataConsole"), SetPosition.miraship);
            Download_Aisle1.GetComponent<Console>().ConsoleId = 1;
            Download_Aisle1.position = new Vector3(14.2f, 0.95f, 4f);
            AddConsoles.Add(Download_Aisle1.GetComponent<Console>());

            Transform Download_Aisle2 = GameObject.Instantiate(Download_Aisle1, SetPosition.miraship); 
            Download_Aisle2.GetComponent<Console>().ConsoleId = 2;
            Download_Aisle2.position = new Vector3(21.6f, 9.4f, 4f);
            AddConsoles.Add(Download_Aisle2.GetComponent<Console>());

            Transform Download_Aisle3 = GameObject.Instantiate(Download_Aisle1, SetPosition.miraship);
            Download_Aisle3.GetComponent<Console>().ConsoleId = 3;
            Download_Aisle3.position = new Vector3(17.7f, 24.3f, 4f);
            AddConsoles.Add(Download_Aisle3.GetComponent<Console>());

            Transform Download_Aisle4 = GameObject.Instantiate(Download_Aisle1, SetPosition.miraship);
            Download_Aisle4.GetComponent<Console>().ConsoleId = 4;
            Download_Aisle4.position = new Vector3(-5, 5.9f, 4f);
            AddConsoles.Add(Download_Aisle4.GetComponent<Console>());

            Transform Download_Aisle5 = GameObject.Instantiate(Download_Aisle1, SetPosition.miraship);
            Download_Aisle5.GetComponent<Console>().ConsoleId = 5;
            Download_Aisle5.position = new Vector3(1.25f, 1.65f, 4f);
            AddConsoles.Add(Download_Aisle5.GetComponent<Console>());

            Transform Download_Aisle6 = GameObject.Instantiate(Download_Aisle1, SetPosition.miraship);
            Download_Aisle6.GetComponent<Console>().ConsoleId = 6;
            Download_Aisle6.position = new Vector3(-2.3f, 23.1f, 0f);
            Download_Aisle6.localScale = new Vector3(1.25f,1.25f,1.25f);
            AddConsoles.Add(Download_Aisle6.GetComponent<Console>());

            Transform reactordesc = MiraShip.FindChild("Reactor").FindChild("reactor-desk-elec");
            reactordesc.gameObject.SetActive(true);
            Transform DivertPowerConsoleMain = reactordesc.FindChild("DivertPowerConsoleMain");
            DivertPowerConsoleMain.GetComponent<Console>().ConsoleId = 0;
            DivertPowerConsoleMain.position = new Vector3(21.1f, 11.3f, 4f);

            Transform DivertPowerConsole1 = MiraShip.FindChild("Comms").FindChild("DivertPowerConsole (9)");
            DivertPowerConsole1.gameObject.SetActive(true);
            DivertPowerConsole1.GetComponent<Console>().ConsoleId = 1;
            DivertPowerConsole1.position = new Vector3(1.5f, 23f, 0.1f);
            DivertPowerConsole1.localScale = new Vector3(1.2f, 1.2f, 0);
            AddConsoles.Add(DivertPowerConsole1.GetComponent<Console>());

            Transform DivertPowerConsole2 = MiraShip.FindChild("Cafe").FindChild("DivertPowerConsole (3)");
            DivertPowerConsole2.gameObject.SetActive(true);
            DivertPowerConsole2.GetComponent<Console>().ConsoleId = 2;
            DivertPowerConsole2.Rotate(new Vector3(75, 0, 0));
            DivertPowerConsole2.localPosition = new Vector3(1f, -4.5f, 4f);

            Transform DivertPowerConsole3 = MiraShip.FindChild("Laboratory").FindChild("DivertPowerConsole (6)");
            DivertPowerConsole3.gameObject.SetActive(true);
            DivertPowerConsole3.GetComponent<Console>().ConsoleId = 3;
            DivertPowerConsole3.position = new Vector3(19.7f, 24.4f, 4f);

            Transform DivertPowerConsole4 = MiraShip.FindChild("MedBay").FindChild("DivertPowerConsole (8)");
            DivertPowerConsole4.gameObject.SetActive(true);
            DivertPowerConsole4.GetComponent<Console>().ConsoleId = 4;
            DivertPowerConsole4.position = new Vector3(-2.7f, 15.3f, 4f);

            Transform DivertPowerConsole5 = MiraShip.FindChild("Admin").FindChild("DivertPowerConsoleAdmin");
            DivertPowerConsole5.gameObject.SetActive(true);
            DivertPowerConsole5.GetComponent<Console>().ConsoleId = 5;
            DivertPowerConsole5.position = new Vector3(8.9f, 0.9f, 4f);

            Transform OfficeStant = MiraShip.FindChild("Office").FindChild("divertElevStand");
            OfficeStant.gameObject.SetActive(true);
            GameObject.Destroy(OfficeStant.GetComponent<CircleCollider2D>());
            GameObject.Destroy(OfficeStant.GetComponent<SpriteRenderer>());
            Transform DivertPowerConsole6 = OfficeStant.FindChild("DivertPowerConsoleOffice");
            DivertPowerConsole6.gameObject.SetActive(true);
            DivertPowerConsole6.GetComponent<Console>().ConsoleId = 6;
            DivertPowerConsole6.position = new Vector3(-6.9f, -0.8f, 4f);

            Transform Airlock_O2 = GameObject.Instantiate(MapLoader.Skeld.transform.FindChild("LifeSupport").FindChild("Ground").FindChild("GarbageConsole"), SetPosition.miraship);
            Airlock_O2.GetComponent<Console>().ConsoleId = 2;
            Airlock_O2.position = new Vector3(-3.3f, 9.6f, 4f);
            AddConsoles.Add(Airlock_O2.GetComponent<Console>());

            Transform Airlock_Labo = GameObject.Instantiate(MapLoader.Skeld.transform.FindChild("Cafeteria").FindChild("Ground").FindChild("GarbageConsole"), SetPosition.miraship);
            Airlock_Labo.name = "Labo_GarbageConsole";
            Airlock_Labo.GetComponent<Console>().ConsoleId = 1;
            Airlock_Labo.position = new Vector3(19.4f, 8f, 4f);
            AddConsoles.Add(Airlock_Labo.GetComponent<Console>());

            Transform Airlock_Aisle1 = GameObject.Instantiate(MapLoader.Skeld.transform.FindChild("Storage").FindChild("Ground").FindChild("AirlockConsole"), SetPosition.miraship);
            Airlock_Aisle1.GetComponent<Console>().ConsoleId = 0;
            Airlock_Aisle1.position = new Vector3(22.6f, 0.3f, 4f);
            AddConsoles.Add(Airlock_Aisle1.GetComponent<Console>());
            
            Transform Airlock_AisleSeeObject1 = GameObject.Instantiate(MapLoader.Skeld.transform.FindChild("HullItems").FindChild("hatch0001"), SetPosition.miraship);
            Airlock_AisleSeeObject1.position = new Vector3(22.78f, 0.3f, 4f);
            AirlockParticle = Airlock_AisleSeeObject1.FindChild("Particles");
            AirlockParticle.position = new Vector3(22.78f, 0.3f, 4f);
            Airlock_AisleSeeObject1.Rotate(new Vector3(0, 261f, 100f));
        }
        static Transform AirlockParticle;
        [HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.OpenHatch))]
        class ab
        {
            static void Postfix(ShipStatus __instance)
            {
                if (Data.IsMap(CustomMapNames.Agartha))
                {
                    AmongUsClient.Instance.StartCoroutine(Airlockhatch(__instance));
                }
            }
            static IEnumerator Airlockhatch(ShipStatus __instance)
            {
                AirlockParticle.gameObject.SetActive(false);
                AirlockParticle.gameObject.SetActive(true);
                yield return new WaitForSeconds(1);
                AirlockParticle.gameObject.SetActive(false);
            }
        }
        [HarmonyPatch(typeof(Minigame),nameof(Minigame.Begin))]
        class task
        {
            public static void Postfix(Minigame __instance,[HarmonyArgument(0)] PlayerTask task)
            {
                SuperNewRolesPlugin.Logger.LogInfo("ミニゲーム名:"+__instance.name);
                switch (__instance.name)
                {
                    case "FossilMinigame(Clone)":
                        __instance.SetMinigameCond();
                        Task.FossilMinigame.Start(__instance,task.TryCast<NormalPlayerTask>());
                        break;
                }
            }
        }
        public static void SetMinigameCond(this Minigame minigame)
        {
            minigame.transform.FindChild("RightWires").gameObject.SetActive(false);
            minigame.transform.FindChild("LeftWires").gameObject.SetActive(false);
            minigame.transform.FindChild("RightLights").gameObject.SetActive(false);
            minigame.transform.FindChild("LeftLights").gameObject.SetActive(false);
        }
        public static void ShipSetTask()
        {
            List<NormalPlayerTask> CommonTasks = new List<NormalPlayerTask>();
            List<NormalPlayerTask> NormalTasks = new List<NormalPlayerTask>();
            List<NormalPlayerTask> LongTasks = new List<NormalPlayerTask>();

            foreach (NormalPlayerTask task in ShipStatus.Instance.CommonTasks)
            {
                switch (task.name)
                {
                    case "FixWiring 1":
                    case "EnterCodeTask":
                        CommonTasks.Add(task);
                        break;
                }
                SuperNewRolesPlugin.Logger.LogInfo("(C)" + task.name);
            }
            foreach (NormalPlayerTask task in ShipStatus.Instance.NormalTasks)
            {
                switch (task.name)
                {
                    case "VentCleaning":
                        NormalTasks.Add(task);
                        break;
                }
                SuperNewRolesPlugin.Logger.LogInfo("(N)" + task.name);
            }
            foreach (NormalPlayerTask task in ShipStatus.Instance.LongTasks)
            {
                switch (task.name)
                {
                    case "DivertHqCommsPower":
                    case "DivertCafePower":
                    case "DivertLaunchPadPower":
                    case "DivertMedbayPower":
                    case "DivertAdminPower":
                    case "DivertOfficePower":
                    case "MedScanTask":
                        LongTasks.Add(task);
                        break;

                }
                SuperNewRolesPlugin.Logger.LogInfo("(L)" + task.name);
            }
            foreach (NormalPlayerTask task in MapLoader.Skeld.LongTasks)
            {
                switch (task.name)
                {
                    case "EmptyFilterChute":
                    case "EmptyGarbage":
                    case "InspectSample":
                        LongTasks.Add(task);
                        break;

                }
                //SuperNewRolesPlugin.Logger.LogInfo("(Skeld)(L)" + task.name);
            }
            foreach (NormalPlayerTask task in MapLoader.Skeld.CommonTasks)
            {
                switch (task.name)
                {
                    case "InspectSample":
                        break;
                }
                //SuperNewRolesPlugin.Logger.LogInfo("(Skeld)(C)" + task.name);
            }
            foreach (NormalPlayerTask task in MapLoader.Skeld.NormalTasks)
            {
                switch (task.name)
                {
                    case "UploadNav":
                        NormalTasks.Add(task);
                        break;
                }
                //SuperNewRolesPlugin.Logger.LogInfo("(Skeld)(N)" + task.name);
            }
            /*
            foreach (NormalPlayerTask task in MapLoader.Airship.LongTasks)
            {
                switch (task.name)
                {
                    case "InspectSample":
                        break;

                }
                SuperNewRolesPlugin.Logger.LogInfo("(Airship)(L)" + task.name);
            }
            foreach (NormalPlayerTask task in MapLoader.Airship.CommonTasks)
            {
                switch (task.name)
                {
                    case "InspectSample":
                        break;
                }
                SuperNewRolesPlugin.Logger.LogInfo("(Airship)(C)" + task.name);
            }
            foreach (NormalPlayerTask task in MapLoader.Airship.NormalTasks)
            {
                switch (task.name)
                {
                    case "UploadNav":
                        break;
                }
                SuperNewRolesPlugin.Logger.LogInfo("(Airship)(N)" + task.name);
            }
            */
            List<Console> newconsole = ShipStatus.Instance.AllConsoles.ToList();
            foreach (Console consolea in AddConsoles)
            {
                newconsole.Add(consolea);
            }
            ShipStatus.Instance.AllConsoles = newconsole.ToArray();

            ShipStatus.Instance.CommonTasks = CommonTasks.ToArray();
            ShipStatus.Instance.NormalTasks = NormalTasks.ToArray();
            ShipStatus.Instance.LongTasks = LongTasks.ToArray();

            foreach (NormalPlayerTask task in ShipStatus.Instance.LongTasks)
            {
                if (ChangeTasks.ContainsKey(task.name))
                {
                    task.StartAt = ChangeTasks[task.name];
                }
                SuperNewRolesPlugin.Logger.LogInfo("(L)"+task.name);
            }
            foreach (NormalPlayerTask task in ShipStatus.Instance.NormalTasks)
            {
                if (ChangeTasks.ContainsKey(task.name))
                {
                    task.StartAt = ChangeTasks[task.name];
                }
                SuperNewRolesPlugin.Logger.LogInfo("(N)"+task.name);
            }
            foreach (NormalPlayerTask task in ShipStatus.Instance.CommonTasks)
            {
                if (ChangeTasks.ContainsKey(task.name))
                {
                    task.StartAt = ChangeTasks[task.name];
                }
                SuperNewRolesPlugin.Logger.LogInfo("(C)" + task.name);
            }
        }
        static Dictionary<string, SystemTypes> ChangeTasks = new Dictionary<string, SystemTypes>()
        {
            { "EmptyGarbage",SystemTypes.LifeSupp },{ "EmptyFilterChute",SystemTypes.Laboratory},{ "UploadNav" ,SystemTypes.Nav}
        };
        public static void SetSabotage(Transform Miraship)
        {
            Transform O2_Locker = Miraship.FindChild("Locker").FindChild("NoOxyConsole");
            O2_Locker.gameObject.SetActive(true);
            O2_Locker.position = new Vector3(-1.85f, 9.813f, 0.1f);

            Transform GardenGreenHousePanel = Miraship.FindChild("Garden").FindChild("greenhousePanel");
            GardenGreenHousePanel.gameObject.SetActive(true);
            GardenGreenHousePanel.gameObject.GetChildren().SetActiveAllObject("NoOxyConsole",false);
            Transform O2_Garden = GardenGreenHousePanel.FindChild("NoOxyConsole");
            O2_Garden.position = new Vector3(0f, 9.813f, 0.1f);

            Transform Comms_Comm = Miraship.FindChild("Comms").FindChild("comms-top").FindChild("FixCommsConsole");
            Comms_Comm.gameObject.SetActive(true);
            Comms_Comm.position = new Vector3(0.6f, 23.15f, 0.1f);
            Comms_Comm.localScale *= 2f;

            Transform OfficeMid = Miraship.FindChild("Office").FindChild("office-mid");
            OfficeMid.gameObject.SetActive(true);
            GameObject.Destroy(OfficeMid.GetComponent<BoxCollider2D>());
            GameObject.Destroy(OfficeMid.GetComponent<SpriteRenderer>());
            Transform Comms_Ofice = OfficeMid.FindChild("FixCommsConsole");
            Comms_Ofice.gameObject.SetActive(true);
            Comms_Ofice.position = new Vector3(-1.4f, 23.15f, 0.1f);

            Transform Powerdown = Miraship.FindChild("Office").FindChild("SwitchConsole");
            Powerdown.gameObject.SetActive(true);
            Powerdown.position = new Vector3(19f, 19.25f, 0.1f);
            Powerdown.localScale = new Vector3(1.25f, 1.25f, 1.25f);
        }
        [HarmonyPatch(typeof(ShipStatus),nameof(ShipStatus.OnEnable))]
        class EnablePatch
        {
            public static void Postfix(ShipStatus __instance)
            {
                if (Data.IsMap(CustomMapNames.Agartha))
                {
                    ShipStatus.Instance.Systems.Add(SystemTypes.Doors, new DoorsSystemType().TryCast<ISystemType>());
                }
            }
        }
    }
}
