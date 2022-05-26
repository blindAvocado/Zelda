using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zelda
{
    public enum DoorType
    {
        OPEN,
        CLOSED,
        LOCKED
    }

    public class Door
    {
        private Direction direction;
        private DoorType type;
        private Sprite sprite;
        private Room room;
        private Dungeon dungeon;
        private Rectangle hitbox;

        public Door(Dungeon dungeon, Direction direction, DoorType type, Room room)
        {
            this.dungeon = dungeon;
            this.direction = direction;
            this.type = type;
            this.room = room;
            this.sprite = this.CreateSprite();
            this.hitbox = this.CreateHitbox();
        }

        public Rectangle Hitbox { get { return hitbox; } }

        private Sprite CreateSprite()
        {
            Sprite sprite = new Sprite("doors", 0, 0, 2, 3);

            switch (this.direction)
            {
                case Direction.UP:
                    break;
                case Direction.LEFT:
                    sprite.CurrentFrameX = 1;
                    sprite.SetSpriteEffect(SpriteEffects.FlipHorizontally);
                    break;
                case Direction.RIGHT:
                    sprite.CurrentFrameX = 1;
                    break;
                case Direction.DOWN:
                    sprite.SetSpriteEffect(SpriteEffects.FlipVertically);
                    break;
                default:
                    break;
            }

            switch(this.type)
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

            return sprite;
        }

        private Rectangle CreateHitbox()
        {
            Rectangle hitbox = new Rectangle();

            switch (this.direction)
            {
                case Direction.UP:
                    hitbox.Width = 32;
                    hitbox.Height = 20;
                    break;
                case Direction.DOWN:
                    hitbox.Y = 12;
                    hitbox.Width = 32;
                    hitbox.Height = 20;
                    break;
                case Direction.LEFT:
                    hitbox.X = 10;
                    hitbox.Width = 20;
                    hitbox.Height = 32;
                    break;
                case Direction.RIGHT:
                    hitbox.Width = 20;
                    hitbox.Height = 32;
                    break;
                default:
                    break;
            }

            return hitbox;
        }

        public EntityBlookDoor createDoor()
        {
            return new EntityBlookDoor(this.dungeon, this.direction, this.type, this.room, this.sprite, this.hitbox);
        }
    }
}
