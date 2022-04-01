using System;
using System.Collections.Generic;
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
        private Block[,] border;
        private Block[,] blocks;
        private List<Entity> entities;

        public static event Action<Vector2> collisionEvent;


        public Room()
        {
            this.roomBorder = new Sprite("room_border", 0, Settings.SCREEN_HEIGHT - 168);
            this.border = new Block[14, 9];
            this.blocks = new Block[12, 7];
            this.entities = new List<Entity>();

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
                        this.border[x, y] = new BlockHole(x - 1, y - 1);
                }
            }

            this.blocks[4, 4] = new BlockWall(4, 4);

        }

        public void Spawn(Entity entity, int roomX, int roomY)
        {
            entity.SetPosition(Room.ROOM_OFFSET_X + (roomX * Block.WIDTH),
                               Room.ROOM_OFFSET_Y + (roomY * Block.HEIGHT));
            entity.UpdateSprite();
            this.entities.Add(entity);
        }

        public void Update(GameTime gameTime, Input input)
        {
            foreach (EntityPlayer entity in this.entities)
            {
                entity.Update(gameTime, input);
            }

            foreach (EntityPlayer entity in this.entities)
            {
                if (!entity.HasMoved)
                    continue;

                //foreach (Entity other in this.entities)
                //{
                //    if (entity == other)
                //        continue;

                //    CollisionInfo info = entity.CollisionWith(other);
                //    if (info.isCollision)
                //    {
                //        if (entity.OnCollision(other))
                //            entity.CancelMove(info.offset);
                //    }
                //}
                foreach (Block block in this.blocks)
                {
                    if (block.IsWalkable())
                        continue;

                    CollisionInfo info = entity.CollisionWith(block);
                    if (info.isCollision)
                    {
                        Room.collisionEvent += entity.CancelMove;
                        collisionEvent?.Invoke(info.offset);
                        Room.collisionEvent -= entity.CancelMove;
                        //entity.CancelMove(info.offset);
                    }
                }

                foreach (Block block in this.border)
                {
                    if (block == null)
                        continue;

                    CollisionInfo info = entity.CollisionWith(block);
                    if (info.isCollision)
                    {
                        Room.collisionEvent += entity.CancelMove;
                        collisionEvent?.Invoke(info.offset);
                        Room.collisionEvent -= entity.CancelMove;
                        //entity.CancelMove(info.offset);
                    }
                }

                entity.UpdateSprite();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.roomBorder.Draw(spriteBatch);

            foreach (Block block in this.blocks)
            {
                block.Draw(spriteBatch);
            }
            foreach (EntityPlayer entity in this.entities)
            {
                entity.Draw(spriteBatch);
            }
        }
    }
}
