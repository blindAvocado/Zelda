using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Zelda
{
    public class Projectile
    {
        private Entity owner;
        private Sprite sprite;
        private int damage;
        private int speed;
        private int attackSpeed;
        private Rectangle projectileShape;


        public void SetOwner(Entity owner)
        {
            this.owner = owner;
        }

        public Projectile(int index, int damage, int attackSpeed, int speed, int projectileWidth, int projectileHeight, int offsetX, int offsetY)
        {
            this.owner = null;
            this.sprite = new Sprite("projectiles", 0, 0, 7, 2, index, 0);
            this.damage = damage;
            this.speed = speed;
            this.attackSpeed = attackSpeed;
            this.projectileShape = new Rectangle(offsetX, offsetY, projectileWidth, projectileHeight);
        }

        public EntityProjectile CreateEntity(Direction direction)
        {
            Rectangle baseHitbox = new Rectangle(0, 0, this.sprite.Width, this.sprite.Height);

            switch(direction)
            {
                case Direction.UP:
                case Direction.DOWN:
                    baseHitbox = this.projectileShape;
                    break;
                case Direction.LEFT:
                case Direction.RIGHT:
                    baseHitbox.X = this.projectileShape.Y;
                    baseHitbox.Y = this.projectileShape.X;
                    baseHitbox.Width = this.projectileShape.Width;
                    baseHitbox.Height = this.projectileShape.Height;
                    break;
            }

            return new EntityProjectile(this.owner, this.sprite.Clone(), direction, this.damage, this.attackSpeed, this.speed, baseHitbox);
        }
    }
}
