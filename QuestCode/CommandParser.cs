using System;
using System.Collections.Generic;
using System.Linq;

public class CommandParser
{
    public void ProcessCommand(string input, GameState gameState)
    {
        if (string.IsNullOrWhiteSpace(input)) return;

        string[] parts = input.ToLower().Trim().Split(' ');
        string command = parts[0];

        switch (command)
        {
            case "look":
                Console.WriteLine($"\n--- {gameState.CurrentRoom.Name} ---");
                Console.WriteLine(gameState.CurrentRoom.Description);

                // --- SHOW ITEMS ---
                if (gameState.CurrentRoom.ItemsInRoom.Count > 0)
                {
                    Console.WriteLine("Items here: " + string.Join(", ", gameState.CurrentRoom.ItemsInRoom.Select(i => i.Name)));
                }

                // --- SHOW NPC ---
                if (gameState.CurrentRoom.RoomNPC != null)
                {
                    Console.WriteLine($"You see {gameState.CurrentRoom.RoomNPC.Name} here.");
                }

                // --- SHOW DIRECTIONS (EXITS) ---
                if (gameState.CurrentRoom.Exits.Count > 0)
                {
                    Console.WriteLine("Exits: " + string.Join(", ", gameState.CurrentRoom.Exits.Keys));
                }
                break;

            case "go":
                if (parts.Length > 1)
                {
                    string direction = parts[1];
                    if (gameState.CurrentRoom.Exits.ContainsKey(direction))
                    {
                        gameState.CurrentRoom = gameState.CurrentRoom.Exits[direction];
                        ProcessCommand("look", gameState); // Automatically show new room info
                    }
                    else
                    {
                        Console.WriteLine("You can't go that way.");
                    }
                }
                else { Console.WriteLine("Go where? (e.g., 'go north')"); }
                break;

            case "take":
                if (parts.Length > 1)
                {
                    if (parts[1] == "all") { TakeAllItems(gameState); }
                    else
                    {
                        string itemName = parts[1];
                        var item = gameState.CurrentRoom.ItemsInRoom.FirstOrDefault(i => i.Name.ToLower() == itemName);
                        if (item != null)
                        {
                            gameState.PlayerInventory.AddItem(item);
                            gameState.CurrentRoom.ItemsInRoom.Remove(item);
                            Console.WriteLine($"Added {item.Name} to inventory.");
                            CheckWinCondition(gameState);
                        }
                    }
                }
                break;

            case "inventory":
                Console.WriteLine("\n--- Inventory ---");
                var currentItems = gameState.PlayerInventory.GetItems();
                if (currentItems.Count == 0) Console.WriteLine("Your inventory is empty.");
                foreach (var item in currentItems)
                    Console.WriteLine($"- {item.Name}: {item.Description}");
                break;

            case "talk":
                if (gameState.CurrentRoom.RoomNPC != null)
                {
                    Console.WriteLine($"[{gameState.CurrentRoom.RoomNPC.Name}]: \"{gameState.CurrentRoom.RoomNPC.Dialogue}\"");
                    if (gameState.CurrentRoom.RoomNPC.ItemToGive != null)
                    {
                        var gift = gameState.CurrentRoom.RoomNPC.ItemToGive;
                        gameState.PlayerInventory.AddItem(gift);
                        Console.WriteLine($"The {gameState.CurrentRoom.RoomNPC.Name} hands you: {gift.Name}");
                        gameState.CurrentRoom.RoomNPC.ItemToGive = null;
                        CheckWinCondition(gameState);
                    }
                }
                else { Console.WriteLine("There is no one here to talk to."); }
                break;

            case "save":
                SaveSystem.Save(gameState);
                Console.WriteLine("Game saved successfully! Progress secured.");
                break;

            case "load":
                var loadedState = SaveSystem.Load();
                if (loadedState != null)
                {
                    gameState.CurrentRoom = loadedState.CurrentRoom;
                    gameState.PlayerInventory = loadedState.PlayerInventory;
                    Console.WriteLine("Game Loaded! Welcome back.");
                    ProcessCommand("look", gameState);
                }
                else { Console.WriteLine("No save file found."); }
                break;

            case "help":
                ShowHelp();
                break;

            case "quit":
                Environment.Exit(0);
                break;

            default:
                Console.WriteLine("Unknown command. Type 'help' for commands.");
                break;
        }
    }

    private void TakeAllItems(GameState gameState)
    {
        if (gameState.CurrentRoom.ItemsInRoom.Count == 0)
        {
            Console.WriteLine("There is nothing here to take.");
            return;
        }
        var itemsToTake = new List<Item>(gameState.CurrentRoom.ItemsInRoom);
        foreach (var item in itemsToTake)
        {
            gameState.PlayerInventory.AddItem(item);
            gameState.CurrentRoom.ItemsInRoom.Remove(item);
            Console.WriteLine($"Added {item.Name} to inventory.");
        }
        CheckWinCondition(gameState);
    }

    private void CheckWinCondition(GameState gameState)
    {
        if (gameState.IsWorldEmpty())
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n******************************************");
            Console.WriteLine("QUEST COMPLETE: ALL ITEMS HAVE BEEN FOUND!");
            Console.WriteLine("******************************************\n");
            Console.ResetColor();
        }
    }

    private void ShowHelp()
    {
        Console.WriteLine("\n--- AVAILABLE COMMANDS ---");
        Console.WriteLine("look              - Describe your current surroundings.");
        Console.WriteLine("go [direction]    - Move (e.g., 'go east', 'go down').");
        Console.WriteLine("take [item]       - Pick up a specific item.");
        Console.WriteLine("take all          - Pick up every item in the room at once!");
        Console.WriteLine("inventory         - View the items you are carrying.");
        Console.WriteLine("talk              - Speak to an NPC in the room.");
        Console.WriteLine("combine [i1] [i2] - Try to merge two items in your inventory.");
        Console.WriteLine("save              - Save your current progress.");
        Console.WriteLine("load              - Load your saved progress.");
        Console.WriteLine("help              - Show this list.");
        Console.WriteLine("quit              - Exit the game.");
    }
}