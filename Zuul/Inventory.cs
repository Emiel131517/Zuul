using System;
using System.Collections.Generic;
using System.Text;

namespace Zuul
{
    class Inventory
    {
        private int maxWeight;
        private int currentWeight;
        private List<Item> items;

        public Inventory(int maxWeight)
        {
            this.maxWeight = maxWeight;
            this.items = new List<Item>();
        }
        public bool Put(Item item)
        {
            if (currentWeight + item.Weight <= maxWeight)
            {
                Console.WriteLine("You picked up " + item);
                items.Add(item);
                return true;
            }
            else
            {
                Console.WriteLine("You can't pick that up or you will be encumbered");
                return false;
            }
        }
        public Item Get(string itemName)
        {
            return null;
        }
    }
}
