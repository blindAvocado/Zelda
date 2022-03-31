using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Zelda
{
    public struct CollisionInfo
    {
        public bool isCollision;
        public Vector2 offset;


        public CollisionInfo(Vector2 intersectionDepth)
        {
            this.isCollision = !intersectionDepth.Equals(Vector2.Zero);
            this.offset = intersectionDepth;
        }
    }
}
