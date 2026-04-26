public class NPC
{
    public string Name { get; set; }
    public string Dialogue { get; set; }
    public Item ItemToGive { get; set; }

    public NPC(string name, string dialogue, Item itemToGive = null)
    {
        Name = name;
        Dialogue = dialogue;
        ItemToGive = itemToGive;
    }

    public void Talk(Inventory<Item> playerInventory)
    {
        Console.WriteLine($"\n[{Name}]: \"{Dialogue}\"");

        if (ItemToGive != null)
        {
            Console.WriteLine($"{Name} hands you: {ItemToGive.Name}");
            playerInventory.AddItem(ItemToGive);
            ItemToGive = null;
        }
    }
}