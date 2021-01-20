using UnityEngine;

public class SpawnItems : MonoBehaviour
{    
    void Start()
    {
        IItem battery = InventorySystem.CreateItem(ResourceManager.ItemsDataBase[Constants.Items.Battery]);
        IItem bottle = InventorySystem.CreateItem(ResourceManager.ItemsDataBase[Constants.Items.Bottle]);
        IItem apple = InventorySystem.CreateItem(ResourceManager.ItemsDataBase[Constants.Items.Apple]);

        InventorySystem.SpawnItem(battery, new Vector3(Random.Range(-1.1f, 1.6f), 0.2f, Random.Range(-0.3f, 1.1f)), Environment.GetItemsRoot());
        InventorySystem.SpawnItem(bottle, new Vector3(Random.Range(-1.1f, 1.6f), 0.2f, Random.Range(-0.3f, 1.1f)), Environment.GetItemsRoot());
        InventorySystem.SpawnItem(apple, new Vector3(Random.Range(-1.1f, 1.6f), 0.2f, Random.Range(-0.3f, 1.1f)), Environment.GetItemsRoot());       
    }
}
