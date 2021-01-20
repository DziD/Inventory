using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventorySystem : MonoBehaviour
{
    public static Action<int> onAddedStorage;
    public static Action<int> onRemovedStorage;

    private static InventorySystem instance;
    private readonly Dictionary<IItem, ItemView> itemsOnScene = new Dictionary<IItem, ItemView>();
    private readonly Dictionary<int, InventoryStorage> _inventoryStorageDict = new Dictionary<int, InventoryStorage>();

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

        var inventoryStorage = new InventoryStorage(storageId);

        instance._inventoryStorageDict.Add(storageId, new InventoryStorage(storageId));
        if(onAddedStorage != null)
        {
            onAddedStorage(storageId);
        }
        return true;
    }
    public static IInventoryStorage GetStorage(int storageId)
    {
        if (instance == null)
        {
            return null;
        }

        if (instance._inventoryStorageDict.TryGetValue(storageId, out InventoryStorage storage))
        {
            return storage;
        }
        else
        {
            AddStorage(storageId);            
        }

        return instance._inventoryStorageDict[storageId];
    }

    public static List<IInventoryStorage> GetAllStorages()
    {
        if (instance == null)
        {
            return null;
        }

        return new List<IInventoryStorage>(instance._inventoryStorageDict.Values);
    }
    public static bool RemoveStorage(int storageId)
    {
        if (instance == null)
        {
            return false;
        }

        if (instance._inventoryStorageDict.TryGetValue(storageId, out InventoryStorage inventoryStorage))
        {
            if (onRemovedStorage != null)
            {
                onRemovedStorage(storageId);
            }

            instance._inventoryStorageDict.Remove(storageId);            
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

        var itemGo = ResourceManager.SpawnObject(item.PrefabName);
        var itemView = itemGo.GetComponent<ItemView>();
        itemView.Initialize(item);

        if(parent != null)
        {
            itemGo.transform.SetParent(parent);
        }

        instance.itemsOnScene.Add(item, itemView);

        return itemGo;
    }

    private static int id = 0;
    public static int GetID()
    {
        return ++id;
    }
}