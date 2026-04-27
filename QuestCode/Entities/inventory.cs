using System;
using System.Collections.Generic;

public class Inventory<T>
{
    // This is the internal list that holds your items
    public List<T> Items { get; set; } = new List<T>();

    public void AddItem(T item)
    {
        if (item != null)
        {
            Items.Add(item);
        }
    }

    public void RemoveItem(T item)
    {
        Items.Remove(item);
    }

    // This is the specific method the CommandParser is looking for
    public List<T> GetItems()
    {
        return Items;
    }
}