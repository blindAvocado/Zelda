using System;
using System.Collections.Generic;
using System.Text;

namespace Zelda
{
    public class EnemyMoblin : EntityEnemy
    {
        public EnemyMoblin() : base(new Sprite("enemy_moblin", 0, 0, 4, 2), 10, 1, Weapon.None, 2)
        {

        }
    }
}
