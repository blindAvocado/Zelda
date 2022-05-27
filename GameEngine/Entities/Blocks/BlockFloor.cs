using System;
using System.Collections.Generic;
using System.Text;

namespace Zelda
{
    [Serializable]
    public class BlockFloor : EntityBlock
    {
        public BlockFloor(int roomX, int roomY) : base(roomX, roomY, 0, 0)
        {
            this.roomX = roomX;
            this.roomY = roomY;
            this.spriteX = 0;
            this.spriteY = 0;
        }
    }
}
