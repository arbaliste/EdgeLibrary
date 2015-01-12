﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EdgeDemo.CheckersGame
{
    public class ButtonMultiToggle : Button
    {
        public bool On = false;

        public List<Style> Styles;
        public int CurrentIndex;
        public int MaxIndices;

        public ButtonMultiToggle(string texture, Vector2 position, int maxIndices)
            : base(texture, position)
        {
            Styles = new List<Style>() { Style };

            MaxIndices = maxIndices;
            CurrentIndex = 0;

            OnClick += new ButtonEvent(ButtonToggle_OnClick);
        }

        void ButtonToggle_OnClick(Button sender, GameTime gameTime)
        {
        }
    }
}
