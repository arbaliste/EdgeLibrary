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

namespace EdgeLibrary.Basic
{
    public class EActionTeleport : EAction
    {
        public Vector2 targetPos { get; set; }

        public EActionTeleport(Vector2 eTargetPos)
        {
            targetPos = eTargetPos;
            initVars();
        }

        public EActionTeleport(EActionTeleport action)
        {
            targetPos = action.targetPos;
            initVars();
        }

        protected void initVars()
        {
            RequiresUpdate = false;
        }

        public override void PerformAction(ESprite sprite)
        {
            sprite.Position = targetPos;
        }
    }
}