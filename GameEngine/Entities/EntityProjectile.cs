using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda
{
    public class EntityProjectile : Entity
    {
        private Entity owner;
        private int damage;
        private Point speed;

        private int attackSpeed;
        private int lifeTime;

        public EntityProjectile(Entity owner, Sprite sprite, Direction direction, int damage, int attackSpeed, int speed, Rectangle baseHitbox, int x = 0, int y = 0) : base(sprite, x, y, baseHitbox)
        {
            this.owner = owner;
            this.speed = new Point(0, 0);
            this.sprite.SetLayerDepth(0.2f);

            switch (direction)
            {
                case Direction.UP:
                    this.speed.Y = -speed;
                    break;
                case Direction.LEFT:
                    this.speed.X = -speed;
                    this.sprite.CurrentFrameY = 1;
                    break;
                case Direction.RIGHT:
                    this.speed.X = speed;
                    this.sprite.CurrentFrameY = 1;
                    this.sprite.SetSpriteEffect(SpriteEffects.FlipHorizontally);
                    break;
                case Direction.DOWN:
                    this.speed.Y = speed;
                    this.sprite.SetSpriteEffect(SpriteEffects.FlipVertically);
                    break;

            }


            this.damage = damage;
            this.attackSpeed = attackSpeed;
            this.lifeTime = 0;
        }

        public override bool OnCollision(Entity other)
        {
            if (other == this.owner)
                return false;

            if (other is LivingEntity)
            {
                LivingEntity entity = (LivingEntity)other;
                entity.Damage(this.damage);

                if (!this.speed.Equals(Point.Zero))
                    this.Destroy();
            }
            //if (other is BlockWall)
            //{
            //    if (!this.speed.Equals(Point.Zero))
            //        this.Destroy();
            //}

            return false;
        }

        public override void UpdateChildren(GameTime gameTime, Input input)
        {
            if (!this.speed.Equals(Point.Zero))
                this.Move(this.speed);
            else
            {
                this.lifeTime += gameTime.ElapsedGameTime.Milliseconds;

                if (this.lifeTime >= this.attackSpeed / 2)
                    this.Destroy();

                if (this.Hitbox.X == Settings.SCREEN_WIDTH || this.Hitbox.X == 0 || this.Hitbox.Y == Settings.SCREEN_HEIGHT || this.Hitbox.Y == 0)
                {
                    this.Destroy();
                }
            }
        }
    }
}
