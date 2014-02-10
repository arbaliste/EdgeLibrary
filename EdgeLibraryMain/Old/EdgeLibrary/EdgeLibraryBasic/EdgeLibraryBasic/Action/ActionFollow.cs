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
    //An action to make a sprite follow an element
    public class EActionFollow : EAction
    {
        public Element spriteToFollow { get; set; }
        public float speed { get; set; }
        protected EActionMove moveAction;

        public EActionFollow(Element SpriteToFollow, float eSpeed) : base()
        {
            RequiresUpdate = true;
            spriteToFollow = SpriteToFollow;
            speed = eSpeed;
            moveAction = new EActionMove(spriteToFollow.Position, speed);
        }

        public override bool Update(Sprite targetSprite)
        {
            if (spriteToFollow == null) 
            {
                return true;
            }

            moveAction.cancel();
            moveAction = new EActionMove(spriteToFollow.Position, speed);
            targetSprite.runAction(moveAction);

            return false;
        }
    }
}