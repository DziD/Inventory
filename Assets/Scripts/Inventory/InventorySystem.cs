using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventorySystem : MonoBehaviour
{
    public UnityEvent<int, IItem> onItemAddedToStorage;
    public UnityEvent<int, IItem> onItemRemovedFromStorage;

    private static InventorySystem instance;

    public static readonly Dictionary<int, ItemDesc> itemsDataBase = new Dictionary<int, ItemDesc>();

    private Dictionary<int, InventoryStorage> _inventoryStorageList = new Dictionary<int, InventoryStorage>();
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);

        TextAsset itemsDB = Resources.Load("ItemsDB") as TextAsset;
        var items = JsonConvert.DeserializeObject<List<ItemDesc>>(itemsDB.text);
        for (int i = 0; i < items.Count; i++)
        {
            itemsDataBase.Add(items[i].type, items[i]);
        }
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

    public static GameObject SpawnItem(IItem item, Vector3 position, Transform parent = null)
    {
        var itemGo = ResourceManager.SpawnObject(item.prefabName);
        var dragObject3D = itemGo.GetComponent<DragObject3D>();
        dragObject3D.Initialize(item);

        return itemGo;
    }
}