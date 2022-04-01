using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Zelda
{
    public class EntityPlayer : LivingEntity
    {
        private int keys;
        private int bombs;
        private int coins;



        public EntityPlayer(int x = 0, int y = 0) : base(new Sprite("link", x, y, 4, 2), 1, 1, x, y)
        {
            this.keys = 0;
            this.bombs = 0;
            this.coins = 0;

            Room.collisionEvent += CancelMove;
        }

        public int KeysCount { get { return this.keys; } }
        public int BombsCount { get { return this.bombs; } }
        public int CoinsCount { get { return this.coins; } }

        public override void UpdateChildren(GameTime gameTime, Input input)
        {
            if (input.IsKeyDown(Keys.W)) // UP
            {
                this.Move(0, -this.moveSpeed);
            }
            if (input.IsKeyDown(Keys.A)) // LEFT
            {
                this.Move(-this.moveSpeed, 0);
            }
            if (input.IsKeyDown(Keys.S)) // DOWN
            {
                this.Move(0, +this.moveSpeed);
            }
            if (input.IsKeyDown(Keys.D)) // RIGHT
            {
                this.Move(+this.moveSpeed, 0);
            }

            this.UpdateSpriteAnimation();
            base.UpdateChildren(gameTime, input);
        }
    }
}
