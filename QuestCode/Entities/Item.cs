public class Item
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsUsable { get; set; }
    public bool IsQuestItem { get; set; }
    public bool IsCombinable { get; set; }
    public string CombinesWith { get; set; }

    public Item(string name, string description, bool isUsable,
                bool isQuestItem, bool isCombinable, string combinesWith = "")
    {
        Name = name;
        Description = description;
        IsUsable = isUsable;
        IsQuestItem = isQuestItem;
        IsCombinable = isCombinable;
        CombinesWith = combinesWith;
    }
}