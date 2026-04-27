using System;

public class Program
{
    public static void Main(string[] args)
    {
        GameState gameState;

        Console.WriteLine("--- Team QuestCode Puzzle Adventure ---");

        // 1. Attempt to LOAD the game right at the start
        var savedData = SaveSystem.Load();

        if (savedData != null)
        {
            Console.Write("Save file found! Would you like to LOAD your game? (yes/no): ");
            string answer = Console.ReadLine()?.ToLower() ?? "";

            if (answer == "yes")
            {
                gameState = savedData;
                Console.WriteLine("Success: Game state loaded.");
            }
            else
            {
                gameState = new GameState();
                Console.WriteLine("Starting a new game...");
            }
        }
        else
        {
            // If no save file exists, start fresh
            gameState = new GameState();
        }

        CommandParser parser = new CommandParser();

        // Show the player where they are starting
        parser.ProcessCommand("look", gameState);

        // 2. The main Game Loop
        while (gameState.IsRunning)
        {
            Console.Write("\n> ");
            string input = Console.ReadLine() ?? "";
            parser.ProcessCommand(input, gameState);
        }

        Console.WriteLine("\nExiting game. Press any key to close.");
        Console.ReadKey();
    }
}