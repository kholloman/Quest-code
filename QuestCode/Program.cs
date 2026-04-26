public class Program
{
    public static void Main(string[] args)
    {
        GameState gameState = new GameState();
        CommandParser parser = new CommandParser();

        Console.WriteLine("Welcome to Team QuestCode Puzzle Adventure!");
        Console.WriteLine("Type 'help' to see commands.\n");

        // Show starting room
        parser.ProcessCommand("look", gameState);

        while (gameState.IsRunning)
        {
            Console.Write("\n> ");
            string input = Console.ReadLine();

            parser.ProcessCommand(input, gameState);
        }
    }
}