using System;
using System.Collections.Generic;
using System.Text;

namespace Zuul
{
    class Player
    {

        private int health;
        private bool isAlive; 

        public Room currentRoom { get; set; }
        public int Health
        {
            get { return health; }
        }
        public bool IsAlive
        {
            get { return isAlive; }
        }

        public Player()
        {
            health = 2;
            currentRoom = null;
        }
        public int Damage(int amount)
        {
            health = health - amount;
            return amount;
        }
        public int Heal(int amount)
        {
            health = health + amount;
            return amount;
        }
        public bool PlayerIsAlive()
        {
            if (health < 1)
            {
                isAlive = false;
            }
            else
            {
                isAlive = true;
            }
            return isAlive;
        }
    }
}
