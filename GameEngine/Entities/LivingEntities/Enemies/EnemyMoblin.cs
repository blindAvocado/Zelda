using System;
using System.Collections.Generic;
using System.Text;

namespace Zelda
{
    [Serializable]
    public class EnemyMoblin : EntityEnemy
    {
        public EnemyMoblin() : base(new Sprite("enemy_moblin", 0, 0, 4, 2), 10, 1, Weapon.None, 2)
        {
            this.spriteName = "enemy_moblin";
            this.spriteCol = 4;
            this.spriteRow = 2;

            this.positionInt[0] = this.position.X;
            this.positionInt[1] = this.position.Y;
            this.positionInt[2] = this.position.Width;
            this.positionInt[3] = this.position.Height;

            this.baseHitboxInt[0] = this.baseHitbox.X;
            this.baseHitboxInt[1] = this.baseHitbox.Y;
            this.baseHitboxInt[2] = this.baseHitbox.Width;
            this.baseHitboxInt[3] = this.baseHitbox.Height;
        }
    }
}
