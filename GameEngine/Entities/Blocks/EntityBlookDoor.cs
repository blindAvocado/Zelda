using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zelda
{

    public class EntityBlookDoor : Entity
    {
        private Direction direction;
        private DoorType type;
        private Room targetRoom;
        private Dungeon dungeon;

        public void SetDoorType(DoorType type)
        {
            this.type = type;
        }

        public EntityBlookDoor(Dungeon dungeon, Direction direction, DoorType type, Room room, Sprite sprite, Rectangle hitbox) : base(sprite, 0, 0, hitbox)
        {
            this.sprite.SetLayerDepth(0.25f);
            this.dungeon = dungeon;
            this.direction = direction;
            this.type = type;
            this.targetRoom = room;
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
