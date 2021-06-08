using System;
using System.Collections.Generic;
using System.Text;

namespace Zuul
{
    public class Inventory
    {
        private int maxWeight;
        private int currentWeight;
        private Dictionary<string, Item> items;

        public Inventory(int maxWeight)
        {
            this.maxWeight = maxWeight;
            this.items = new Dictionary<string, Item>();
        }
        public bool Put(string itemName, Item item)
        {
            if (currentWeight + item.Weight <= maxWeight)
            {
                currentWeight = currentWeight + item.Weight;
                items.Add(itemName, item);
                return true;
            }
            else
            {
                return false;
            }
        }
        public Item Get(string itemName)
        {
            if (items.ContainsKey(itemName))
            {
                Item item = items[itemName];
                items.Remove(itemName);
                return item;
            }
            return null;
        }
    }
}
