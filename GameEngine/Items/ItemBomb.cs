using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zelda
{
    [Serializable]
    public class ItemBomb : Item
    {
        public ItemBomb() : base(12, ItemType.BOMB, new Rectangle(5, 2, 6, 12))
        {

        }

        public override void OnPickupItem(EntityPlayer player)
        {
            player.AddBomb(1);
        }
    }
}
