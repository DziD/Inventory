using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Constants
{
    public class Items
    {
        public const int Crate = 0;
        public const int Apple = 1;
        public const int Battery = 2;
        public const int Bottle = 3;
        public const int Flashlight = 4;
        public const int Lighter = 5;
        public const int Rope = 6;
        public const int Signal_Rocket = 7;
        public const int Knife = 8;
    }    
}

public class InventorySystem : MonoBehaviour
{
    public UnityEvent<int, IItem> onItemAddedToBag;
    public UnityEvent<int, IItem> onItemRemovedFromBag;

    private static InventorySystem instance;
    
    public static readonly Dictionary<int, ItemDesc> itemsDataBase = new Dictionary<int, ItemDesc>();

    private Dictionary<int, InventoryBag> _inventoryBags = new Dictionary<int, InventoryBag>();

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);

        TextAsset itemsDB = Resources.Load("ItemsDB") as TextAsset;
        var items = JsonConvert.DeserializeObject<List<ItemDesc>>(itemsDB.text);
        for(int i = 0; i < items.Count; i++)
        {
            itemsDataBase.Add(items[i].type, items[i]);
        }
    }

    public static bool AddBag(int bagId)
    {
        if (instance == null)
        {
            return false;
        }

        instance._inventoryBags.Add(bagId, new InventoryBag() { id = bagId });

        return true;
    }

    public static InventoryBag GetBag(int bagId)
    {
        if (instance == null)
        {
            return null;
        }

        if (instance._inventoryBags.TryGetValue(bagId, out InventoryBag bag))
        {
            // add item
            return bag;
        }

        bag = new InventoryBag() { id = bagId };
        instance._inventoryBags.Add(bagId, bag);

        return bag;
    }

    public static bool RemoveBag(int bagId)
    {
        if (instance == null)
        {
            return false;
        }

        if (instance._inventoryBags.ContainsKey(bagId))
        {
            instance._inventoryBags.Remove(bagId);
        }

        return true;
    }

    public static bool AddItem(int bagId, IItem item)
    {
        if(instance == null)
        {
            return false;
        }        

        if(instance._inventoryBags.TryGetValue(bagId, out InventoryBag bag))
        {
            instance.onItemAddedToBag.Invoke(bagId, item);
            // add item
            return true;
        }

        return false;
    }

    public static bool RemoveItem(int bagId, IItem item)
    {
        if (instance == null)
        {
            return false;
        }

        if (instance._inventoryBags.TryGetValue(bagId, out InventoryBag bag))
        {
            instance.onItemRemovedFromBag.Invoke(bagId, item);
            return true;
        }

        return false;
    }

    public static bool ContainsItem(int bagId, IItem item)
    {
        if (instance == null)
        {
            return false;
        }

        return true;
    }

    public static IItem CreateItem(ItemDesc desc)
    {
        return new Item(desc);
    }
}