using System;
using System.Collections.Generic;
using System.Text;

namespace Zelda
{
    public class BlockWall : EntityBlock
    {
        public BlockWall(int roomX, int roomY) : base(roomX, roomY, 1, 0, false)
        {

        }
    }
}
