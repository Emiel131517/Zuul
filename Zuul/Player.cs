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
        private Random rnd = new Random();

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
        public void Status()
        {
            Console.WriteLine("[status] This is your status update:");
            Console.WriteLine("You are loosing blood! You have " + Health + " health left");
            Console.WriteLine("Your weight is: " + inventory.CurrentWeight());
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

            isAlive = false;

            if (health > 1)
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
            Item item = inventory.Get(itemName);
            if (item == null) 
            {
                
                return false;
            }
            Console.WriteLine("You dropped " + itemName);
            currentRoom.Chest.Put(itemName, item);
            return true;
        }
        public string Use(Command command)
        {
            string itemName = command.GetSecondWord();
            Item item = inventory.Get(itemName);
            if (item == null)
            {
                return "You don't have " + itemName; 
            }
            if (itemName == "medkit")
            {
                this.Heal(7);
                Console.WriteLine("You used a medkit!");
                this.inventory.Get("medkit");
            }
           if (itemName == "hammer")
            {
                string exitString = command.GetThirdWord();
                Room next = currentRoom.GetExit(exitString);
                next.Locked = false;
                Console.WriteLine("You rammed open the door");
                this.inventory.Get("hammer");
            }
           if (itemName == "harmonica")
            {
                this.Heal(3);
                Console.WriteLine("You play a nice song and you healed a bit, but the harmonica broke");
                this.inventory.Get("harmonica");
            }
           if (itemName == "beer")
            {
                int beerchance = rnd.Next(0, 2);
                if (beerchance == 0)
                {
                    this.Heal(5);
                    Console.WriteLine("You feel much better now and you healed alot");
                    this.inventory.Get("beer");
                }
                if (beerchance == 1)
                {
                    this.Damage(2);
                    Console.WriteLine("You got drunk and hit your toe, you lost some health");
                    this.inventory.Get("beer");
                }
            }
            return "";
        }
    }
}
