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
    //Basically a timer - sends off an event whenever a certain time has passed
    public class Ticker : GameComponent, ICloneable
    {
        public double MillisecondsWait { get; set; }
        public double elapsedMilliseconds;
        public bool Started;

        public delegate void TickerEventHandler(GameTime gameTime);
        public event TickerEventHandler OnTick;

        public Ticker(double milliseconds)
            : base(EdgeGame.Game)
        {
            MillisecondsWait = milliseconds;
            elapsedMilliseconds = 0;
            Started = true;
        }

        public override void Update(GameTime gameTime)
        {
            if (Started)
            {
                elapsedMilliseconds += gameTime.ElapsedGameTime.TotalMilliseconds * EdgeGame.GameSpeed;

                if (elapsedMilliseconds >= MillisecondsWait)
                {
                    elapsedMilliseconds = 0;
                    if (OnTick != null)
                    {
                        OnTick(gameTime);
                    }
                }
            }
        }

        public object Clone()
        {
            Ticker clone = (Ticker)MemberwiseClone();
            if (OnTick != null)
            {
                clone.OnTick = (TickerEventHandler)OnTick.Clone();
            }
            return clone;
        }
    }


}
