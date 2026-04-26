using System.Collections.Generic;
using System.Linq;

public class Inventory<T> where T : Item
{
    private List<T> _items = new List<T>();
    private HashSet<string> _itemNames = new HashSet<string>();
    private Dictionary<string, T> _itemLookup = new Dictionary<string, T>();

    public void AddItem(T item)
    {
        if (!_itemNames.Contains(item.Name))
        {
            _items.Add(item);
            _itemNames.Add(item.Name);
            _itemLookup[item.Name] = item;
            Console.WriteLine($"Added {item.Name} to inventory.");
        }
        else
        {
            Console.WriteLine($"{item.Name} is already in your inventory.");
        }
    }

    public void RemoveItem(string name)
    {
        var item = _itemLookup.GetValueOrDefault(name);
        if (item != null)
        {
            _items.Remove(item);
            _itemNames.Remove(name);
            _itemLookup.Remove(name);
        }
    }

    public T GetItem(string name) => _itemLookup.GetValueOrDefault(name);

    public void ShowInventory()
    {
        if (!_items.Any())
        {
            Console.WriteLine("Your inventory is empty.");
            return;
        }

        Console.WriteLine("\n--- Inventory ---");
        foreach (var item in _items)
            Console.WriteLine($"- {item.Name}: {item.Description}");
    }

    public List<T> GetUsableItems() => _items.Where(i => i.IsUsable).ToList();
    public List<T> GetQuestItems() => _items.Where(i => i.IsQuestItem).ToList();
    public List<T> GetCombinableItems() => _items.Where(i => i.IsCombinable).ToList();

    public T FindItem(string name) =>
        _items.FirstOrDefault(i => i.Name.ToLower() == name.ToLower());

    public bool HasItem(string name) =>
        _items.Any(i => i.Name.ToLower() == name.ToLower());

    public int Count => _items.Count;
}