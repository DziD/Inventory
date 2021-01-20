using UnityEngine;

public class SpawnItems : MonoBehaviour
{    
    void Start()
    {
        IItem battery = InventorySystem.CreateItem(InventorySystem.itemsDataBase[Constants.Items.Battery]);
        var batteryGo = InventorySystem.SpawnItem(battery, new Vector3(-0.593f, 0.058f, 0.285f));

        IItem bottle = InventorySystem.CreateItem(InventorySystem.itemsDataBase[Constants.Items.Bottle]);
        var bottleGo = InventorySystem.SpawnItem(bottle, new Vector3(-1.19f, 0.157f, 0.54f));

        IItem apple = InventorySystem.CreateItem(InventorySystem.itemsDataBase[Constants.Items.Apple]);
        var appleGo = InventorySystem.SpawnItem(apple, new Vector3(1.921f, 0.05f, 0.584f));
    }
}
