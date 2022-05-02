using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Zelda
{
    public class ItemKey : Item
    {
        public ItemKey(): base(19, ItemType.KEY, new Rectangle(5, 1, 6, 14))
        {

        }

        public override void OnPickupItem(EntityPlayer player)
        {
            player.AddKey(1);
        }
    }
}
