using System.Collections.Generic;

public static class WorldBuilder
{
    public static Room CreateWorld()
    {
        Room foyer = new Room("Grand Foyer", "A large entrance hall with a dusty chandelier.");
        Room kitchen = new Room("Kitchen", "The smell of old soup lingers. Dirty pots hang from the ceiling.");
        Room library = new Room("Ancient Library", "Floor-to-ceiling bookshelves surround you.");
        Room armory = new Room("The Armory", "Racks of rusted swords and shields line the walls.");
        Room cellar = new Room("Wine Cellar", "It is dark, damp, and smells of fermented grapes.");

        foyer.AddExit("north", library);
        library.AddExit("south", foyer);

        foyer.AddExit("west", kitchen);
        kitchen.AddExit("east", foyer);

        library.AddExit("east", armory);
        armory.AddExit("west", library);

        kitchen.AddExit("down", cellar);
        cellar.AddExit("up", kitchen);

        var allItems = ItemData.GetAllItems();

        foyer.ItemsInRoom.Add(allItems.Find(i => i.Name == "Torch"));

        kitchen.ItemsInRoom.Add(allItems.Find(i => i.Name == "Rusty Key"));
        kitchen.ItemsInRoom.Add(allItems.Find(i => i.Name == "Potion"));

        library.ItemsInRoom.Add(allItems.Find(i => i.Name == "Map Fragment"));
        library.ItemsInRoom.Add(allItems.Find(i => i.Name == "Crystal Orb"));

        armory.ItemsInRoom.Add(allItems.Find(i => i.Name == "Rope"));
        armory.ItemsInRoom.Add(allItems.Find(i => i.Name == "Grappling Hook"));
        armory.ItemsInRoom.Add(allItems.Find(i => i.Name == "Lock Pick"));

        cellar.ItemsInRoom.Add(allItems.Find(i => i.Name == "Golden Coin"));
        cellar.ItemsInRoom.Add(allItems.Find(i => i.Name == "Crystal Shard"));

        NPC ghost = new NPC(
            "The Librarian",
            "I have protected these books for centuries... take this scroll.",
            allItems.Find(i => i.Name == "Ancient Scroll")
        );

        library.RoomNPC = ghost;

        return foyer;
    }
}