using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zelda
{
    public class ItemEntity : Entity
    {
        public Item item;

        public ItemEntity(Item item, int x = 0, int y = 0) : base(item.GetSprite(), x, y, item.GetBaseHitbox())
        {
            this.sprite.SetLayerDepth(0.15f);
            this.item = item;
        }

        public void PickupItem(EntityPlayer player)
        {
            this.item.OnPickupItem(player);
            this.Destroy();
        }

        public override void UpdateChildren(GameTime gameTime, Input input)
        {
        }
    }

}
