using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventorySystem : MonoBehaviour
{
    public UnityEvent<int, IItem> onItemAddedToStorage;
    public UnityEvent<int, IItem> onItemRemovedFromStorage;

    private static InventorySystem instance;
    private readonly Dictionary<IItem, ItemView> itemsOnScene = new Dictionary<IItem, ItemView>();
    private readonly Dictionary<int, InventoryStorage> _inventoryStorageList = new Dictionary<int, InventoryStorage>();

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }
    public static bool AddStorage(int storageId)
    {
        if (instance == null)
        {
            return false;
        }

        instance._inventoryStorageList.Add(storageId, new InventoryStorage(storageId));

        return true;
    }
    public static IInventoryStorage GetStorage(int storageId)
    {
        if (instance == null)
        {
            return null;
        }

        if (instance._inventoryStorageList.TryGetValue(storageId, out InventoryStorage storage))
        {
            return storage;
        }
        else
        {
            AddStorage(storageId);
        }

        return instance._inventoryStorageList[storageId];
    }
    public static bool RemoveStorage(int storageId)
    {
        if (instance == null)
        {
            return false;
        }

        if (instance._inventoryStorageList.ContainsKey(storageId))
        {
            instance._inventoryStorageList.Remove(storageId);
        }

        return true;
    }
    public static IItem CreateItem(ItemDesc desc)
    {
        return new Item(desc);
    }
    public static ItemView GetViewForItem(IItem item)
    {
        if (instance != null)
        {
            if (instance.itemsOnScene.TryGetValue(item, out ItemView itemView))
            {
                return itemView;
            }
        }

        return null;
    }
    public static GameObject SpawnItem(IItem item, Vector3 position, Transform parent = null)
    {
        if (instance == null)
        {
            return null;
        }

        var itemGo = ResourceManager.SpawnObject(item.prefabName);
        var itemView = itemGo.GetComponent<ItemView>();
        itemView.Initialize(item);

        if(parent != null)
        {
            itemGo.transform.SetParent(parent);
        }

        instance.itemsOnScene.Add(item, itemView);

        return itemGo;
    }
}