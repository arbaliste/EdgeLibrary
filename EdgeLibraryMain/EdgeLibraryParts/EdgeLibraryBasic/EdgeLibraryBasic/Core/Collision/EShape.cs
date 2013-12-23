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

namespace EdgeLibrary.Basic
{
    public enum EShapeTypes
    {
        circle,
        rectangle
    };

    public class EShape
    {
        public EShapeTypes ShapeType { get; private set; }
        public Vector2 CenterPosition { get; set; }

        public EShape(Vector2 position, EShapeTypes shapeType)
        {
            CenterPosition = position;
            ShapeType = shapeType;
        }

        //Check for collision here
        public virtual bool CollidesWith(EShape shape)
        {
            return false;
        }
    }

    public class EShapeCircle : EShape
    {
        public float Radius {get; set;}

        public EShapeCircle(Vector2 position, float radius) : base(position, EShapeTypes.circle)
        {
            Radius = radius;
        }

        public override bool CollidesWith(EShape shape)
        {
            switch (shape.ShapeType)
            {
                case EShapeTypes.circle:
                    return (EMath.DistanceBetween(CenterPosition, shape.CenterPosition) <= (Radius + ((EShapeCircle)shape).Radius));
                    break;
                case EShapeTypes.rectangle:

                    break;
            }
            return false;
        }
    }

    public class EShapeRectangle : EShape
    {
        public float Width { get; set; }
        public float Height { get; set; }

        public EShapeRectangle(Vector2 position, float width, float height) : base(position, EShapeTypes.rectangle)
        {
            Width = width;
            Height = height;
        }

        public override bool CollidesWith(EShape shape)
        {
            switch (shape.ShapeType)
            {
                case EShapeTypes.circle:
                    break;
                case EShapeTypes.rectangle:
                    return new Rectangle((int)CenterPosition.X + (int)Width / 2, (int)CenterPosition.Y + (int)Height / 2, (int)Width, (int)Height).Intersects(new Rectangle((int)shape.CenterPosition.X + (int)((EShapeRectangle)shape).Width / 2, (int)shape.CenterPosition.Y + (int)((EShapeRectangle)shape).Height / 2, (int)((EShapeRectangle)shape).Width, (int)((EShapeRectangle)shape).Height));
                    break;
            }
            return false;
        }
    }
}