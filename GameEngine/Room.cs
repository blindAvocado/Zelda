using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda
{
    public class Room
    {
        public static int ROOM_OFFSET_X = 32;
        public static int ROOM_OFFSET_Y = (Settings.SCREEN_HEIGHT - 168) + 32;

        private Sprite roomBorder;
        private EntityBlock[,] border;
        private EntityBlock[,] blocks;
        private List<Entity> entities;

        public static event Action<Vector2> collisionEvent;


        public Room()
        {
            this.roomBorder = new Sprite("room_border", 0, Settings.SCREEN_HEIGHT - 168);
            this.border = new EntityBlock[14, 9];
            this.blocks = new EntityBlock[12, 7];
            this.entities = new List<Entity>();
        }

        public void Initialize()
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
                                room.Spawn(new EntityEnemy(new Sprite("enemy_octorok", 0, 0, 4, 2), 2, 1, Weapon.None, 1), x, y);
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
            entity.SetRoom(this);
            entity.UpdateSprite();
            this.entities.Add(entity);
        }

        public void SpawnProjectile(Entity entity, int x, int y)
        {
            entity.SetPosition(x, y);
            entity.SetRoom(this);
            entity.UpdateSprite();
            this.entities.Add(entity);
        }

        public void SpawnItem(Item item, int roomX, int roomY)
        {
            ItemEntity entity = new ItemEntity(item,
                               Room.ROOM_OFFSET_X + (roomX * EntityBlock.WIDTH),
                               Room.ROOM_OFFSET_Y + (roomY * EntityBlock.HEIGHT));
            entity.SetRoom(this);
            entity.UpdateSprite();
            this.entities.Add(entity);
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

                foreach (Entity other in new List<Entity>(this.entities))
                {
                    if (entity == other)
                        continue;

                    // OTHER ENTITIES
                    CollisionInfo info = entity.CollisionWith(other);
                    if (info.isCollision)
                    {
                        if (entity.OnCollision(other))
                            entity.CancelMove(info.offset);
                            //collisionEvent?.Invoke(info.offset);
                        if (entity.IsDestroyed)
                            this.entities.Remove(entity);
                    }
                }

                foreach (EntityBlock block in this.blocks)
                {
                    if (block.IsWalkable())
                        continue;

                    // ROOM BLOCKS
                    CollisionInfo info = entity.CollisionWith(block);
                    if (info.isCollision)
                    {
                        if (entity.OnCollision(block))
                            entity.CancelMove(info.offset);
                            //collisionEvent?.Invoke(info.offset);
                        if (entity.IsDestroyed)
                            this.entities.Remove(entity);
                    }
                }

                foreach (EntityBlock block in this.border)
                {
                    if (block == null)
                        continue;

                    // ROOM BORDERS
                    CollisionInfo info = entity.CollisionWith(block);
                    if (info.isCollision)
                    {
                        if (entity.OnCollision(block))
                            entity.CancelMove(info.offset);
                            //collisionEvent?.Invoke(info.offset);
                        if (entity.IsDestroyed)
                            this.entities.Remove(entity);
                    }
                }

                entity.UpdateSprite();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.roomBorder.Draw(spriteBatch);

            foreach (EntityBlock block in this.blocks)
            {
                block.Draw(spriteBatch);
            }
            foreach (Entity entity in this.entities)
            {
                entity.Draw(spriteBatch);
            }
        }
    }
}
