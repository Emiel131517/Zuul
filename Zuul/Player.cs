using System;
using System.Collections.Generic;
using System.Text;

namespace Zuul
{
    class Player
    {

        private Inventory inventory;
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
            inventory = new Inventory(20);
            health = 10;
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
        public bool TakeFromChest(string itemName)
        {
            Item item = currentRoom.Chest.Get(itemName);
            if (item == null)
            {
                Console.WriteLine("There is no " + itemName + " in your current room!");
                return false;
            }
            if (inventory.Put(itemName, item))
            {
                Console.WriteLine("You picked up " + itemName);
                return true;
            }
            Console.WriteLine("You do not have the space to carry " + itemName + " or you will be encumbered!");
            currentRoom.Chest.Put(itemName, item);
            return false;
        }
        public bool DropToChest(string itemName)
        {
            Item item = currentRoom.Chest.Get(itemName);
            if (currentRoom.Chest.Put(itemName, item))
            {
                Console.WriteLine("You dropped your item!");
                return true;
            }
            return false;
        }
    }
}
