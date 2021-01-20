using System.Collections.Generic;
using UnityEngine.Events;

[System.Serializable]
public class ItemEvent : UnityEvent<IItem>
{
}
public interface IInventoryStorage
{
    int StorageID { get; }

    ItemEvent onItemAdded { get; }
    ItemEvent onItemRemoved { get; }
    List<IItem> Items { get; }

    bool AddItem(IItem item);
    bool ContainsItem(IItem item);
    bool RemoveItem(IItem item);
}
