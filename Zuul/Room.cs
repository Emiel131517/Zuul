using System.Collections.Generic;

namespace Zuul
{
	public class Room
	{
		private Inventory chest;
		public Inventory Chest 
		{ 
			get { return chest; } 
		}
		private bool locked;
		public bool Locked
		{
			get { return locked; }
			set { locked = value; }
		}
		private bool finalDestination = false;
		public bool FinalDestination
		{ 
			get { return finalDestination; } 
			set { finalDestination = value; }
		}

		private string description;
		private Dictionary<string, Room> exits; // stores exits of this room.

		/**
		 * Create a room described "description". Initially, it has no exits.
		 * "description" is something like "in a kitchen" or "in an open court
		 * yard".
		 */
		public Room(string desc)
		{
			description = desc;
			exits = new Dictionary<string, Room>();
			chest = new Inventory(100);
		}

		public bool UnlockDoor()
        {
			if (locked)
            {
				locked = false;
				return locked;
            }
			return true;
        }

		public bool LockDoor()
        {
			if (locked == false)
            {
				locked = true;
            }
			return true;
        }

		/**
		 * Define an exit from this room.
		 */
		public void AddExit(string direction, Room neighbor)
		{
			exits.Add(direction, neighbor);
		}

		/**
		 * Return the description of the room (the one that was defined in the
		 * constructor).
		 */
		public string GetShortDescription()
		{
			return description;
		}

		/**
		 * Return a long description of this room, in the form:
		 *     You are in the kitchen.
		 *     Exits: north, west
		 */
		public string GetLongDescription()
		{
			string str = "You are ";
			str += description;
			str += ".\n";
			str += GetExitString();
			str += ".\n";
			str += chest.ShowRoomItems();
			return str;
		}

		/**
		 * Return the room that is reached if we go from this room in direction
		 * "direction". If there is no room in that direction, return null.
		 */
		public Room GetExit(string direction)
		{
			if (exits.ContainsKey(direction)) {
				return exits[direction];
			} else {
				return null;
			}
		}

		/**
		 * Return a string describing the room's exits, for example
		 * "Exits: north, west".
		 */
		private string GetExitString()
		{
			string str = "Exits:";

			// because `exits` is a Dictionary, we use a `foreach` loop
			int countcommas = 0;
			foreach (string key in exits.Keys)
			{
				if (countcommas != 0)
				{
					str += ",";
				}
				str += " " + key;
				countcommas++;
			}

			return str;
		}
	}
}
