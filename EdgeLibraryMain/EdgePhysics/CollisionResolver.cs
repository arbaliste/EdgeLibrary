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
using EdgeLibrary;

namespace EdgePhysics
{
    public static class CollisionResolver
    {
        public static void Solve(CollisionInfo info, PhysicsBody a, PhysicsBody b)
        {
            if (a.Shape.GetShapeType() == ShapeType.Circle)
            {
                if (b.Shape.GetShapeType() == ShapeType.Circle)
                {
                    CircleCircle(info, a, b);
                }
                else
                {
                    CirclePolygon(info, a, b);
                }
            }
            else
            {
                if (b.Shape.GetShapeType() == ShapeType.Circle)
                {
                    PolygonCircle(info, a, b);
                }
                else
                {
                    PolygonPolygon(info, a, b);
                }
            }
        }

        public static void CircleCircle(CollisionInfo info, PhysicsBody a, PhysicsBody b)
        {
            CircleShape A = (CircleShape)a.Shape;
            CircleShape B = (CircleShape)b.Shape;

            Vector2 normal = b.Position - a.Position;
            float distSqr = normal.LengthSquared();
            float distance = normal.Length();
            float radius = A.Radius + B.Radius;

            if (distSqr > radius * radius) { info.ContactNumber = 0;  return; }

            if (distance == 0)
            {
                info.Depth = A.Radius;
                info.Normal = new Vector2(1, 0);
                info.Contacts[0] = a.Position;
            }
            else
            {
                info.Depth = radius - distance;
                info.Normal = normal / distance;
                info.Contacts[0] = info.Normal * A.Radius + a.Position;
            }
        }

        public static void CirclePolygon(CollisionInfo info, PhysicsBody a, PhysicsBody b)
        {
            CircleShape A = (CircleShape)a.Shape;
            PolygonShape B = (PolygonShape)b.Shape;

            info.ContactNumber = 0;
            Vector2 center = B.Matrix.Transpose() * (a.Position - b.Position);

            float separation = -float.MaxValue;
            int faceNormal = 0;
            for (int i = 0; i < B.Vertices.Count; i++)
            {
                float s = B.Normals[i].DotProduct(center - B.Vertices[i]);

                if (s > A.Radius) { return; }

                if (s > separation) { separation = s; faceNormal = i; }
            }

            Vector2 v1 = B.Vertices[faceNormal];
            int i2 = faceNormal + 1 < B.Vertices.Count ? faceNormal + 1 : 0;
            Vector2 v2 = B.Vertices[i2];

            if (separation <= 0)
            {
                info.ContactNumber = 1;
                info.Normal = -(B.Matrix * B.Normals[faceNormal]);
                info.Contacts[0] = info.Normal * A.Radius + a.Position;
                info.Depth = A.Radius;
                return;
            }

            float dot1 = (center - v1).DotProduct(v2 - v1);
            float dot2 = (center - v2).DotProduct(v1 - v2);
            info.Depth = A.Radius- separation;

            if (dot1 <= 0.0f)
            {
                if (Vector2.DistanceSquared(center, v1) > A.Radius * A.Radius) { return; }

                info.ContactNumber = 1;
                Vector2 n = v1 - center;
                n = B.Matrix * n;
                n.Normalize();
                info.Normal = n;
                v1 = B.Matrix * v1 + b.Position;
                info.Contacts[0] = v1;
            }
            else if (dot2 <= 0.0f)
            {
                if (Vector2.DistanceSquared(center, v2) > A.Radius * A.Radius) { return; }

                info.ContactNumber = 1;
                Vector2 n = v2 - center;
                v2 = B.Matrix * v2 + b.Position;
                info.Contacts[0] = v2;
                n = B.Matrix * n;
                n.Normalize();
                info.Normal = n;
            }
            else
            {
                Vector2 n = B.Normals[faceNormal];
                if ((center - v1).DotProduct(n) > A.Radius) { return; }

                n = B.Matrix * n;
                info.Normal = -n;
                info.Contacts[0] = info.Normal * A.Radius + a.Position;
                info.ContactNumber = 1;
            }
        }

        public static void PolygonCircle(CollisionInfo info, PhysicsBody a, PhysicsBody b)
        {
            CirclePolygon(info, a, b);
            info.Normal *= -1;
        }

