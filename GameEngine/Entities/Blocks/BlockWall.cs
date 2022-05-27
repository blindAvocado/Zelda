using System;
using System.Collections.Generic;
using System.Text;

namespace Zelda
{
    [Serializable]
    public class BlockWall : EntityBlock
    {
        public BlockWall(int roomX, int roomY) : base(roomX, roomY, 1, 0, false)
        {
            this.roomX = roomX;
            this.roomY = roomY;
            this.spriteX = 1;
            this.spriteY = 0;
        }
    }
}
