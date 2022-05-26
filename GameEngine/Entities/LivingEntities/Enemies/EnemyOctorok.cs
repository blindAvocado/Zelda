using System;
using System.Collections.Generic;
using System.Text;

namespace Zelda
{
    public class EnemyOctorok : EntityEnemy
    {
        public EnemyOctorok() : base(new Sprite("enemy_octorok", 0, 0, 4, 2), 2, 1, Weapon.None, 1)
        {

        }
    }
}
