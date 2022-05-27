using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zelda
{
    public enum Direction
    {
        UP,
        LEFT,
        RIGHT,
        DOWN
    }

    [Serializable]
    public abstract class LivingEntity : Entity
    {
        protected int currentLife;
        protected int maxLife;
        protected int moveSpeed;
        protected Weapon weapon;

        protected bool invincible;
        protected Direction direction;

        [NonSerialized] protected int animationFrame;
        [NonSerialized] protected int animationTimer;
        [NonSerialized] protected int colorTimer;
        [NonSerialized] protected const int HIT_TIMER = 1000;

        public void SetWeapon(Weapon weapon)
        {
            this.weapon = weapon;
            if (this.weapon != null)
                this.weapon.SetOwner(this);
        }

        protected LivingEntity(Sprite sprite, int maxLife = 1, int moveSpeed = 1, int x = 0, int y = 0) : base(sprite, x, y)
        {
            this.sprite.SetLayerDepth(0.3f);
            this.currentLife = maxLife;
            this.maxLife = maxLife;
            this.moveSpeed = moveSpeed;
            this.weapon = Weapon.None;
            this.direction = Direction.DOWN;
            this.animationFrame = 0;
            this.animationTimer = 0;
            this.colorTimer = HIT_TIMER;
            this.invincible = false;
        }

        public int CurrentLife { get { return this.currentLife; } }
        public int MaxLife { get { return this.maxLife; } }
        public Weapon Weapon { get { return this.weapon; } }

        public override void InitializeLoad()
        {
            this.weapon?.Initialize();
            this.animationFrame = 0;
            this.animationTimer = 0;
            this.colorTimer = HIT_TIMER;

            this.sprite = new Sprite(this.spriteName, 0, 0, this.spriteCol, this.spriteRow);
            this.sprite.SetLayerDepth(0.3f);

            
            base.InitializeLoad();
        }

        public override void Save()
        {
            this.positionInt[0] = this.position.X;
            this.positionInt[1] = this.position.Y;
            this.positionInt[2] = this.position.Width;
            this.positionInt[3] = this.position.Height;

            this.baseHitboxInt[0] = this.baseHitbox.X;
            this.baseHitboxInt[1] = this.baseHitbox.Y;
            this.baseHitboxInt[2] = this.baseHitbox.Width;
            this.baseHitboxInt[3] = this.baseHitbox.Height;

            this.hitboxInt[0] = this.hitbox.X;
            this.hitboxInt[1] = this.hitbox.Y;
            this.hitboxInt[2] = this.hitbox.Width;
            this.hitboxInt[3] = this.hitbox.Height;
        }

        public virtual void Damage(int damage)
        {
            if (!this.invincible)
            {
                this.currentLife -= damage;
                this.sprite.SetColor(Color.Red);
                this.colorTimer = HIT_TIMER;
                this.invincible = true;

                if (this.currentLife <= 0)
                    this.Destroy();
            }
        }

        public void Heal(int amount)
        {
            if (this.currentLife + amount <= this.maxLife)
            {
                this.currentLife += amount;
            }
            else
            {
                this.currentLife = this.maxLife;
            }
        }

        public override void Move(int offsetX, int offsetY)
        {
            if (!this.hasMoved)
                this.animationTimer += 16;
                
            base.Move(offsetX, offsetY);

            if (offsetY < 0)
                this.direction = Direction.UP;
            if (offsetX < 0)
                this.direction = Direction.LEFT;
            if (offsetY > 0)
                this.direction = Direction.DOWN;
            if (offsetX > 0)
                this.direction = Direction.RIGHT;

        }

        public void UpdateSpriteAnimation(GameTime gameTime)
        {
            if (this.colorTimer >= 0)
                this.colorTimer -= gameTime.ElapsedGameTime.Milliseconds;
            else
            {
                this.sprite.SetColor(Color.White);
                this.colorTimer = HIT_TIMER;
                this.invincible = false;
            }


            if (this.animationFrame == 2)
            {
                this.animationTimer += gameTime.ElapsedGameTime.Milliseconds;

                if (this.animationTimer >= this.Weapon.AttackSpeed)
                {
                    this.animationFrame = 0;
                    this.animationTimer = 0;
                }
            }

            if (this.animationTimer >= 250)
            {
                this.animationTimer = 0;
                if (this.animationFrame == 0)
                    this.animationFrame = 1;
                else
                    this.animationFrame = 0;
            }


            switch (this.direction)
            {
                case Direction.UP:
                    this.sprite.SetCurrentFrame(2, this.animationFrame);
                    break;
                case Direction.LEFT:
                    this.sprite.SetCurrentFrame(1, this.animationFrame);
                    break;
                case Direction.DOWN:
                    this.sprite.SetCurrentFrame(0, this.animationFrame);
                    break;
                case Direction.RIGHT:
                    this.sprite.SetCurrentFrame(3, this.animationFrame);
                    break;
                default:
                    break;
            }
        }

        public override void UpdateChildren(GameTime gameTime, Input input)
        {
            if (this.weapon != null)
                this.weapon.Update(gameTime);
            this.UpdateSpriteAnimation(gameTime);
        }
    }
}
