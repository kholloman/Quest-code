using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;

using System.Text.Json;

public class SaveData
{
    public string CurrentRoomName { get; set; }
    public List<string> InventoryItems { get; set; } = new List<string>();
}

public static class SaveManager
{
    private static string filePath = "savegame.json";

    public static void SaveGame(GameState gameState)
    {
        SaveData data = new SaveData
        {
            CurrentRoomName = gameState.CurrentRoom.Name,
            InventoryItems = gameState.PlayerInventory
                .GetUsableItems()
                .Concat(gameState.PlayerInventory.GetQuestItems())
                .Concat(gameState.PlayerInventory.GetCombinableItems())
                .Select(i => i.Name)
                .Distinct()
                .ToList()
        };

        string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, json);

        Console.WriteLine("Game saved.");
    }

    public static void LoadGame(GameState gameState)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("No save file found.");
            return;
        }

        string json = File.ReadAllText(filePath);
        SaveData data = JsonSerializer.Deserialize<SaveData>(json);

        gameState.CurrentRoom = FindRoom(WorldBuilder.CreateWorld(), data.CurrentRoomName);
        gameState.PlayerInventory = new Inventory<Item>();

        var allItems = ItemData.GetAllItems();

        foreach (var itemName in data.InventoryItems)
        {
            var item = allItems.FirstOrDefault(i => i.Name == itemName);
            if (item != null)
                gameState.PlayerInventory.AddItem(item);
        }

        Console.WriteLine("Game loaded.");
    }

    private static Room FindRoom(Room start, string name)
    {
        Queue<Room> queue = new Queue<Room>();
        HashSet<string> visited = new HashSet<string>();

        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            var room = queue.Dequeue();

            if (room.Name == name)
                return room;

            visited.Add(room.Name);

            foreach (var next in room.Exits.Values)
            {
                if (!visited.Contains(next.Name))
                    queue.Enqueue(next);
            }
        }

        return start;
    }
}