using System.Collections.Generic;

public static class ItemData
{
    public static List<Item> GetAllItems()
    {
        return new List<Item>
        {
            new Item("Rusty Key", "An old key, maybe opens something.", true, false, false),
            new Item("Torch", "Lights up dark rooms.", true, false, false),
            new Item("Ancient Scroll", "Contains a mysterious spell.", false, true, false),
            new Item("Rope", "Strong enough to climb with.", true, false, true, "Grappling Hook"),
            new Item("Grappling Hook", "A metal hook.", false, false, true, "Rope"),
            new Item("Potion", "Restores health when used.", true, false, false),
            new Item("Crystal Shard", "Glows faintly blue.", false, true, true, "Crystal Orb"),
            new Item("Crystal Orb", "Empty, needs a shard.", false, false, true, "Crystal Shard"),
            new Item("Map Fragment", "Part of a larger map.", false, true, false),
            new Item("Golden Coin", "Currency for the merchant NPC.", true, false, false),
            new Item("Lock Pick", "Opens locked doors.", true, false, false),
        };
    }
}