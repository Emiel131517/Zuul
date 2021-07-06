using System;

namespace Zuul
{
	public class Game
	{
		private Parser parser;
		private Player player;
		private bool test = false;

		public Game ()
		{
			player = new Player();
			parser = new Parser();
			CreateRooms();
		}

		private void CreateRooms()
		{
			// create the rooms
			Room outside = new Room("outside the main entrance of the university");
			Room theatre = new Room("in a lecture theatre");
			Room pub = new Room("in the campus pub");
			Room janitorsroom = new Room("in a janitors room");
			Room lab = new Room("in a computing lab");
			Room medicbay = new Room("In a medical bay");
			Room office = new Room("in the computing admin office");
			Room basement = new Room("In a foggy and smelly basement");
			Room cave = new Room("In a dark unknown cave");
			Room freedom = new Room("You made it to freedom and you are far away from the university");

			// initialise room exits
			outside.AddExit("east", theatre);
			outside.AddExit("south", lab);
			outside.AddExit("west", pub);
			outside.AddExit("down", basement);

			basement.AddExit("up", outside);
			basement.AddExit("west", cave);

			theatre.AddExit("west", outside);

			pub.AddExit("east", outside);
			pub.AddExit("west", janitorsroom);

			janitorsroom.AddExit("east", pub);

			lab.AddExit("north", outside);
			lab.AddExit("east", office);
			lab.AddExit("south", medicbay);

			medicbay.AddExit("north", lab);

			office.AddExit("west", lab);

			cave.AddExit("north", freedom);
			cave.LockDoor();

			freedom.FinalDestination = true;

			player.currentRoom = outside;  // start game outside

			// Items
			janitorsroom.Chest.Put("hammer", new Item(15, "A heavy hammer"));
			medicbay.Chest.Put("medkit", new Item(5, "A medical health kit"));
			office.Chest.Put("businesscard", new Item(1, "A boring looking and useless business card"));
			theatre.Chest.Put("harmonica", new Item(2, "A nice sounding harmonica"));
			pub.Chest.Put("beer", new Item(1, "A nice cold glass of beer"));
		}

		/**
		 *  Main play routine.  Loops until end of play.
		 */
		public void Play()
		{
			PrintWelcome();

			// Enter the main command loop.  Here we repeatedly read commands and
			// execute them until the player wants to quit
			bool finished = false;
			if (test == true)
			{
				finished = true;
			}
			if (player.Health < 1)
			{
				finished = true;
			} 
			while (!finished)
			{
				if (player.PlayerIsAlive() == true)
				{
					Command command = parser.GetCommand();
					finished = ProcessCommand(command);
					if (player.currentRoom.FinalDestination)
                    {
						finished = true;
						Console.WriteLine(" you finished");
                    }
				}
				else
                {
					Console.WriteLine("Oh no, you bled out!");
					Console.WriteLine("#####################");
					Console.WriteLine("      GAME OVER");
					Console.WriteLine("#####################");
					finished = true;
                }
			}
			Console.WriteLine("Thank you for playing.");
			Console.WriteLine("Press [Enter] to continue.");
			Console.ReadLine();

		}

		/**
		 * Print out the opening message for the player.
		 */
		private void PrintWelcome()
		{
			Console.WriteLine();
			Console.WriteLine("Welcome to Zuul!");
			Console.WriteLine("Zuul is a new, incredibly boring adventure game.");
			Console.WriteLine("Type 'help' if you need help.");
			Console.WriteLine();
			Console.WriteLine(player.currentRoom.GetLongDescription());
		}

		/**
		 * Given a command, process (that is: execute) the command.
		 * If this command ends the game, true is returned, otherwise false is
		 * returned.
		 */
		private bool ProcessCommand(Command command)
		{
			bool wantToQuit = false;

			if(command.IsUnknown())
			{
				Console.WriteLine("I don't know what you mean...");
				return false;
			}

			string commandWord = command.GetCommandWord();
			switch (commandWord)
			{
				case "help":
					PrintHelp();
					break;
				case "go":
					GoRoom(command);
					break;
				case "quit":
					wantToQuit = true;
					break;
				case "look":
					Look();
					break;
				case "status":
					player.Status();
					break;
				case "take":
					Take(command);
					break;
				case "drop":
					Drop(command);
					break;
				case "use":
					player.Use(command);
					break;
			}

			return wantToQuit;
		}

		// implementations of user commands:

		private void Look()
        {
			Console.WriteLine(player.currentRoom.GetLongDescription());
        }
		private void Take(Command command)
        {
			if (command.HasSecondWord())
            {
				player.TakeFromChest(command.GetSecondWord());
			}
        }
		private void Drop(Command command)
        {
			if (command.HasSecondWord())
            {
				player.DropToChest(command.GetSecondWord());
			}
        }

		/**
		 * Print out some help information.
		 * Here we print the mission and a list of the command words.
		 */
		private void PrintHelp()
		{
			Console.WriteLine("You are lost. You are alone.");
			Console.WriteLine("You wander around at the university.");
			Console.WriteLine();
			// let the parser print the commands
			parser.PrintValidCommands();
		}

		/**
		 * Try to go to one direction. If there is an exit, enter the new
		 * room, otherwise print an error message.
		 */
		private void GoRoom(Command command)
		{
			if(!command.HasSecondWord())
			{
				// if there is no second word, we don't know where to go...
				Console.WriteLine("Go where?");
				return;
			}

			string direction = command.GetSecondWord();

			// Try to go to the next room.
			Room nextRoom = player.currentRoom.GetExit(direction);

			if (nextRoom == null)
			{
				Console.WriteLine("There is no door to " + direction + "!");
			}
			else
			{
				if (nextRoom.Locked)
                {
					Console.WriteLine("This entrance is locked, you need something to open it");
					return;
                }
				player.currentRoom = nextRoom;
				Console.WriteLine(player.currentRoom.GetLongDescription());
				player.Damage(1);
			}
		}

	}
}
