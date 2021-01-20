using System.Collections.Generic;

public class InventoryStorage : IInventoryStorage
{        
    public int StorageID { get; private set; }    

    public InventoryStorage(int storageId)
    {
        StorageID = storageId;
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
        item.SetStorageId(StorageID);
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
            _onItemRemoved.Invoke(item);
            item.SetStorageId(-1);
            _items.Remove(item);
            return true;
        }

        return false;
    }
}
