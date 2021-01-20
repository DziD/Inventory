using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryBag
{
    public UnityEvent<IItem> onItemAdded;
    public UnityEvent<IItem> onItemRemoved;

    public int id;
    public List<IItem> items;
}
