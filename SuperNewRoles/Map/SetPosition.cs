using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using SuperNewRoles.CustomOption;
using UnityEngine;

namespace SuperNewRoles.Map
{
    class SetPosition
    {
        /*
        [HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.Begin))]
        public static class ShipStatusBeginPatch
        {
            [HarmonyPostfix]
            //[HarmonyPatch]
            public static void Postfix(ShipStatus __instance)
            {
                ApplyChanges(__instance);
            }
        }
        */
        [HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.Awake))]
        public static class ShipStatusAwakePatch
        {
            public static void Postfix(ShipStatus __instance)
            {
                CustomAnimation.Animation.Animations = new List<CustomAnimation.Animation>();
                try
                {
                    ApplyChanges(__instance);
                }
                catch(Exception e) {
                    SuperNewRolesPlugin.Logger.LogInfo("ShipStatusのAwakeエラー:"+e);
                }
                SuperNewRoles.Patch.SelectTask.IsAwakeEnd = true;
            }
        }
        public static void ApplyChanges(ShipStatus __instance)
        {
            if (Data.IsMap(CustomMapNames.Agartha))
            {
                if (__instance.Type == ShipStatus.MapType.Hq)
                {
                    SuperNewRolesPlugin.Logger.LogInfo("Awakeを実行！");
                    Transform MiraShip = GameObject.Find("MiraShip(Clone)").transform;

                    Agartha.Patch.SetPosition.miraship = MiraShip;

                    Agartha.Patch.SetPosition.SetCamera();

                    Transform Wall = MiraShip.FindChild("Walls");
                    
                    Wall.gameObject.AddComponent<EdgeCollider2D>().points =
                        new Vector2[] { new Vector2(-6.25f, 2.8f), new Vector2(10.175f, 2.8f), new Vector2(10.175f, 6f), new Vector2(12.1f, 6f), new Vector2(12.1f, 22.55f), new Vector2(10.825f, 22.55f), new Vector2(10.825f, 24.5f), new Vector2(11.2f, 24.5f), new Vector2(11.2f, 27.4f), new Vector2(-6.25f, 27.4f), new Vector2(-6.25f, 22f),
                            new Vector2(-11.8f, 22f), new Vector2(-11.8f, 24f), new Vector2(-10.5f, 24f), new Vector2(-10.5f, 26.7f), new Vector2(-15.5f, 26.7f), new Vector2(-15.5f, 24f), new Vector2(-13.3f, 24f), new Vector2(-13.3f, 22f), new Vector2(-19f, 22f), new Vector2(-19f, 26.7f), new Vector2(-25.3f, 26.7f), new Vector2(-25.3f, 15.95f),
                            new Vector2(-23f,15.95f), new Vector2(-23f, 13f), new Vector2(-25.3f, 13f), new Vector2(-25.3f, 2.5f), new Vector2(-19f, 2.5f), new Vector2(-19f, 6.7f), new Vector2(-13.3f, 6.7f), new Vector2(-13.3f, 4.8f), new Vector2(-16.05f, 4.8f), new Vector2(-16.05f, 4.025f), new Vector2(-10.75f, 4.025f), new Vector2(-10.75f, 3.25f), 
                            new Vector2(-16.05f, 3.25f), new Vector2(-16.05f, 2.15f), new Vector2(-9.65f, 2.15f), new Vector2(-9.65f, 4.8f), new Vector2(-11.65f, 4.8f), new Vector2(-11.65f, 6.7f), new Vector2(-6.25f, 6.7f), new Vector2(-6.25f, 3f)
                        };
                    Wall.gameObject.AddComponent<EdgeCollider2D>().points =
                       new Vector2[] { new Vector2(-5f, 4f), new Vector2(9f, 4f), new Vector2(9f, 6f), new Vector2(6.8f, 6f), new Vector2(6.8f, 8f), new Vector2(5.3f, 8f), new Vector2(5.3f, 7f), new Vector2(2.3f, 7f), new Vector2(2.3f, 11f), new Vector2(4.5f, 11f), new Vector2(5.3f, 10f), new Vector2(5.3f, 9.25f), new Vector2(6.8f, 9.25f), new Vector2(6.8f, 12.5f), new Vector2(10.8f, 12.5f), new Vector2(10.8f, 13.675f), new Vector2(6.875f, 13.675f), new Vector2(6.875f, 14.4f), new Vector2(5.8f, 14.4f), new Vector2(5.8f, 14.4f), new Vector2(5.8f, 19.5f), new Vector2(4f, 19.5f),
                            new Vector2(4f, 14.3f), new Vector2(-2.8f, 14.3f), new Vector2(-2.8f, 17.8f), new Vector2(-5f, 17.8f), new Vector2(-5f, 9.2f), new Vector2(-3f, 9.2f), new Vector2(-3f, 10.2f), new Vector2(-2.5f, 11f), new Vector2(0f, 11f), new Vector2(0f, 7f), new Vector2(-3f, 7f), new Vector2(-3f, 8f), new Vector2(-5f, 8f), new Vector2(-5f, 4f)
                        };
                    //会議室左上
                    Wall.gameObject.AddComponent<EdgeCollider2D>().points =
                           new Vector2[] {
                               new Vector2(-4.9f, 19.3f),new Vector2(-4.9f, 25.25f),new Vector2(0f, 25.25f),new Vector2(0f, 24.5f),new Vector2(0.25f, 24.5f),new Vector2(0.25f, 23.5f),new Vector2(0f, 23.5f), new Vector2(-0.75f, 23.5f),new Vector2(-2.8f, 21.4f),new Vector2(-2.8f, 19.3f),new Vector2(-5f, 19.3f)
                           };
                    //会議室右上
                    Wall.gameObject.AddComponent<EdgeCollider2D>().points =
                           new Vector2[] {
                               new Vector2(1.4f, 23.5f),new Vector2(1.4f, 24.5f),new Vector2(1.6f, 24.5f),new Vector2(1.6f, 25.25f),new Vector2(9.3f, 25.25f),new Vector2(9.5f, 25.25f),new Vector2(9.5f, 23.5f),new Vector2(5.8f, 23.5f),new Vector2(5.8f, 20f),new Vector2(7f, 20f),new Vector2(7f, 19f),new Vector2(4f, 19f),new Vector2(4f, 19f),new Vector2(4f, 23.5f),new Vector2(1.5f, 23.5f)
                           };
                    //左側
                    Wall.gameObject.AddComponent<EdgeCollider2D>().points =
                           new Vector2[] {
                               new Vector2(-6.15f, 9f),new Vector2(-11.65f, 9f),new Vector2(-11.65f, 10.5f),new Vector2(-10f, 10.5f),new Vector2(-10f, 13f),new Vector2(-16f, 13f),new Vector2(-16f, 10.5f),new Vector2(-13.45f, 10.5f),new Vector2(-13.45f, 9f),new Vector2(-19f, 9f),
                               new Vector2(-19f, 13f),new Vector2(-21.7f, 13f),new Vector2(-21.7f, 15.95f), new Vector2(-19f, 15.95f), new Vector2(-19f, 20.25f), new Vector2(-18.6f, 20.25f), new Vector2(-18.6f, 19.75f), new Vector2(-13.45f, 19.75f), new Vector2(-13.45f, 18.5f), new Vector2(-16f, 18.5f), new Vector2(-16f, 15.8f), new Vector2(-10f, 15.8f),
                               new Vector2(-10f, 18.5f), new Vector2(-11.65f, 18.5f), new Vector2(-11.65f, 19.75f), new Vector2(-6.15f, 19.75f),new Vector2(-6.15f, 9f)
                           };

                    //倉庫階段右1
                    Wall.gameObject.AddComponent<EdgeCollider2D>().points =
                           new Vector2[] {
                               new Vector2(-21.4f,13f),new Vector2(-21.4f,12.55f),new Vector2(-21f,12.55f),new Vector2(-20.2f,12.8f),new Vector2(-20.2f,12.9f),new Vector2(-19f,12.9f)
                           };
                    //倉庫階段右2
                    Wall.gameObject.AddComponent<EdgeCollider2D>().points =
                           new Vector2[] {
                               new Vector2(-19f,3.35f),new Vector2(-20.2f,3.35f),new Vector2(-20.2f,0f),new Vector2(-20.2f,3.9f),new Vector2(-21f,3.65f),new Vector2(-21.4f,3.65f)
                           };
                    //倉庫階段右3
                    Wall.gameObject.AddComponent<EdgeCollider2D>().points =
                           new Vector2[] {
                               new Vector2(-21.4f,4.8f),new Vector2(-21f,4.8f),new Vector2(-20.2f,5.05f),new Vector2(-20.2f,11.6f), new Vector2(-21f,11.45f),new Vector2(-21.4f,11.45f),
                           };

                    SuperNewRolesPlugin.Logger.LogInfo("オールドア:" + ShipStatus.Instance.AllDoors.Length);

                    SpriteRenderer CafeteriaWalls = Wall.FindChild("CafeteriaWalls").gameObject.GetComponent<SpriteRenderer>();
                    //CafeteriaWalls.sprite = Agartha.ImageManager.Room_Meeting;
                    CafeteriaWalls.transform.position = new Vector3(13.15f, 16f, 4f);
                    CafeteriaWalls.transform.localScale *= 1.5f;
                    /*
                    SpriteRenderer AdminWalls = ShipStatus.Instantiate(CafeteriaWalls).gameObject.GetComponent<SpriteRenderer>();
                    AdminWalls.name = "AdminWalls";
                    AdminWalls.sprite = Agartha.ImageManager.Room_Security;
                    AdminWalls.transform.position = new Vector3(10.97f, 5.87f, 4f);
                    AdminWalls.transform.localScale *= 0.7875f;
                    AdminWalls.transform.localScale = new Vector3(AdminWalls.transform.localScale.x * -1, AdminWalls.transform.localScale.y, AdminWalls.transform.localScale.z);

                    SpriteRenderer SecurityWalls = ShipStatus.Instantiate(CafeteriaWalls).gameObject.GetComponent<SpriteRenderer>();
                    SecurityWalls.name = "SecurityWalls";
                    SecurityWalls.sprite = Agartha.ImageManager.Room_Security;
                    SecurityWalls.transform.position = new Vector3(16.6f, 5.87f, 4f);
                    //SecurityWalls.transform.localScale *= 0.75f;
                    SecurityWalls.transform.localScale *= 0.7875f;

                    SpriteRenderer WorkRoomWalls = ShipStatus.Instantiate(CafeteriaWalls).gameObject.GetComponent<SpriteRenderer>();
                    WorkRoomWalls.name = "WorkRoomWalls";
                    WorkRoomWalls.sprite = Agartha.ImageManager.Room_WorkRoom;
                    WorkRoomWalls.transform.position = new Vector3(-9.5f, 17.8f, 4f);
                    WorkRoomWalls.transform.localScale = new Vector3(1.005f, 1.097f, 1.005f);

                    SpriteRenderer WareHouseWalls = ShipStatus.Instantiate(CafeteriaWalls).gameObject.GetComponent<SpriteRenderer>();
                    WareHouseWalls.name = "WareHouseWalls";
                    WareHouseWalls.sprite = Agartha.ImageManager.Room_WareHouse;
                    WareHouseWalls.transform.position = new Vector3(-9.65f, 4.8f, 4f);
                    WareHouseWalls.transform.localScale = new Vector3(2.1f, 2.3f, 2.1f);

                    SpriteRenderer CommsRoomWalls = ShipStatus.Instantiate(CafeteriaWalls).gameObject.GetComponent<SpriteRenderer>();
                    CommsRoomWalls.name = "CommsRoomWalls";
                    CommsRoomWalls.sprite = Agartha.ImageManager.Room_Up;
                    CommsRoomWalls.transform.position = new Vector3(-0.35f, 22.32f, 4f);
                    CommsRoomWalls.transform.localScale = new Vector3(1.41f, 1.35f, 1.35f);

                    SpriteRenderer O2RoomWalls = ShipStatus.Instantiate(CafeteriaWalls).gameObject.GetComponent<SpriteRenderer>();
                    O2RoomWalls.name = "O2RoomWalls";
                    O2RoomWalls.sprite = Agartha.ImageManager.Room_Up;
                    O2RoomWalls.transform.position = new Vector3(-0.4f, 8.7f, 4f);
                    O2RoomWalls.transform.localScale = new Vector3(1.7f, 1.25f, 1.35f);

                    SpriteRenderer MedBayRoomWalls = ShipStatus.Instantiate(CafeteriaWalls).gameObject.GetComponent<SpriteRenderer>();
                    MedBayRoomWalls.name = "MedBayRoomWalls";
                    MedBayRoomWalls.sprite = Agartha.ImageManager.Room_Down;
                    MedBayRoomWalls.transform.position = new Vector3(-0.4f, 13.35f, 4f);
                    MedBayRoomWalls.transform.localScale = new Vector3(1.7f, 1.3f, 1.35f);

                    SpriteRenderer ToolRoomWalls = ShipStatus.Instantiate(CafeteriaWalls).gameObject.GetComponent<SpriteRenderer>();
                    ToolRoomWalls.name = "ToolRoomWalls";
                    ToolRoomWalls.sprite = Agartha.ImageManager.Room_Down;
                    ToolRoomWalls.transform.position = new Vector3(-0.3f, -0.25f, 4f);
                    ToolRoomWalls.transform.localScale = new Vector3(1.75f, 1.2f, 1.35f);

                    SpriteRenderer ElectricalRoomWalls = ShipStatus.Instantiate(CafeteriaWalls).gameObject.GetComponent<SpriteRenderer>();
                    ElectricalRoomWalls.name = "ElectricalRoomWalls";
                    ElectricalRoomWalls.sprite = Agartha.ImageManager.AgarthagetSprite("Room_Electrical");
                    ElectricalRoomWalls.transform.position = new Vector3(21.5f, 16f, 4f);
                    ElectricalRoomWalls.transform.localScale = new Vector3(1.5f, 1.23f, 1.35f);

                    SpriteRenderer LaboRoomWalls = ShipStatus.Instantiate(CafeteriaWalls).gameObject.GetComponent<SpriteRenderer>();
                    LaboRoomWalls.name = "LaboRoomWalls";
                    LaboRoomWalls.sprite = Agartha.ImageManager.AgarthagetSprite("Room_Labo");
                    LaboRoomWalls.transform.position = new Vector3(22.075f, 6.6f, 4f);
                    LaboRoomWalls.transform.localScale = new Vector3(0.379f, 0.4255f, 0.405f);


                    SpriteRenderer Aisle1Walls = ShipStatus.Instantiate(CafeteriaWalls).gameObject.GetComponent<SpriteRenderer>();
                    Aisle1Walls.name = "Aisle1Walls";
                    Aisle1Walls.sprite = Agartha.ImageManager.AgarthagetSprite("Aisle1");
                    Aisle1Walls.transform.position = new Vector3(2.8f, 18.6f, 5f);
                    Aisle1Walls.transform.localScale *= 2.6f;

                    SpriteRenderer Aisle2Walls = ShipStatus.Instantiate(Aisle1Walls).gameObject.GetComponent<SpriteRenderer>();
                    Aisle2Walls.name = "Aisle2Walls";
                    Aisle2Walls.sprite = Agartha.ImageManager.AgarthagetSprite("Aisle1");
                    Aisle2Walls.transform.position = new Vector3(3.26f, 5.73f, 5f);
                    Aisle2Walls.transform.localScale *= 1.1f;

                    SpriteRenderer Aisle3Walls = ShipStatus.Instantiate(Aisle1Walls).gameObject.GetComponent<SpriteRenderer>();
                    Aisle3Walls.name = "Aisle3Walls";
                    Aisle3Walls.sprite = Agartha.ImageManager.AgarthagetSprite("Aisle3");
                    Aisle3Walls.transform.position = new Vector3(17f, -0.35f, 5f);
                    Aisle3Walls.transform.localScale = new Vector3(2.7f, 2.1f, 5f);

                    SpriteRenderer Aisle4Walls = ShipStatus.Instantiate(Aisle1Walls).gameObject.GetComponent<SpriteRenderer>();
                    Aisle4Walls.name = "Aisle4Walls";
                    Aisle4Walls.sprite = Agartha.ImageManager.AgarthagetSprite("Aisle4");
                    Aisle4Walls.transform.position = new Vector3(9.67f, 19.15f, 4.99f);
                    Aisle4Walls.transform.localScale = new Vector3(0.6f, 0.445f, 1.4f);

                    SpriteRenderer Aisle5Walls = ShipStatus.Instantiate(Aisle1Walls).gameObject.GetComponent<SpriteRenderer>();
                    Aisle5Walls.name = "Aisle5Walls";
                    Aisle5Walls.sprite = Agartha.ImageManager.AgarthagetSprite("Aisle5");
                    Aisle5Walls.transform.position = new Vector3(13f, 13.475f, 4.99f);
                    Aisle5Walls.transform.localScale = new Vector3(3.9f, 5f, 3.9f);
                    */
                    SpriteRenderer BackGroundWalls = ShipStatus.Instantiate(CafeteriaWalls).gameObject.GetComponent<SpriteRenderer>();
                    BackGroundWalls.name = "BackGroundWalls";
                    BackGroundWalls.sprite = Agartha.ImageManager.AgarthagetSprite("BackGround");
                    BackGroundWalls.transform.position = new Vector3(2.8f, 18.6f, 100f);
                    BackGroundWalls.transform.localScale *= 100000f;
                    
                    SpriteRenderer Walls = ShipStatus.Instantiate(CafeteriaWalls, MiraShip).gameObject.GetComponent<SpriteRenderer>();
                    Walls.name = "WallImages";
                    Walls.sprite = Agartha.ImageManager.Map;
                    Walls.transform.position = new Vector3(4.95f, 12.5f, 7f);
                    Walls.transform.localScale = new Vector3(1.81f,1.81f,1.81f);

                    GameObject.Destroy(CafeteriaWalls);

                    MiraShip.FindChild("CloudGen").gameObject.SetActive(false);

                    Transform CafeObject = MiraShip.FindChild("Cafe");
                    CafeObject.gameObject.GetChildren().SetActiveAllObject("Table", false);
                    CafeObject.FindChild("BalconyVent").gameObject.SetActive(true);
                    GameObject.Destroy(CafeObject.gameObject.GetComponent<EdgeCollider2D>());
                    Transform CafeObject_Table = CafeObject.FindChild("Table");
                    CafeObject_Table.gameObject.GetChildren().SetActiveAllObject("EmergencyConsole", false);
                    CafeObject_Table.GetComponent<SpriteRenderer>().sprite = Agartha.ImageManager.Object_Table1;
                    GameObject.Destroy(CafeObject_Table.GetComponent<BoxCollider2D>());
                    GameObject.Destroy(CafeObject_Table.GetComponent<PolygonCollider2D>());
                    CafeObject_Table.gameObject.AddComponent<PolygonCollider2D>();
                    CafeObject_Table.transform.localPosition = new Vector3(0.25f, -2f, 0.05f);
                    CafeObject_Table.transform.localScale = new Vector3(1.75f, 1.75f, 1.75f);
                    var emconsole = CafeObject_Table.FindChild("EmergencyConsole").transform;
                    emconsole.position = new Vector3(13.55f, 14.15f, 0f);
                    emconsole.localPosition = new Vector3(0,0,0);
                    emconsole.localScale = new Vector3(0.9f,0.9f,0.9f);
                    //CafeObject.position = new Vector3(1000, 1000, 1000);
                    Transform SkyBri = MiraShip.FindChild("SkyBridge");
                    GameObject.Destroy(SkyBri.GetComponent<PolygonCollider2D>());
                    foreach (EdgeCollider2D edge in SkyBri.GetComponents<EdgeCollider2D>())
                    {
                        GameObject.Destroy(edge);
                    }
                    Transform Garden = MiraShip.FindChild("Garden");
                    GameObject.Destroy(Garden.GetComponent<BoxCollider2D>());
                    foreach (EdgeCollider2D edge in Garden.GetComponents<EdgeCollider2D>())
                    {
                        GameObject.Destroy(edge);
                    }
                    Transform Dec = MiraShip.FindChild("Decontam");
                    foreach (EdgeCollider2D edge in Dec.GetComponents<EdgeCollider2D>())
                    {
                        GameObject.Destroy(edge);
                    }
                    foreach (BoxCollider2D box in Dec.GetComponents<BoxCollider2D>())
                    {
                        GameObject.Destroy(box);
                    }
                    Transform Launch = MiraShip.FindChild("LaunchPad");
                    GameObject.Destroy(Launch.GetComponent<BoxCollider2D>());
                    GameObject.Destroy(Launch.GetComponent<EdgeCollider2D>());
                    SuperNewRolesPlugin.Logger.LogInfo("きゃあ");
                    Transform MedBay = MiraShip.FindChild("MedBay");

                    Launch.gameObject.GetChildren().SetActiveAllObject("LaunchVent",false);
                    MiraShip.FindChild("LeftBottomRoom").gameObject.SetActive(false);
                    Dec.gameObject.GetChildren().SetActiveAllObject("DeconVent",false);
                    MedBay.gameObject.GetChildren().SetActiveAllObject("MedScanner", false);
                    MedBay.FindChild("MedVent").gameObject.SetActive(true);
                    MiraShip.FindChild("Storage").gameObject.SetActive(false);
                    MiraShip.FindChild("Comms").gameObject.GetChildren().SetActiveAllObject("comms-top", false);
                    SkyBri.gameObject.GetChildren().SetActiveAllObject("FixWiringConsole (2)", false);
                    SkyBri.FindChild("YHallRightVent").gameObject.SetActive(true);
                    Garden.gameObject.GetChildren().SetActiveAllObject("FixWiringConsole", false);
                    Garden.FindChild("AgriVent").gameObject.SetActive(true);
                    MiraShip.FindChild("Laboratory").gameObject.GetChildren().SetActiveAllObject("LabVent",false);
                    MiraShip.FindChild("LabHall").gameObject.GetChildren().SetActiveAllObject("FixWiringConsole", false);
                    MiraShip.FindChild("Walls").gameObject.GetChildren().SetActiveAllObject("CafeteriaWalls", false);
                    MiraShip.FindChild("Admin").gameObject.GetChildren().SetActiveAllObject("MapTable", false);
                    MiraShip.FindChild("Admin").FindChild("AdminVent").gameObject.SetActive(true);
                    GameObject.Destroy(MiraShip.FindChild("LabHall").gameObject.GetComponent<EdgeCollider2D>());
                    MiraShip.FindChild("Balcony").gameObject.SetActive(false);
                    MiraShip.FindChild("Office").gameObject.GetChildren().SetActiveAllObject("OfficeVent", false);
                    //MiraShip.FindChild("Office").gameObject.SetActive(false);

                    Agartha.Patch.SetPosition.SetVent(MiraShip);
                    Agartha.Patch.SetTasksClass.SetTasks(MiraShip);
                    Agartha.Patch.SetPosition.SetObject(MiraShip);

                    Transform Admin = MiraShip.FindChild("Admin");
                    Transform AdminMapTable = Admin.FindChild("MapTable");
                    AdminMapTable.GetComponent<SpriteRenderer>().sprite = Agartha.ImageManager.Admin_Table;
                    AdminMapTable.position = new Vector3(11.85f, 6.95f, 4f);
                    Transform AdminMapConsole = AdminMapTable.FindChild("AdminMapConsole");
                    AdminMapConsole.GetComponent<SpriteRenderer>().sprite = Agartha.ImageManager.Admin_Table;
                    AdminMapConsole.position = AdminMapTable.position;
                    Transform AdminEnterCodeConsole = AdminMapTable.FindChild("EnterCodeConsole");
                    AdminEnterCodeConsole.position = new Vector3(11.1f, 7f, 3f);
                    AdminEnterCodeConsole.GetComponent<SpriteRenderer>().sprite = Agartha.ImageManager.AgarthagetSprite("Task_EnterCode");
                    GameObject.Destroy(AdminMapTable.gameObject.GetComponent<PolygonCollider2D>());
                    AdminMapTable.gameObject.AddComponent<PolygonCollider2D>();
                    //AdminMapConsole.position = new Vector3(2.573f, -2, 0);

                    if (AmongUsClient.Instance.GameMode == GameModes.FreePlay)
                    {
                        MiraShip.FindChild("TaskAddConsole").position = new Vector3(12.7f, 13.5f, 0f);
                        Agartha.Patch.SetPosition.SetDummy();
                    }
                    Agartha.Patch.ChangeRoomArea.Change(MiraShip);
                    Agartha.Patch.SetPosition.ObjectsDestroy(MiraShip);
                    Agartha.Patch.CreateShadow.Create(MiraShip);
                    Agartha.Patch.SetTasksClass.SetSabotage(MiraShip);
                    Agartha.Patch.ChangeMapCounter.Change(MiraShip);
                    Agartha.Patch.SetPosition.SetDoor();
                    Agartha.Patch.SetTasksClass.ShipSetTask();

                    if (AmongUsClient.Instance.GameMode != GameModes.FreePlay && CustomOptions.enableMirroMap.getBool())
                    {
                        SuperNewRolesPlugin.Logger.LogInfo("a");
                        ShipStatus.Instance.MeetingSpawnCenter = new Vector2(-12.9f, 15.7f);
                        ShipStatus.Instance.InitialSpawnCenter = new Vector2(-12.9f, 15.7f);
                        ShipStatus.Instance.transform.localScale = new Vector3(-1f, 1f, 1f);
                        ShipStatus.Instance.SpawnRadius = 1.5f;

                    } else
                    {
                        SuperNewRolesPlugin.Logger.LogInfo("b");
                        ShipStatus.Instance.MeetingSpawnCenter = new Vector2(12.8f, 15.6f);
                        ShipStatus.Instance.InitialSpawnCenter = new Vector2(12.8f, 15.6f);
                        ShipStatus.Instance.SpawnRadius = 1.5f;
                    }
                }
            }
        }
    }
}
