using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        IItem battery = InventorySystem.CreateItem(InventorySystem.itemsDataBase[Constants.Items.Battery]);
        SpawnItem(battery, new Vector3(-0.593f, 0.058f, 0.285f), Quaternion.Euler(0.0f, 66.8f, 0.0f));

        IItem bottle = InventorySystem.CreateItem(InventorySystem.itemsDataBase[Constants.Items.Bottle]);
        SpawnItem(bottle, new Vector3(-1.19f, 0.157f, 0.54f), Quaternion.Euler(0.0f, 217.103f, 0.0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameObject SpawnItem(IItem item, Vector3 position, Quaternion rotation)
    {
        var itemGo = Instantiate(Resources.Load<GameObject>(item.prefabName));
        
        itemGo.transform.parent = this.transform;
        itemGo.GetComponent<DragObject3D>().item = item;
        itemGo.transform.position = position;
        itemGo.transform.rotation = rotation;

        return itemGo;
    }
}
