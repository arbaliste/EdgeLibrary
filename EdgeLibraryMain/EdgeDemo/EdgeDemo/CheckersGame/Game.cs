﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using EdgeLibrary;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace EdgeDemo.CheckersGame
{
    public class Game
    {
        /* TODO:
         * -Add Documentation
         * -Singleplayer
         *      -Fix movement generation
         *      -Add jumping
         *          -Add double jumping
         *      -If possible to jump, only allow jumps
         * -Multiplayer
         */

        public void OnInit()
        {
            EdgeGame.InitializeWorld(new Vector2(0, 9.8f));

            EdgeGame.WindowSize = new Vector2(1000);

            EdgeGame.GameSpeed = 1;

            EdgeGame.ClearColor = Color.Gray;

            EdgeGame.IsShuffled = true;

            EdgeGame.playPlaylist("Music");

            BoardManager manager = new BoardManager();
            manager.AddToGame();

        }

        public void OnUpdate(GameTime gameTime)
        {
        }

        public void OnDraw(GameTime gameTime)
        {
        }

        public void OnLoadContent()
        {
            EdgeGame.LoadFont("Fonts/Comic Sans/ComicSans-10");
            EdgeGame.LoadFont("Fonts/Comic Sans/ComicSans-20");
            EdgeGame.LoadFont("Fonts/Comic Sans/ComicSans-30");
            EdgeGame.LoadFont("Fonts/Comic Sans/ComicSans-40");
            EdgeGame.LoadFont("Fonts/Comic Sans/ComicSans-50");
            EdgeGame.LoadFont("Fonts/Comic Sans/ComicSans-60");
            EdgeGame.LoadFont("Fonts/Courier New/CourierNew-10");
            EdgeGame.LoadFont("Fonts/Courier New/CourierNew-20");
            EdgeGame.LoadFont("Fonts/Courier New/CourierNew-30");
            EdgeGame.LoadFont("Fonts/Courier New/CourierNew-40");
            EdgeGame.LoadFont("Fonts/Courier New/CourierNew-50");
            EdgeGame.LoadFont("Fonts/Courier New/CourierNew-60");
            EdgeGame.LoadFont("Fonts/Georgia/Georgia-10");
            EdgeGame.LoadFont("Fonts/Georgia/Georgia-20");
            EdgeGame.LoadFont("Fonts/Georgia/Georgia-30");
            EdgeGame.LoadFont("Fonts/Georgia/Georgia-40");
            EdgeGame.LoadFont("Fonts/Georgia/Georgia-50");
            EdgeGame.LoadFont("Fonts/Georgia/Georgia-60");
            EdgeGame.LoadFont("Fonts/Impact/Impact-10");
            EdgeGame.LoadFont("Fonts/Impact/Impact-20");
            EdgeGame.LoadFont("Fonts/Impact/Impact-30");
            EdgeGame.LoadFont("Fonts/Impact/Impact-40");
            EdgeGame.LoadFont("Fonts/Impact/Impact-50");
            EdgeGame.LoadFont("Fonts/Impact/Impact-60");
            EdgeGame.LoadTexturesInSpritesheet("SpaceSheet", "SpaceSheet");
            EdgeGame.LoadTexturesInSpritesheet("ButtonSheet", "ButtonSheet");
            EdgeGame.LoadTexturesInSpritesheet("ParticleSheet", "ParticleSheet");

            EdgeGame.LoadTexture(Config.PieceTexture);
            EdgeGame.LoadTexture(Config.KingTexture);

            EdgeGame.LoadSong("Music/Carefree");
            EdgeGame.LoadSong("Music/Fig Leaf Times Two");
            EdgeGame.LoadSong("Music/Fun in a Bottle");
            EdgeGame.LoadSong("Music/Hyperfun");
            EdgeGame.LoadSong("Music/Run Amok");
            EdgeGame.LoadSong("Music/Wallpaper");
            EdgeGame.AddPlaylist("Music", "Carefree", "Fig Leaf Times Two", "Fun in a Bottle", "Hyperfun", "Run Amok", "Wallpaper");
        }

    }
}