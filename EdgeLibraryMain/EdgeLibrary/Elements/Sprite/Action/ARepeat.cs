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
    //Repeats an action for a specific number of times - or forever
    public class ARepeat : Action
    {
        public Action Action;
        public int LoopTimes;
        public bool RepeatForever;
        private int loopedTimes;

        public ARepeat(Action action) : base()
        {
            Action = action;
            LoopTimes = 0;
            RepeatForever = true;
            loopedTimes = 0;
        }

        public ARepeat(int loopTimes, Action action) : base()
        {
            Action = action;
            LoopTimes = loopTimes;
            RepeatForever = false;
            loopedTimes = 0;
        }
        
        public ARepeat(string ID, Action action) : base(ID)
        {
            Action = action;
            LoopTimes = 0;
            RepeatForever = true;
            loopedTimes = 0;
        }

        public ARepeat(string ID, int loopTimes, Action action) : base(ID)
        {
            Action = action;
            LoopTimes = loopTimes;
            RepeatForever = false;
            loopedTimes = 0;
        }

        //Updates the specific action
        protected override void UpdateAction(GameTime gameTime, Sprite sprite)
        {
            loopedTimes++;

            Action.Update(gameTime, sprite);

            if (loopedTimes >= LoopTimes && !RepeatForever)
            {
                Stop(gameTime, sprite);
            }
        }

        public override Action Clone()
        {
            return RepeatForever ? new ARepeat(ID, Action) : new ARepeat(ID, LoopTimes, Action);
        }
    }
}