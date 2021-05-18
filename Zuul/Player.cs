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
        private bool IsAlive()
        {
            if (health < 1)
            {
                isAlive = false;
                Die();
            }
            else
            {
                isAlive = true;
            }
            return isAlive;
        }
        private void Die()
        {
            Console.WriteLine();
            Console.WriteLine("You have no health left, you bled out!");
            Console.WriteLine("Thanks for playing! Press [ENTER] to quit");
            Console.ReadLine();
        }
        
        //Get info methods
        public int GetHealth()
        {
            return health;
        }
        public bool GetIsAlive()
        {
            IsAlive();
            return isAlive;
        }
    }
}