        public static void PolygonPolygon(CollisionInfo info, PhysicsBody a, PhysicsBody b)
        {
            PolygonShape A = (PolygonShape)a.Shape;
            PolygonShape B = (PolygonShape)b.Shape;

          info.ContactNumber = 0;

          int faceA = 0;
          float penetrationA = FindAxisLeastPenetration( ref faceA, A, a.Position, B, b.Position);
          if(penetrationA >= 0) { return; }

          int faceB = 0;
          float penetrationB = FindAxisLeastPenetration( ref faceB, B, b.Position, A, a.Position);
          if(penetrationB >= 0) { return; }

          int referenceIndex;
          bool flip;

          PolygonShape RefPoly;
          PolygonShape IncPoly;

          if(BiasGreaterThan( penetrationA, penetrationB ))
          {
            RefPoly = A;
            IncPoly = B;
            referenceIndex = faceA;
            flip = false;
          }

          else
          {
            RefPoly = B;
            IncPoly = A;
            referenceIndex = faceB;
            flip = true;
          }

          Vector2[] incidentFace = new Vector2[2];
          FindIncidentFace( ref incidentFace, RefPoly, IncPoly, IncPoly == A ? a.Position : b.Position, referenceIndex );

          Vector2 v1 = RefPoly.Vertices[referenceIndex];
          referenceIndex = referenceIndex + 1 == RefPoly.Vertices.Count ? 0 : referenceIndex + 1;
          Vector2 v2 = RefPoly.Vertices[referenceIndex];

          v1 = RefPoly.Matrix * v1 + (RefPoly == A ? a.Position : b.Position);
          v2 = RefPoly.Matrix * v2 + (RefPoly == A ? a.Position : b.Position);

          Vector2 sidePlaneNormal = (v2 - v1);
          sidePlaneNormal.Normalize( );

          Vector2 refFaceNormal = new Vector2( sidePlaneNormal.Y, -sidePlaneNormal.X );

          float refC = refFaceNormal.DotProduct(v1);
          float negSide = -sidePlaneNormal.DotProduct(v1);
          float posSide = refFaceNormal.DotProduct(v2);

          if(Clip( -sidePlaneNormal, negSide, ref incidentFace ) < 2) { return; }

          if(Clip(  sidePlaneNormal, posSide, ref incidentFace ) < 2) { return; }

          info.Normal = flip ? -refFaceNormal : refFaceNormal;

          int cp = 0;
          float separation = refFaceNormal.DotProduct(incidentFace[0]) - refC;
          if(separation <= 0.0f)
          {
            info.Contacts[cp] = incidentFace[0];
            info.Depth = -separation;
            ++cp;
          }
          else
          {
            info.Depth = 0;
          }

          separation = refFaceNormal.DotProduct(incidentFace[1]) -refC;
          if(separation <= 0.0f)
          {
            info.Contacts[cp] = incidentFace[1];

            info.Depth += -separation;
            ++cp;

            info.Depth /= (float)cp;
          }

          info.ContactNumber = cp;
        }

        public static bool BiasGreaterThan(float a, float b)
        {
            float biasRelative = 0.95f;
            float biasAbsolute = 0.01f;
            return a >= b * biasRelative + a * biasAbsolute;
        }

        public static float FindAxisLeastPenetration(ref int faceIndex, PolygonShape a, Vector2 aPosition, PolygonShape b, Vector2 bPosition)
        {
            float bestDistance = -float.MaxValue;
            int bestIndex = 0;

            for (int i = 0; i < a.Vertices.Count; ++i)
            {
                Vector2 n = a.Normals[i];
                Vector2 nw = a.Matrix * n;

                PhysicsMatrix buT = b.Matrix.Transpose();
                n = buT * nw;

                Vector2 s = b.GetSupport(-n);

                Vector2 v = a.Vertices[i];
                v = a.Matrix * v + aPosition;
                v -= bPosition;
                v = buT * v;

                float d = n.DotProduct(s - v); ;

                if (d > bestDistance)
                {
                    bestDistance = d;
                    bestIndex = i;
                }
            }

            faceIndex = bestIndex;
            return bestDistance;
        }

        public static void FindIncidentFace(ref Vector2[] vector, PolygonShape refPoly, PolygonShape incPoly, Vector2 incPolyPosition, int referenceIndex)
        {
            Vector2 referenceNormal = refPoly.Normals[referenceIndex];

            referenceNormal = refPoly.Matrix * referenceNormal;
            referenceNormal = incPoly.Matrix.Transpose() * referenceNormal;

            int incidentFace = 0;
            float minDot = float.MaxValue;
            for (int i = 0; i < incPoly.Vertices.Count; ++i)
            {
                float dot = referenceNormal.DotProduct(incPoly.Normals[i]);
                if (dot < minDot)
                {
                    minDot = dot;
                    incidentFace = i;
                }
            }

            vector[0] = incPoly.Matrix * incPoly.Vertices[incidentFace] + incPolyPosition;
            incidentFace = incidentFace + 1 >= incPoly.Vertices.Count ? 0 : incidentFace + 1;
            vector[1] = incPoly.Matrix * incPoly.Vertices[incidentFace] + incPolyPosition;
        }

        public static int Clip(Vector2 n, float c, ref Vector2[] face)
        {
            int sp = 0;
            Vector2[] outVector = face;

          float d1 = n.DotProduct(face[0]) - c;
          float d2 = n.DotProduct(face[1]) - c;

          if(d1 <= 0.0f) outVector[sp++] = face[0];
          if(d2 <= 0.0f) outVector[sp++] = face[1];
  
          if(d1 * d2 < 0.0f) 
          {
            float alpha = d1 / (d1 - d2);
            outVector[sp] = face[0] + alpha * (face[1] - face[0]);
            ++sp;
          }

          face[0] = outVector[0];
          face[1] = outVector[1];

          return sp;
        }
    }
}