using System;
using System.Collections.Generic;
using System.Text;

namespace Zuul
{
    class Player
    {
        public Room currentRoom {get; set;}

        public Player()
        {
            currentRoom = null;
        }
    }
}
