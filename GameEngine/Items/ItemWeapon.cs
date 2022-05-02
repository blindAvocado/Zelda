using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Zelda
{
    class ItemWeapon : Item
    {
        private Weapon weapon;

        public ItemWeapon(Weapon weapon): base(weapon.Index, ItemType.WEAPON, new Rectangle(5, 1, 6, 14))
        {
            this.weapon = weapon;
        }

        public override void OnPickupItem(EntityPlayer player)
        {
            player.SetWeapon(this.weapon);
        }
    }
}
