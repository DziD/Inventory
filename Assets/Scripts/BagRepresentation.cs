using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider))]
public class BagRepresentation : MonoBehaviour
{
    private struct AllInOne
    {
        public UIItem uiItem;
        public IItem item;
        public GameObject itemGo;
    }

    private List<UIItem> uiItems = new List<UIItem>();

    private readonly Dictionary<IItem, AllInOne> allInOne = new Dictionary<IItem, AllInOne>();

    private Dictionary<IItem, GameObject> gameObjects = new Dictionary<IItem, GameObject>();

    [SerializeField]
    private Transform uiParent;

    public int StorageId = 0;

    public IInventoryStorage inventoryStorage = null;
    private void Start()
    {
        inventoryStorage = InventorySystem.GetStorage(StorageId);

        inventoryStorage.onItemAdded.AddListener(OnItemAdded);
        inventoryStorage.onItemRemoved.AddListener(OnItemRemoved);
    }
    private void OnDestroy()
    {
        inventoryStorage.onItemAdded.RemoveListener(OnItemAdded);
        inventoryStorage.onItemRemoved.RemoveListener(OnItemRemoved);
    }
    private void OnTriggerEnter(Collider other)
    {
        var dragObject = other.gameObject.GetComponent<DragObject3D>();
        if (dragObject != null)
        {
            //dragObject.gameObject.SetActive(false);
            dragObject.EnableIntercative(false);
            dragObject.transform.position = dragObject.item.LinkedPosition;
            gameObjects.Add(dragObject.item, dragObject.gameObject);
            inventoryStorage.AddItem(dragObject.item);
        }
    }
    private void OnMouseDown()
    {
        for (int i = 0; i < uiItems.Count; i++)
        {
            uiItems[i].Show();
        }
    }
    private void OnMouseUp()
    {
        for (int i = uiItems.Count - 1; i >= 0; i--)
        {
            uiItems[i].Hide();
            if (uiItems[i].itemUnderCursor != null)
            {
                uiItems[i].gameObject.SetActive(false);
                inventoryStorage.RemoveItem(uiItems[i].item);
                uiItems.RemoveAt(i);
            }
        }
    }
    private void OnMouseDrag()
    {
        for (int i = 0; i < uiItems.Count; i++)
        {
            uiItems[i].CheckItemUnderMouse(Input.mousePosition);
        }
    }

    private void OnItemAdded(IItem item)
    {
        var uiItemGO = ResourceManager.SpawnObject("Prefabs/UI/Items/UIItem");

        uiItemGO.transform.parent = uiParent;
        uiItemGO.transform.localPosition = item.UIPosition;
        uiItemGO.transform.rotation = Quaternion.Euler(18.0f, 0.0f, 0.0f);

        var uiItem = uiItemGO.GetComponent<UIItem>();
        uiItem.Items[0].image.sprite = ResourceManager.GetSprite(item.iconName);
        //uiItem.Items[0].image.sprite = Resources.Load<Sprite>(item.iconName);
        uiItem.item = item;
        uiItems.Add(uiItem);

        allInOne.Add(item, new AllInOne()
        {
            item = item,
            itemGo = gameObjects[item],
            uiItem = uiItem
        });
    }

    private void OnItemRemoved(IItem item)
    {
        gameObjects[item].GetComponent<DragObject3D>().EnableIntercative(true);
        allInOne.Remove(item);
        gameObjects.Remove(item);
    }
}
