using System;

public class NPC
{
    public string Name { get; set; }
    public string Dialogue { get; set; }

    // The '?' allows this to be null without warnings
    public Item? ItemToGive { get; set; }

    public NPC(string name, string dialogue, Item? itemToGive = null)
    {
        Name = name;
        Dialogue = dialogue;
        ItemToGive = itemToGive;
    }
}