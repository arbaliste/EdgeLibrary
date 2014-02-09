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
using EdgeLibrary.Basic;

namespace EdgeLibrary.Elements
{
    public class EParticle : ESprite
    {
        protected TimeSpan livedTime;

        protected float liveTime;
        protected Vector2 velocity;
        protected float rotateSpeed;
        protected float growSpeed;

        public bool shouldRemove;

        public EParticle(float eLifeTime, Vector2 eVelocity, float eRotateSpeed, float eGrowSpeed) : base("", Vector2.Zero, 0, 0)
        {
            liveTime = eLifeTime;
            velocity = eVelocity;
            rotateSpeed = eRotateSpeed;
            growSpeed = eGrowSpeed;

            shouldRemove = false;
            livedTime = TimeSpan.Zero;
        }

        public override void updateElement(EUpdateArgs updateArgs)
        {
            base.updateElement(updateArgs);

            livedTime += updateArgs.gameTime.ElapsedGameTime;

            if (livedTime.TotalMilliseconds >= liveTime)
            {
                shouldRemove = true;
            }
            else
            {
                Position += velocity;
                Rotation += rotateSpeed;
                Width += growSpeed;
                Height += growSpeed;
            }
        }

    }
}