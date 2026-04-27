using System.Collections.Generic;

public class GameState
{
    public Room CurrentRoom { get; set; }
    public Inventory<Item> PlayerInventory { get; set; }
    public bool IsRunning { get; set; }

    // This allows the win condition to check every room for items
    public List<Room> AllRooms { get; set; } = new List<Room>();

    public GameState()
    {
        // We set up the world and store the rooms list
        CurrentRoom = WorldBuilder.CreateWorld(this);
        PlayerInventory = new Inventory<Item>();
        IsRunning = true;
    }

    // This helper method makes the win check very easy for the CommandParser
    public bool IsWorldEmpty()
    {
        foreach (var room in AllRooms)
        {
            if (room.ItemsInRoom.Count > 0) return false;
        }
        return true;
    }
}