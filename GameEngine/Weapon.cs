using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda
{
    [Serializable]
    public class Weapon
    {
        public static Weapon None = null;
        public static Weapon ForestSword = new Weapon(26, 1, 250, 0, 0, 7, 16, 5, 0);
        public static Weapon ForestBow = new Weapon(21, 1, 500, 4, 5, 3, 16, 6, 0);

        protected int iconIndex;
        [NonSerialized] protected Sprite sprite;
        protected int damage;
        protected int attackSpeed;
        private int projectileIndex;
        private int projectileSpeed;
        private int projectileWidth;
        private int projectileHeight;
        private int projectileOffsetX;
        private int projectileOffsetY;
        private int offsetX;
        private int offsetY;
        [NonSerialized] protected Projectile projectile;
        [NonSerialized] protected Point projectileOffset;

        [NonSerialized] protected bool isWeaponReady;
        [NonSerialized] protected int weaponCooldown;

        public void SetOwner(Entity owner)
        {
            this.projectile.SetOwner(owner);
        }

        public Weapon(int iconIndex, int damage, int attackSpeed, int projectileIndex, int projectileSpeed,
            int projectileWidth = 16, int projectileHeight = 16, int offsetX = 0, int offsetY = 0, int projectileOffsetX = -4, int projectileOffsetY = 2)
        {
            this.iconIndex = iconIndex;
            this.sprite = new Sprite("items", 124, 26, 8, 5, iconIndex % 8, iconIndex / 8);
            this.damage = damage;
            this.attackSpeed = attackSpeed;
            this.projectileIndex = projectileIndex;
            this.projectileSpeed = projectileSpeed;
            this.projectileWidth = projectileWidth;
            this.projectileHeight = projectileHeight;
            this.projectileOffsetX = projectileOffsetX;
            this.projectileOffsetY = projectileOffsetY;
            this.offsetX = offsetX;
            this.offsetY = offsetY;
            this.projectile = new Projectile(projectileIndex, damage, this.attackSpeed, projectileSpeed, projectileWidth, projectileHeight, offsetX, offsetY);
            this.projectileOffset = new Point(projectileOffsetX, projectileOffsetY);

            this.isWeaponReady = true;
            this.weaponCooldown = 0;
        }

        public int Index { get { return this.iconIndex; } }
        public int AttackSpeed { get { return this.attackSpeed; } }

        public bool CanUseWeapon()
        {
            return this.isWeaponReady;
        }

        public void Initialize()
        {
            this.sprite = new Sprite("items", 124, 26, 8, 5, iconIndex % 8, iconIndex / 8);
            this.projectile = new Projectile(this.projectileIndex, this.damage, this.attackSpeed, this.projectileSpeed, this.projectileWidth, this.projectileHeight, this.offsetX, this.offsetY);
            this.projectileOffset = new Point(this.projectileOffsetX, this.projectileOffsetY);
        }

        public void UseWeapon(Room room, Rectangle ownerHitbox, Direction direction)
        {
            int x = ownerHitbox.X;
            int y = ownerHitbox.Y;

            switch (direction)
            {
                case Direction.UP:
                    x -= this.projectileOffset.Y;
                    y = ownerHitbox.Top - 16 - this.projectileOffset.X;
                    break;
                case Direction.LEFT:
                    x = ownerHitbox.Left - 16 - this.projectileOffset.X;
                    y += this.projectileOffset.Y;
                    break;
                case Direction.RIGHT:
                    x = ownerHitbox.Right + this.projectileOffset.X;
                    y += this.projectileOffset.Y;
                    break;
                case Direction.DOWN:
                    x += this.projectileOffset.Y - 2;
                    y = ownerHitbox.Bottom + this.projectileOffset.X;
                    break;
                default:
                    break;
            }

            room.SpawnProjectile(projectile.CreateEntity(direction), x, y);
            this.isWeaponReady = false;
        }

        public void Update(GameTime gameTime)
        {
            if(!this.isWeaponReady)
            {
                this.weaponCooldown += gameTime.ElapsedGameTime.Milliseconds;

                if (this.weaponCooldown >= this.attackSpeed)
                {
                    this.isWeaponReady = true;
                    this.weaponCooldown = 0;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.sprite.Draw(spriteBatch);
        }
    }
}
