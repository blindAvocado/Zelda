using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Zelda
{
    public enum ItemType
    {
        KEY,
        BOMB,
        CONSUMABLE,
        UPGRADE,
        WEAPON,
    }

    public abstract class Item
    {
        protected Sprite sprite;
        protected ItemType type;
        protected Rectangle baseHitbox;

        public Sprite GetSprite()
        {
            return this.sprite;
        }
        public Rectangle GetBaseHitbox()
        {
            return this.baseHitbox;
        }


        protected Item(int index, ItemType type, Rectangle baseHitbox, int x = 0, int y = 0)
        {
            this.sprite = new Sprite("items", x, y, 8, 5, index % 8, index / 8);
            this.type = type;
            this.baseHitbox = baseHitbox;
        }


        public abstract void OnPickupItem(EntityPlayer player);
    }
}
