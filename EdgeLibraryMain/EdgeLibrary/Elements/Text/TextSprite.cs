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
    //Provides a base game object
    public class TextSprite : Sprite
    {
        public SpriteFont Font { get; set; }
        public string Text { get { return _text; } set { _text = value; reloadBoundingBox(); } }
        //WARNING: Setting this to false unaligns the collision body from what is drawn
        public bool CenterText { get; set; }
        protected string _text;

        public TextSprite(string id, string eFontName, string eText, Vector2 ePosition, Color eColor) : base(id, "", ePosition)
        {
            CenterText = true;

            Font = ResourceManager.getFont(eFontName);

            Style.Effects = SpriteEffects.None;
            _position = ePosition;

            _text = eText;

            Style.Color = eColor;
            Style.Rotation = 0;
            Scale = Vector2.One;

            if (eFontName != null)
            {
                Font = ResourceManager.getFont(eFontName);
            }

            reloadBoundingBox();
        }

        public TextSprite(string id, string eFontName, string eText, Vector2 ePosition, Color eColor, float eRotation, Vector2 eScale): this(id, eFontName, eText, ePosition, eColor)
        {
            Style.Rotation = eRotation;
            Scale = eScale;
        }

        public override void reloadBoundingBox()
        {
            if (Font != null)
            {
                Vector2 Measured = Font.MeasureString(_text);
                BoundingBox = new Rectangle((int)(Position.X - Measured.X / 2), (int)(Position.Y - Measured.Y / 2), (int)Measured.X, (int)Measured.Y);
                _width = BoundingBox.Width;
                _height = BoundingBox.Height;
            }
        }

        protected override void UpdateCollision()
        {
            if (EdgeGame.CollisionsInTextSprites)
            {
                base.UpdateCollision();
            }
        }

        protected Vector2 MeasuredPosition()
        {
            if (Font != null)
            {
                if (!CenterText)
                {
                    return new Vector2(Position.X, Position.Y - Font.MeasureString(_text).Y / 2); 
                }
                    return new Vector2(Position.X - Font.MeasureString(_text).X / 2, Position.Y - Font.MeasureString(_text).Y / 2);
            }
            return Position;
        }

        protected override void drawElement(GameTime gameTime)
        {
            EdgeGame.drawString(Font, Text, MeasuredPosition(), Style.Color, Style.Rotation, OriginPoint, Scale, Style.Effects);
        }
    }
}
