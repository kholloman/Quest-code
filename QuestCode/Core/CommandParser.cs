using System.Linq;

public class CommandParser
{
    public void ProcessCommand(string input, GameState gameState)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Please enter a command.");
            return;
        }

        string[] parts = input.ToLower().Split(" ");
        string command = parts[0];

        switch (command)
        {
            case "look":
                Look(gameState);
                break;

            case "go":
            case "move":
                if (parts.Length < 2)
                    Console.WriteLine("Go where?");
                else
                    Move(parts[1], gameState);
                break;

            case "take":
                if (parts.Length < 2)
                    Console.WriteLine("Take what?");
                else
                    TakeItem(string.Join(" ", parts.Skip(1)), gameState);
                break;

            case "inventory":
                gameState.PlayerInventory.ShowInventory();
                break;

            case "talk":
                Talk(gameState);
                break;

            case "help":
                ShowHelp();
                break;

            case "quit":
                gameState.IsRunning = false;
                Console.WriteLine("Game ended.");
                break;

            default:
                Console.WriteLine("Unknown command. Type 'help' for commands.");
                break;
                case "save":
               
            case "combine":
    if (parts.Length < 3)
        Console.WriteLine("Usage: combine item1 item2");
    else
        CombineItems(parts[1], parts[2], gameState);
    break;
    
    SaveManager.SaveGame(gameState);
    break;

case "load":
    SaveManager.LoadGame(gameState);
    break;
        }
    }

    private void Look(GameState gameState)
    {
        var room = gameState.CurrentRoom;

        Console.WriteLine($"\n{room.Name}");
        Console.WriteLine(room.Description);

        if (room.ItemsInRoom.Any())
        {
            Console.WriteLine("Items here:");
            foreach (var item in room.ItemsInRoom)
                Console.WriteLine("- " + item.Name);
        }

        if (room.RoomNPC != null)
        {
            Console.WriteLine("You see " + room.RoomNPC.Name + " here.");
        }

        Console.WriteLine("Exits: " + string.Join(", ", room.Exits.Keys));
    }

    private void Move(string direction, GameState gameState)
    {
        if (gameState.CurrentRoom.Exits.ContainsKey(direction))
        {
            gameState.CurrentRoom = gameState.CurrentRoom.Exits[direction];
            Look(gameState);
        }
        else
        {
            Console.WriteLine("You can't go that way.");
        }
    }

    private void TakeItem(string itemName, GameState gameState)
    {
        var item = gameState.CurrentRoom.ItemsInRoom
            .FirstOrDefault(i => i.Name.ToLower() == itemName.ToLower());

        if (item == null)
        {
            Console.WriteLine("Item not found.");
            return;
        }

        gameState.PlayerInventory.AddItem(item);
        gameState.CurrentRoom.ItemsInRoom.Remove(item);
    }

    private void Talk(GameState gameState)
    {
        if (gameState.CurrentRoom.RoomNPC == null)
        {
            Console.WriteLine("No one is here.");
            return;
        }

        gameState.CurrentRoom.RoomNPC.Talk(gameState.PlayerInventory);
    }

    private void ShowHelp()
    {
        Console.WriteLine("\nCommands:");
        Console.WriteLine("look");
        Console.WriteLine("go [direction]");
        Console.WriteLine("take [item]");
        Console.WriteLine("inventory");
        Console.WriteLine("talk");
        Console.WriteLine("help");
        Console.WriteLine("quit");
        Console.WriteLine("combine item1 item2");
    }
    
    private void CombineItems(string item1Name, string item2Name, GameState gameState)
{
    var item1 = gameState.PlayerInventory.FindItem(item1Name);
    var item2 = gameState.PlayerInventory.FindItem(item2Name);

    if (item1 == null || item2 == null)
    {
        Console.WriteLine("You don’t have those items.");
        return;
    }

    if (item1.IsCombinable && item1.CombinesWith.ToLower() == item2.Name.ToLower())
    {
        Console.WriteLine($"You combined {item1.Name} with {item2.Name}!");

        // Example effect
        Console.WriteLine("A powerful new tool is formed!");

        // Remove both items (optional logic)
        gameState.PlayerInventory.RemoveItem(item1.Name);
        gameState.PlayerInventory.RemoveItem(item2.Name);
    }
    else
    {
        Console.WriteLine("These items cannot be combined.");
    }
}
}