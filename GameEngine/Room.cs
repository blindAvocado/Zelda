using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda
{
    [Serializable]
    public class Room
    {
        public static int ROOM_WIDTH = 256;
        public static int ROOM_HEIGHT = 168;
        public static int ROOM_OFFSET_X = 32;
        public static int ROOM_OFFSET_Y = (Settings.SCREEN_HEIGHT - ROOM_HEIGHT) + 32;


        [NonSerialized] private Sprite roomBorder;
        private char roomId;
        private EntityBlock[,] border;
        private EntityBlock[,] blocks;
        private List<Entity> entities;
        private List<EntityDoor> doors;
        private bool empty;

        public char RoomID { set { this.roomId = value; } }

        public Room(bool empty = false)
        {
            this.roomBorder = new Sprite("room_border", 0, Settings.SCREEN_HEIGHT - ROOM_HEIGHT);
            this.border = new EntityBlock[14, 9];
            this.blocks = new EntityBlock[12, 7];
            this.entities = new List<Entity>();
            this.doors = new List<EntityDoor>();
            this.empty = empty;

            if (empty)
                this.InitializeEmptyRoom();
        }

        public void Initialize()
        {
            this.roomBorder = new Sprite("room_border", 0, Settings.SCREEN_HEIGHT - ROOM_HEIGHT);

            foreach (EntityBlock block in this.border)
            {
                if (block is BlockFloor)
                    Debug.WriteLine("FLOOR");
                block?.InitializeLoad();
            }

            foreach (EntityBlock block in this.blocks)
                block?.InitializeLoad();

            foreach (Entity entity in this.entities)
            {
                if (entity is EnemyOctorok)
                {
                    Debug.WriteLine("OCTOROK");
                }
                entity?.InitializeLoad();
            }

            foreach (EntityDoor door in this.doors)
                door?.InitializeLoad();

        }

        public void Save()
        {
            foreach (EntityBlock block in this.border)
                block?.Save();

            foreach (EntityBlock block in this.blocks)
                block?.Save();

            foreach (Entity entity in this.entities)
                entity?.Save();

            foreach (EntityDoor door in this.doors)
                door?.Save();
        }

        public void InitializeEmptyRoom()
        {

            for (int y = 0; y < this.blocks.GetLength(1); y++)
            {
                for (int x = 0; x < this.blocks.GetLength(0); x++)
                {
                    this.blocks[x, y] = new BlockFloor(x, y);
                }
            }

            for (int y = 0; y < this.border.GetLength(1); y++)
            {
                for (int x = 0; x < this.border.GetLength(0); x++)
                {
                    if (x == 0 || y == 0 || x == this.border.GetLength(0) - 1 || y == this.border.GetLength(1) - 1)
                        this.border[x, y] = new BlockWall(x - 1, y - 1);
                }
            }
        }

        public static Room LoadRoom(string name)
        {
            Room room = new Room();

            using (BinaryReader reader = new BinaryReader(new FileStream(("rooms/" + name + ".room"), FileMode.Open, FileAccess.Read)))
            {
                for (int y = 0; y < room.blocks.GetLength(1); y++)
                {
                    for (int x = 0; x < room.blocks.GetLength(0); x++)
                    {
                        char c = reader.ReadChar();
                        switch(c)
                        {
                            case '0':
                                room.blocks[x, y] = new BlockFloor(x, y);
                                break;
                            case '1':
                                room.blocks[x, y] = new BlockWall(x, y);
                                break;
                            case '2':
                                room.blocks[x, y] = new BlockHole(x, y);
                                break;
                            case 'x':
                                room.blocks[x, y] = new BlockFloor(x, y);
                                room.Spawn(new EnemyOctorok(), x, y);
                                break;
                            case 'y':
                                room.blocks[x, y] = new BlockFloor(x, y);
                                room.Spawn(new EnemyMoblin(), x, y);
                                break;
                        }
                    }
                    reader.ReadChar();
                }

                for (int y = 0; y < room.border.GetLength(1); y++)
                {
                    for (int x = 0; x < room.border.GetLength(0); x++)
                    {
                        if (x == 0 || y == 0 || x == room.border.GetLength(0) - 1 || y == room.border.GetLength(1) - 1)
                            room.border[x, y] = new BlockWall(x - 1, y - 1);
                    }
                }

            }

            return room;
        }

        public void Spawn(Entity entity, int roomX, int roomY)
        {
            entity.SetPosition(Room.ROOM_OFFSET_X + (roomX * EntityBlock.WIDTH),
                               Room.ROOM_OFFSET_Y + (roomY * EntityBlock.HEIGHT));
            entity.UpdateSprite();
            this.entities.Add(entity);
        }

        public void SpawnProjectile(Entity entity, int x, int y)
        {
            entity.SetPosition(x, y);
            entity.UpdateSprite();
            this.entities.Add(entity);
        }

        public void SpawnItem(Item item, int roomX, int roomY)
        {
            ItemEntity entity = new ItemEntity(item,
                               Room.ROOM_OFFSET_X + (roomX * EntityBlock.WIDTH),
                               Room.ROOM_OFFSET_Y + (roomY * EntityBlock.HEIGHT));
            entity.UpdateSprite();
            this.entities.Add(entity);
        }

        public void Remove(Entity entity)
        {
            this.entities.Remove(entity);
        }

        public void AddDoor(Dungeon dungeon, Direction direction, DoorType type, Room room)
        {
            Door doorTemp = new Door(dungeon, direction, type, room);
            EntityDoor door = doorTemp.createDoor();

            int x = 0;
            int y = 0;

            switch (direction)
            {
                case Direction.UP:
                    x = Room.ROOM_OFFSET_X + 80;
                    y = Room.ROOM_OFFSET_Y - 20;
                    this.border[6, 0] = new BlockFloor(6, 0);
                    this.border[7, 0] = new BlockFloor(7, 0);
                    break;
                case Direction.LEFT:
                    x = Room.ROOM_OFFSET_X - 32;
                    y = Room.ROOM_OFFSET_Y + 40;
                    this.border[0, 4] = new BlockFloor(0, 4);
                    break;
                case Direction.RIGHT:
                    x = Settings.SCREEN_WIDTH - Room.ROOM_OFFSET_X;
                    y = Room.ROOM_OFFSET_Y + 40;
                    this.border[13, 4] = new BlockFloor(13, 4);
                    break;
                case Direction.DOWN:
                    x = Room.ROOM_OFFSET_X + 80;
                    y = Settings.SCREEN_HEIGHT - 32;
                    this.border[6, 8] = new BlockFloor(6, 8);
                    this.border[7, 8] = new BlockFloor(7, 8);
                    break;
                default:
                    break;
            }

            door.SetPosition(x, y);
            this.doors.Add(door);
        }

        public void Update(GameTime gameTime, Input input)
        {
            foreach (Entity entity in new List<Entity>(this.entities))
            {
                entity.Update(gameTime, input);
                if (entity.IsDestroyed)
                {
                    this.entities.Remove(entity);
                }
            }

            foreach (Entity entity in new List<Entity>(this.entities))
            {
                if (!entity.HasMoved && !entity.IsCreated)
                    continue;

                if (entity.IsCreated)
                    entity.IsCreated = false;

                // OTHER ENTITIES
                foreach (Entity other in new List<Entity>(this.entities))
                {
                    if (entity == other)
                        continue;

                    CollisionInfo info = entity.CollisionWith(other);
                    if (info.isCollision)
                    {
                        if (entity.OnCollision(other))
                            entity.CancelMove(info.offset);
                        if (entity.IsDestroyed)
                            this.entities.Remove(entity);
                    }
                }


                // ROOM BLOCKS
                foreach (EntityBlock block in this.blocks)
                {
                    if (block.IsWalkable())
                        continue;


                    CollisionInfo info = entity.CollisionWith(block);
                    if (info.isCollision)
                    {
                        if (entity.OnCollision(block))
                            entity.CancelMove(info.offset);
                        if (entity.IsDestroyed)
                            this.entities.Remove(entity);
                    }
                }

                // ROOM BORDERS
                foreach (EntityBlock block in this.border)
                {
                    if (block == null)
                        continue;

                    //if (block is BlockHole)
                    //    Debug.WriteLine(block.Hitbox);

                    CollisionInfo info = entity.CollisionWith(block);
                    if (info.isCollision)
                    {
                        if (entity.OnCollision(block))
                            entity.CancelMove(info.offset);
                        if (entity.IsDestroyed)
                            this.entities.Remove(entity);
                    }
                }

                foreach (EntityDoor door in this.doors)
                {
                    CollisionInfo info = entity.CollisionWith(door);
                    if (info.isCollision)
                    {
                        if (entity.OnCollision(door))
                            entity.CancelMove(info.offset);
                        if (entity.IsDestroyed)
                            this.entities.Remove(entity);
                    }
                }


                entity.UpdateSprite();
            }
            foreach (EntityDoor door in this.doors)
            {
                door.Update(gameTime, input);
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.roomBorder.Draw(spriteBatch);

            foreach (EntityBlock block in this.blocks)
                block.Draw(spriteBatch);
            foreach (EntityDoor door in this.doors)
                door.Draw(spriteBatch);
            foreach (Entity entity in this.entities)
                entity.Draw(spriteBatch);
        }
    }
}
