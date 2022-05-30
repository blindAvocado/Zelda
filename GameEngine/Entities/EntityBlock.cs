using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda
{
    [Serializable]
    public abstract class EntityBlock : Entity
    {
        public static int WIDTH = 16;
        public static int HEIGHT = 16;

        protected int spriteX;
        protected int spriteY;
        protected bool isWalkable;

        public bool IsWalkable()
        {
            return this.isWalkable;
        }

        protected EntityBlock(int roomX, int roomY, int frameX, int frameY, bool isWalkable = true)
                              : base(new Sprite("tiles",
                                     Room.ROOM_OFFSET_X + (roomX * EntityBlock.WIDTH),
                                     Room.ROOM_OFFSET_Y + (roomY * EntityBlock.HEIGHT),
                                     5, 2, frameX, frameY),
                                     Room.ROOM_OFFSET_X + (roomX * EntityBlock.WIDTH),
                                     Room.ROOM_OFFSET_Y + (roomY * EntityBlock.HEIGHT))
        {
            this.roomX = Room.ROOM_OFFSET_X + (roomX * EntityBlock.WIDTH);
            this.roomY = Room.ROOM_OFFSET_Y + (roomY * EntityBlock.HEIGHT);
            this.sprite.SetLayerDepth(0.1f);
            this.isWalkable = isWalkable;

            this.spriteName = "tiles";
            this.spriteCol = 5;
            this.spriteRow = 2;

        }

        public override void InitializeLoad()
        {
            this.sprite = new Sprite(this.spriteName, Room.ROOM_OFFSET_X + (roomX * EntityBlock.WIDTH), Room.ROOM_OFFSET_Y + (roomY * EntityBlock.HEIGHT), this.spriteCol, this.spriteRow, this.spriteX, this.spriteY);
            this.sprite.SetLayerDepth(0.1f);
            this.position = new Rectangle(this.positionInt[0], this.positionInt[1], this.positionInt[2], this.positionInt[3]);
            this.baseHitbox = new Rectangle(this.baseHitboxInt[0], this.baseHitboxInt[1], this.baseHitboxInt[2], this.baseHitboxInt[3]);
            this.hitbox = new Rectangle(this.hitboxInt[0], this.hitboxInt[1], this.hitboxInt[2], this.hitboxInt[3]);

            base.InitializeLoad();
        }

        public override void UpdateChildren(GameTime gameTime, Input input)
        {

        }
    }
}
