using SuperNewRoles.CustomOption;

namespace SuperNewRoles.MapCustoms
{
    class MapCustom
    {
        public static CustomOption.CustomOption MapCustomOption;

        public static CustomOption.CustomOption SkeldSetting;//スケルド


        public static CustomOption.CustomOption MiraSetting;//ミラ
        public static CustomOption.CustomOption MiraAdditionalVents;
        public static CustomOption.CustomOption AddVitalsMira;


        public static CustomOption.CustomOption PolusSetting;//ポーラス
        public static CustomOption.CustomOption PolusAdditionalVents;
        public static CustomOption.CustomOption SpecimenVital;


        public static CustomOption.CustomOption AirshipSetting;//エアーシップ
        public static CustomOption.CustomOption SecretRoomOption;
        public static CustomOption.CustomOption AirShipAdditionalVents;
        public static CustomOption.CustomOption AirshipDisableMovingPlatform;
        public static CustomOption.CustomOption RecordsAdminDestroy;
        public static CustomOption.CustomOption MoveElecPad;
        public static CustomOption.CustomOption AddWireTask;
        public static CustomOption.CustomOption AirshipRandomSpawn;
        public static CustomOption.CustomOption AirshipInitialDoorCooldown;
        public static CustomOption.CustomOption AirshipInitialSabotageCooldown;
        public static CustomOption.CustomOption AirshipAdditionalSpawn;
        public static CustomOption.CustomOption AirshipSynchronizedSpawning;


        public static void CreateOption()
        {
            MapCustomOption = CustomOption.CustomOption.Create(623, false, CustomOptionType.Generic, "MapCustom", false, null, true);

            /*===============スケルド===============*/
            SkeldSetting = CustomOption.CustomOption.Create(624, false, CustomOptionType.Generic, "<color=#8fbc8f>Skeld</color>", false, MapCustomOption);

            /*===============ミラ===============*/
            MiraSetting = CustomOption.CustomOption.Create(660, false, CustomOptionType.Generic, "<color=#cd5c5c>Mira</color>", false, MapCustomOption);
            MiraAdditionalVents = CustomOption.CustomOption.Create(631, false, CustomOptionType.Generic, "MiraAdditionalVents", false, MiraSetting);
            AddVitalsMira = CustomOption.CustomOption.Create(472, false, CustomOptionType.Generic, "AddVitalsMiraSetting", false, MiraSetting);

            /*===============ポーラス===============*/
            PolusSetting = CustomOption.CustomOption.Create(661, false, CustomOptionType.Generic, "<color=#4b0082>Polus</color>", false, MapCustomOption);
            PolusAdditionalVents = CustomOption.CustomOption.Create(662, false, CustomOptionType.Generic, "PolusAdditionalVents", false, PolusSetting);
            SpecimenVital = CustomOption.CustomOption.Create(613, false, CustomOptionType.Generic, "SpecimenVitalSetting", false, PolusSetting);

            /*===============エアーシップ===============*/
            AirshipSetting = CustomOption.CustomOption.Create(663, false, CustomOptionType.Generic, "<color=#ff0000>Airship</color>", false, MapCustomOption);
            SecretRoomOption = CustomOption.CustomOption.Create(664, false, CustomOptionType.Generic, "SecretRoom", false, AirshipSetting);
            AirShipAdditionalVents = CustomOption.CustomOption.Create(605, false, CustomOptionType.Generic, "AirShipAdditionalVents", false, AirshipSetting);
            AirshipDisableMovingPlatform = CustomOption.CustomOption.Create(665, false, CustomOptionType.Generic, "AirshipDisableMovingPlatformSetting", false, AirshipSetting);
            RecordsAdminDestroy = CustomOption.CustomOption.Create(612, false, CustomOptionType.Generic, "RecordsAdminDestroySetting", false, AirshipSetting);
            MoveElecPad = CustomOption.CustomOption.Create(645, false, CustomOptionType.Generic, "MoveElecPadSetting", false, AirshipSetting);
            AddWireTask = CustomOption.CustomOption.Create(646, false, CustomOptionType.Generic, "AddWireTaskSetting", false, AirshipSetting);
            AirshipAdditionalSpawn = CustomOption.CustomOption.Create(9917, false, CustomOptionType.Generic, "airshipAdditionalSpawn", false, AirshipSetting);
            AirshipSynchronizedSpawning = CustomOption.CustomOption.Create(9918, false, CustomOptionType.Generic, "airshipSynchronizedSpawning", false, AirshipSetting);
            AirshipInitialDoorCooldown = CustomOption.CustomOption.Create(9923, false, CustomOptionType.Generic, "airshipInitialDoorCooldown", 0f, 0f, 60f, 1f, AirshipSetting);
            AirshipInitialSabotageCooldown = CustomOption.CustomOption.Create(9924, false, CustomOptionType.Generic, "airshipInitialSabotageCooldown", 15f, 0f, 60f, 1f, AirshipSetting);
        }
    }
}
