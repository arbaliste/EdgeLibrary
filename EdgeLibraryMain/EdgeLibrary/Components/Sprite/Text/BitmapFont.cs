﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace EdgeLibrary
{
    /// <summary>
    /// This class is used for BitmapFonts for use in BitmapTextSprites
    /// This is built to use the XML type exported by FontBuilder
    /// </summary>
    public class BitmapFont
    {
        public Texture2D FontTexture;
        public Dictionary<char, BitmapCharacter> Characters;
        public Dictionary<char, Dictionary<char, int>> KerningPairs;

        public BitmapFont(string xmlPath, Texture2D imageTexture)
        {
            FontTexture = imageTexture.Clone();

            string completePath = string.Format("{0}\\{1}.xml", EdgeGame.ContentRootDirectory, xmlPath);
            XDocument document = XDocument.Load(completePath);
            string[] rectangle;
            string[] offset;

            Characters = new Dictionary<char, BitmapCharacter>();
            KerningPairs = new Dictionary<char, Dictionary<char, int>>();
            foreach (XElement element in document.Root.Elements())
            {
                rectangle = ((string)element.Attribute("rect")).Split(' ');
                offset = ((string)element.Attribute("offset")).Split(' ');

                Characters.Add(((string)element.Attribute("code"))[0],
                    new BitmapCharacter(new Rectangle(Convert.ToInt32(rectangle[0]), Convert.ToInt32(rectangle[1]), Convert.ToInt32(rectangle[2]), Convert.ToInt32(rectangle[3])),
                        Convert.ToInt32((string)element.Attribute("width")),
                        new Vector2(Convert.ToInt32(offset[0]), Convert.ToInt32(offset[1]))
                        ));

                Dictionary<char, int> kerningPair = new Dictionary<char, int>();
                foreach (XElement subElement in element.Elements())
                {
                    kerningPair.Add(((string)subElement.Attribute("id"))[0], (int)subElement.Attribute("advance"));
                }
                if (kerningPair.Count > 0)
                {
                    KerningPairs.Add(((string)element.Attribute("code"))[0], kerningPair);
                }
            }
        }

        public Vector2 MeasureString(string text)
        {
            Vector2 size = Vector2.Zero;

            foreach (BitmapCharacter character in Characters.Values)
            {
                size = new Vector2(size.X + character.Width, size.Y < (character.Rectangle.Height + character.Offset.Y) ? (character.Rectangle.Height + character.Offset.Y) : size.Y);
            }

            return size;
        }

        public void DrawString(string text, Vector2 position, Color color, float rotation, Vector2 scale, SpriteEffects spriteEffects, int layerDepth, SpriteBatch spritebatch)
        {
            Vector2 currentOffset = Vector2.Zero;
            char previousCharacter = new char();

            foreach (char character in text.ToCharArray())
            {
                if (KerningPairs.Keys.Contains(previousCharacter) && KerningPairs[previousCharacter].Keys.Contains(character))
                {
                    spritebatch.Draw(FontTexture, new Rectangle((int)(position.X + currentOffset.X + Characters[character].Offset.X + KerningPairs[previousCharacter][character]), (int)(position.Y + currentOffset.Y + Characters[character].Offset.Y), (int)(Characters[character].Width * scale.X), (int)(Characters[character].Rectangle.Height * scale.Y)), Characters[character].Rectangle, color, rotation, Vector2.Zero, spriteEffects, layerDepth);
                    currentOffset = new Vector2(currentOffset.X + Characters[character].Width + KerningPairs[previousCharacter][character], currentOffset.Y);
                }
                else
                {
                    spritebatch.Draw(FontTexture, new Rectangle((int)(position.X + currentOffset.X + Characters[character].Offset.X), (int)(position.Y + currentOffset.Y + Characters[character].Offset.Y), (int)(Characters[character].Width * scale.X), (int)(Characters[character].Rectangle.Height * scale.Y)), Characters[character].Rectangle, color, rotation, Vector2.Zero, spriteEffects, layerDepth);
                    currentOffset = new Vector2(currentOffset.X + Characters[character].Width, currentOffset.Y);
                }
                previousCharacter = character;
            }
        }
    }

    public struct BitmapCharacter
    {
        public Rectangle Rectangle;
        public int Width;
        public Vector2 Offset;

        public BitmapCharacter(Rectangle rectangle, int width, Vector2 offset)
        {
            Rectangle = rectangle;
            Width = width;
            Offset = offset;
        }
    }
}
