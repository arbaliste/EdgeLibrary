﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace EdgeLibrary
{
    //A sprite which, instead of drawing a texture, displays text
    public class TextSprite : Sprite
    {
        public SpriteFont Font { get { return _font; } set { _font = value; reloadOriginPoint(); } }
        private SpriteFont _font;
        public override float Width { get { return Font == null ? 0 : Font.MeasureString(Text).X; } protected set { } }
        public override float Height { get { return Font == null ? 0 : Font.MeasureString(Text).Y; } protected set { } }
        public string Text { get { return _text; } set { _text = value; reloadOriginPoint(); } }
        protected string _text;

        public TextSprite(string id, string eFontName, string text, Vector2 ePosition, Color eColor) : base(id, "", ePosition)
        {
            Font = ResourceManager.getFont(eFontName);

            _text = text;

            Style.Color = eColor;
            Style.Rotation = 0;
            Scale = Vector2.One;

            if (eFontName != null)
            {
                Font = ResourceManager.getFont(eFontName);
            }
        }

        protected override void reloadOriginPoint()
        {
            if (Font != null && _text != null)
            {
                if (_centerAsOrigin)
                {
                    Vector2 Measured = Font.MeasureString(_text);
                    OriginPoint = new Vector2(Measured.X / 2f, Measured.Y / 2f);
                }
                else
                {
                    OriginPoint = Vector2.Zero;
                }
            }
        }

        protected override void drawElement(GameTime gameTime)
        {
            EdgeGame.drawString(Font, Text, Position, Style.Color, Style.Rotation, OriginPoint, Scale, Style.Effects);
        }
    }
}