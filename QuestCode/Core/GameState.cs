public class GameState
{
    public Room CurrentRoom { get; set; }
    public Inventory<Item> PlayerInventory { get; set; }
    public bool IsRunning { get; set; }

    public GameState()
    {
        CurrentRoom = WorldBuilder.CreateWorld();
        PlayerInventory = new Inventory<Item>();
        IsRunning = true;
    }
}