using System;
using System.Numerics;
using System.Security;

class Game
{
private Parser parser;
private Player player;
public Game ()
{ 
	parser = new Parser();
    player = new Player ();
	CreateRooms();
}
private void CreateRooms()

{


	// Initialise the Rooms (and the Items)

		// Create the rooms
		Room outside = new Room("outside the main entrance of the university");
		Room theatre = new Room("in a lecture theatre");
		Room pub = new Room("in the campus pub");
		Room lab = new Room("in a computing lab");
		Room office = new Room("in the computing admin office");
		Room window = new Room("Looking at the view out the window");
		Room balcony = new Room("looking at the balcony view");

		// Initialise room exits
		outside.AddExit("east", theatre);
		outside.AddExit("south", lab);
		outside.AddExit("west", pub);

		theatre.AddExit("west", outside);
		theatre.AddExit("up" , balcony);
		
		pub.AddExit("east", outside);

		lab.AddExit("north", outside);
		lab.AddExit("east", office);

		office.AddExit("west", lab);

		balcony.AddExit("down" , theatre);
		
		

		// Create your Items here 
		// ...
		// And add them to the Rooms
		// ...

		// Start game outside
		player.CurrentRoom  = outside;
	}

	//  Main play routine. Loops until end of play.
	public void Play()
	{
		PrintWelcome();

		// Enter the main command loop. Here we repeatedly read commands and
		// execute them until the player wants to quit.
		bool finished = false;
		while (!finished)
		{
			Command command = parser.GetCommand();
			finished = ProcessCommand(command);
		}
		Console.WriteLine("Thank you for playing.");
		Console.WriteLine("Press [Enter] to continue.");
		Console.ReadLine();
		while (!finished)
{
    if (!player.IsAlive())
    {
        Console.WriteLine("Game Over! You have died.");
        break;
    }

    Command command = parser.GetCommand();
    finished = ProcessCommand(command);
}

	}

	// Print out the opening message for the player.
	private void PrintWelcome()
	{
		Console.WriteLine();
		Console.WriteLine("Welcome to Zuul!");
		Console.WriteLine("Zuul is a new, incredibly boring adventure game.");
		Console.WriteLine("Type 'help' if you need help.");
		Console.WriteLine();
		Console.WriteLine(player.CurrentRoom .GetLongDescription());
	}

	// Given a command, process (that is: execute) the command.
	// If this command ends the game, it returns true.
	// Otherwise false is returned.
	private bool ProcessCommand(Command command)
	{
		bool wantToQuit = false;

		if(command.IsUnknown())
		{
			Console.WriteLine("I don't know what you mean...");
			return wantToQuit; // false
		}

		switch (command.CommandWord)
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
			look();
			break;
		    case "status":  
            player.Status();
           break;
		  case "damage":
    player.Damage(10); // Speler neemt 10 schade
    if (!player.IsAlive())
    {
        Console.WriteLine("Game Over! You have died.");
        wantToQuit = true;
    }  
    break;
	case "heal":
    player.RestoreHealth(); // Gezondheid herstellen
    break;

		} 
		

		return wantToQuit;
	}

	private void look()
     {
		Console.WriteLine(player.CurrentRoom .GetLongDescription());
	 }

	// ######################################
	// implementations of user commands:
	// ######################################
	
	// Print out some help information.
	// Here we print the mission and a list of the command words.
	private void PrintHelp()
	{
		Console.WriteLine("You are lost. You are alone.");
		Console.WriteLine("You wander around at the university.");
		Console.WriteLine();
		// let the parser print the commands
		parser.PrintValidCommands();
	}

	// Try to go to one direction. If there is an exit, enter the new
	// room, otherwise print an error message.
	private void GoRoom(Command command)
	{
		if(!command.HasSecondWord())
		{
			// if there is no second word, we don't know where to go...
			Console.WriteLine("Go where?");
			return;
		}

		string direction = command.SecondWord;

		// Try to go to the next room.
		Room nextRoom = player.CurrentRoom .GetExit(direction);
		if (nextRoom == null)
		{
			Console.WriteLine("There is no door to "+direction+"!");
			return;
		}

		player.CurrentRoom  = nextRoom;
		Console.WriteLine(player.CurrentRoom.GetLongDescription());
	}
}

