using System.Collections.Generic;

public class Room
{
    public string Name { get; set; }
    public string Description { get; set; }

    public Dictionary<string, Room> Exits { get; set; } = new Dictionary<string, Room>();
    public List<Item> ItemsInRoom { get; set; } = new List<Item>();
    public NPC RoomNPC { get; set; }

    public Room(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public void AddExit(string direction, Room neighbor)
    {
        Exits[direction.ToLower()] = neighbor;
    }
}