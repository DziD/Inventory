using UnityEngine;

public class Item: IItem
{    
    public Item(ItemDesc desc)
    {
        Id = InventorySystem.GetID();
        StorageId = -1;
        this.desc = desc;
    }

    private ItemDesc desc;

    public int Id { get; private set; }

    public int StorageId { get; private set; }
    public string Name 
    { 
        get { return desc.name; }
    }

    public float mass 
    { 
        get { return desc.mass; }
    }

    public int Type
    {
        get { return desc.type; }
    }

    public string IconName
    {
        get { return desc.iconName; }
    }

    public string PrefabName
    {
        get { return desc.prefabName; }
    }

    public Vector3 UIPosition
    {
        get { return desc.uiPosition; }
    }

    public Vector3 LinkedPosition
    {
        get { return desc.linkedPosition; }
    }

    public void SetStorageId(int storageId)
    {
        this.StorageId = storageId;
    }
}