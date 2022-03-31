using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda
{
    public abstract class Block : Entity
    {
        public static int WIDTH = 16;
        public static int HEIGHT = 16;


        protected Point roomPosition;
        protected bool isWalkable;

        public bool IsWalkable()
        {
            return this.isWalkable;
        }

        protected Block(int roomX, int roomY, int frameX, int frameY, bool isWalkable = true)
                              : base(new Sprite("tiles",
                                     Room.ROOM_OFFSET_X + (roomX * Block.WIDTH),
                                     Room.ROOM_OFFSET_Y + (roomY * Block.HEIGHT),
                                     5, 2, frameX, frameY),
                                     Room.ROOM_OFFSET_X + (roomX * Block.WIDTH),
                                     Room.ROOM_OFFSET_Y + (roomY * Block.HEIGHT))
        {
            this.roomPosition = new Point(roomX, roomY);
            this.isWalkable = isWalkable;
        }

        public int X
        {
            get { return this.roomPosition.X; }
        }
        public int Y
        {
            get { return this.roomPosition.Y; }
        }

        public override void UpdateChildren(GameTime gameTime, Input input)
        {

        }
    }
}
