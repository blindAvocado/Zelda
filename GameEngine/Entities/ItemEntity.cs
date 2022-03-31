using System;
using System.Collections.Generic;
using System.Text;

namespace Zelda
{
    public abstract class ItemEntity : Entity
    {
        protected ItemEntity(Sprite sprite, int x = 0, int y = 0) : base(sprite, x, y)
        {

        }
    }
}
