using System;
using System.Collections.Generic;
using System.Text;

namespace Zuul
{
    class Inventory
    {
        private int maxWeight;
        private int currentWeight;
        private Dictionary<string, Item> items;

        public Inventory(int maxWeight)
        {
            this.maxWeight = maxWeight;
            this.items = new Dictionary<string, Item>();
        }
        public bool Put(Item item)
        {
            if (currentWeight + item.Weight <= maxWeight)
            {
                Console.WriteLine("You picked up " + item);
                items.Add(null, item);
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
            if (items.ContainsKey(itemName))
            {
                return items[itemName];
            }
            return null;
        }
    }
}
