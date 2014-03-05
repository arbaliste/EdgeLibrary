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
    //A very small class for sprites that do not want to be drawn - could be used to change a particle emitter's color...
    public class FakeSprite : Sprite
    {
        public FakeSprite() : base("", Vector2.Zero) { }

        protected override void drawElement(GameTime gameTime) { }
    }
}
