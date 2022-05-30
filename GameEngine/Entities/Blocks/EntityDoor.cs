using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zelda
{
    [Serializable]
    public class EntityDoor : Entity
    {
        
        private Direction direction;
        private DoorType type;
        private Room targetRoom;
        private Dungeon dungeon;

        public void SetDoorType(DoorType type)
        {
            this.type = type;
        }

        public EntityDoor(Dungeon dungeon, Direction direction, DoorType type, Room room, Sprite sprite, Rectangle hitbox) : base(sprite, 0, 0, hitbox)
        {
            this.sprite.SetLayerDepth(0.25f);
            this.direction = direction;
            this.type = type;
            this.targetRoom = room;
            this.dungeon = dungeon;

            this.spriteName = "doors";
            this.spriteCol = 2;
            this.spriteRow = 3;
        }

        public override void InitializeLoad()
        {
            this.sprite = new Sprite(this.spriteName, 0, 0, this.spriteCol, this.spriteRow);
            this.baseHitbox = new Rectangle(this.baseHitboxInt[0], this.baseHitboxInt[1], this.baseHitboxInt[2], this.baseHitboxInt[3]);
            this.position = new Rectangle(this.positionInt[0], this.positionInt[1], this.positionInt[2], this.positionInt[3]);
            this.hitbox = new Rectangle(this.hitboxInt[0], this.hitboxInt[1], this.hitboxInt[2], this.hitboxInt[3]);

            switch (this.direction)
            {
                case Direction.UP:
                    hitbox.Width = 32;
                    hitbox.Height = 20;
                    break;
                case Direction.LEFT:
                    sprite.CurrentFrameX = 1;
                    sprite.SetSpriteEffect(SpriteEffects.FlipHorizontally);
                    hitbox.X = 10;
                    hitbox.Width = 20;
                    hitbox.Height = 32;
                    break;
                case Direction.RIGHT:
                    hitbox.Width = 20;
                    hitbox.Height = 32;
                    sprite.CurrentFrameX = 1;
                    break;
                case Direction.DOWN:
                    hitbox.Y = 12;
                    hitbox.Width = 32;
                    hitbox.Height = 20;
                    sprite.SetSpriteEffect(SpriteEffects.FlipVertically);
                    break;
                default:
                    break;
            }

            switch (this.type)
            {
                case DoorType.OPEN:
                    sprite.CurrentFrameY = 0;
                    break;
                case DoorType.CLOSED:
                    sprite.CurrentFrameY = 1;
                    break;
                case DoorType.LOCKED:
                    sprite.CurrentFrameY = 2;
                    break;
                default:
                    break;
            }

            this.UpdateSprite();
            base.InitializeLoad();
        }

        public void Transport(EntityPlayer player)
        {
            this.dungeon.Transport(this, player);
        }

        public Direction Direction { get { return this.direction; } }
        public Room TargetRoom { get { return this.targetRoom; } }
        public DoorType DoorType { get { return this.type; } }

        public override void UpdateChildren(GameTime gameTime, Input input)
        {
        }
    }
}
