using System;
using System.Collections.Generic;
using System.Text;

namespace Zelda
{
    [Serializable]
    public class BlockHole : EntityBlock
    {
        public BlockHole(int roomX, int roomY) : base(roomX, roomY, 2, 0, false)
        {
            this.roomX = roomX;
            this.roomY = roomY;

            this.spriteX = 2;
            this.spriteY = 0;
        }
    }
}
