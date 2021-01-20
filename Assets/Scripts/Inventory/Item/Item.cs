public interface IItem
{
    int id { get; }
    string name { get; }
    float mass { get; }
    int type { get; }

    string iconName { get; }
    string prefabName { get; }
}

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
}