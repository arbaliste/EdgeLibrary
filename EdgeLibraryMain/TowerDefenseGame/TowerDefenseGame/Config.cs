﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using EdgeLibrary;

namespace TowerDefenseGame
{
    public static class Config
    {

        //Variables must be BEFORE lists, for some reason... otherwise, they end up null. Nobody knows why.
        //START VARIABLES
        public static string TrackEasyDifficulty = "Easy";
        public static string TrackMediumDifficulty = "Medium";
        public static string TrackHardDifficulty = "Hard";

        public static string EasyDifficulty = "Easy";
        public static string MediumDifficulty = "Medium";
        public static string HardDifficulty = "Hard";

        public static string StatusFont = "ComicSans-20";
        public static string BigStatusFont = "ComicSans-40";
        public static string SquareFont = "ComicSans-20";
        public static string DebugFont = "Impact-20";

        public static string MenuTitleFont = "Georgia-50";
        public static string MenuMiniTitleFont = "Georgia-30";
        public static string MenuSubtitleFont = "Georgia-20";
        public static string MenuButtonTextFont = "Georgia-20";
        public static Color MenuButtonColor = Color.DarkOrange;
        public static Color MenuTextColor = Color.Orange;

        public static string WaypointsXMLName = "Waypoints";
        public static string ObjectsXMLName = "DecorationCollision";
        public static string PathXMLName = "Path";
        public static string WaterXMLName = "Water";

        public static string ButtonNormalTexture = "grey_button03";
        public static string ButtonClickTexture = "grey_button02";
        public static string ButtonMouseOverTexture = "grey_button01";

        public static float CameraZoomSpeed = 1;
        public static float CameraMaxZoom = 10f;
        public static float CameraMinZoom = 300f;
        public static float CameraScrollSpeed = 10f;

        public static Keys BackKey = Keys.Escape;

        public static float[] EnemyHealthMultiplier = new float[] { 0.5f, 1f, 2f };
        public static float[] TowerCostMultiplier = new float[] { 0.75f, 1f, 1.25f };
        public static int[] LivesNumber = new int[] { 25, 10, 1 };
        public static int[] StartingMoneyNumber = new int[] { 600, 550, 500 };
        //END VARIABLES

        ////START LISTS
        public static List<EnemyType> Enemies = new List<EnemyType>()
        {
            new EnemyType(50, 1, 0, 1, new List<EnemyType>(), "NormalEnemy", Vector2.One, "Just a normal enemy.", null)
        };

        public static List<Round> RoundList = new List<Round>()
        {
            new Round(new Dictionary<EnemyType,float>() {{Enemies[0], 5}})
        };

        public static List<TowerData> Towers = new List<TowerData>()
        {
            new TowerData(1, 10, 0, 300, Projectiles[0], "", Vector2.One, 200, "Just a normal tower.", null)
        };

        public static List<ProjectileData> Projectiles = new List<ProjectileData>()
        {
            new ProjectileData(1000, 1000, 0, 1, null)
        };
        //END LISTS
    }
}
