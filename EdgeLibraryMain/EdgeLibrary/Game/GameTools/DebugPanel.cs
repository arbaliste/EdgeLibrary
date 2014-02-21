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
using System.Xml;

namespace EdgeLibrary
{
    public class DebugPanel : Panel
    {
        public SpriteFont Font { get { return _font; } set { _font = value; reloadTextSprites(); } }
        public Color DrawColor { get { return _drawColor; } set { _drawColor = value; reloadTextSprites(); } }
        private SpriteFont _font;
        private Color _drawColor;

        private float YDifference;

        private TextSprite MouseSprite;
        private TextSprite FPSSprite;
        private TextSprite ScenesSprite;
        private TextSprite ElementsSprite;
        private TextSprite KeysSprite;

        public DebugPanel(string fontName, Color drawColor) : base(MathTools.RandomID("debugPanel"))
        {
            MouseSprite = new TextSprite(string.Format("{0}_MouseSprite", ID), fontName, "Mouse Position: (0, 0)", Vector2.Zero, drawColor);
            AddElement(MouseSprite);

            FPSSprite = new TextSprite(string.Format("{0}_FPSSprite", ID), fontName, "FPS: 0", Vector2.Zero, drawColor);
            AddElement(FPSSprite);

            ScenesSprite = new TextSprite(string.Format("{0}_ScenesSprite", ID), fontName, "Scenes (0):", Vector2.Zero, drawColor);
            AddElement(ScenesSprite);

            ElementsSprite = new TextSprite(string.Format("{0}_ElementsSprite", ID), fontName, "Elements in entire game (0):", Vector2.Zero, drawColor);
            AddElement(ElementsSprite);

            KeysSprite = new TextSprite(string.Format("{0}_KeysSprite", ID), fontName, "Keys Pressed: NONE", Vector2.Zero, drawColor);
            AddElement(KeysSprite);

            Font = ResourceManager.getFont(fontName);
            DrawColor = drawColor;

            YDifference = Font.MeasureString("A").Y * 1.25f;

            reloadTextSpritesPosition();
        }

        private void reloadTextSpritesPosition()
        {
            MouseSprite.Position = new Vector2(_font.MeasureString(MouseSprite.Text).X /2 + 10, YDifference);
            MouseSprite.Position += Position;

            FPSSprite.Position = new Vector2(_font.MeasureString(FPSSprite.Text).X /2 + 10, YDifference * 2);
            FPSSprite.Position += Position;

            ScenesSprite.Position = new Vector2(_font.MeasureString(ScenesSprite.Text).X / 2 + 10, YDifference * 3);
            ScenesSprite.Position += Position;

            ElementsSprite.Position = new Vector2(_font.MeasureString(ElementsSprite.Text).X /2 + 10, YDifference * 4);
            ElementsSprite.Position += Position;

            KeysSprite.Position = new Vector2(_font.MeasureString(KeysSprite.Text).X /2 + 10, YDifference * 5);
            KeysSprite.Position += Position;
        }

        private void reloadTextSprites()
        {
            MouseSprite.Font = _font;
            MouseSprite.Color = _drawColor;
            FPSSprite.Font = _font;
            FPSSprite.Color = _drawColor;
            ScenesSprite.Font = _font;
            ScenesSprite.Color = _drawColor;
            ElementsSprite.Font = _font;
            ElementsSprite.Color = _drawColor;
            KeysSprite.Font = _font;
            KeysSprite.Color = _drawColor;
        }

        public override void updateElement(GameTime gameTime)
        {
            base.updateElement(gameTime);

            MouseSprite.Text = string.Format("Mouse Position: ({0}, {1})", InputManager.MousePos().X, InputManager.MousePos().Y);
            FPSSprite.Text = string.Format("FPS: {0}", FPSCounter.FPS);
            ElementsSprite.Text = string.Format("Elements in current scene ({0}):", EdgeGame.SelectedScene.elements.Count);
            foreach (Element element in EdgeGame.SelectedScene.elements)
            {
                ElementsSprite.Text += string.Format(" {0}, ", element.ID);
            }
            ScenesSprite.Text = string.Format("Scenes ({0}):", EdgeGame.Scenes.Count);
            foreach (Scene scene in EdgeGame.Scenes)
            {
                ScenesSprite.Text += string.Format(" {0}, ", scene.ID);
            }
            KeysSprite.Text = "Keys Pressed:";
            foreach (Keys k in InputManager.KeysPressed())
            {
                KeysSprite.Text += string.Format(" {0}, ", Convert.ToString(k));
            }
            reloadTextSpritesPosition();
        }
    }
}
