using System.Collections.Generic;

public class InventoryStorage : IInventoryStorage
{
    private int id = 0;
    public int StorageID
    {
        get { return id; }
    }

    public InventoryStorage(int storageId)
    {
        this.id = storageId;
    }

    private ItemEvent _onItemAdded = new ItemEvent();
    public ItemEvent onItemAdded
    {
        get { return _onItemAdded; }
    }

    private ItemEvent _onItemRemoved = new ItemEvent();
    public ItemEvent onItemRemoved
    {
        get { return _onItemRemoved; }
    }

    private readonly List<IItem> _items = new List<IItem>();
    public List<IItem> Items
    {
        get
        {
            return _items;
        }
    }

    public bool AddItem(IItem item)
    {
        _items.Add(item);

        _onItemAdded.Invoke(item);

        return true;
    }

    public bool ContainsItem(IItem item)
    {
        return _items.Contains(item);
    }
    public bool RemoveItem(IItem item)
    {
        if (ContainsItem(item))
        {
            _items.Remove(item);
            _onItemRemoved.Invoke(item);
            return true;
        }

        return false;
    }
}
