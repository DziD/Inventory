using UnityEngine;

public class Item: IItem
{
    public static int ID = 0;
    public Item(ItemDesc desc)
    {
        id = ++ID;
        this.desc = desc;
    }

    private ItemDesc desc;

    public int id { get; private set; }
    public string name 
    { 
        get { return desc.name; }
    }

    public float mass 
    { 
        get { return desc.mass; }
    }

    public int type
    {
        get { return desc.type; }
    }

    public string iconName
    {
        get { return desc.iconName; }
    }

    public string prefabName
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
}