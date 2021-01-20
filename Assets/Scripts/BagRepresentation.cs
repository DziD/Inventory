using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider))]
public class BagRepresentation : MonoBehaviour
{
    private const string PATH_TO_UI_PANEL_PREFAB = "Prefabs/UI/Items/UIItemPanel";
    [SerializeField]
    private Transform uiParent;

    [SerializeField]
    private Transform itemsParent;

    private readonly Dictionary<IItem, UIItemPanel> uiItems = new Dictionary<IItem, UIItemPanel>();

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
        var dragObject = other.gameObject.GetComponent<ItemView>();
        if (dragObject != null)
        {
            inventoryStorage.AddItem(dragObject.item);
        }
    }
    private void OnMouseDown()
    {
        foreach (var pair in uiItems)
        {
            pair.Value.Show();
        }
    }

    private readonly List<IItem> itemsForRemove = new List<IItem>();
    private void OnMouseUp()
    {
        itemsForRemove.Clear();

        foreach (var pair in uiItems)
        {
            var uiItem = pair.Value;
            uiItem.Hide();

            if(uiItem.itemUnderCursor != null)
            {
                uiItem.gameObject.SetActive(false);
                inventoryStorage.RemoveItem(pair.Key);
                itemsForRemove.Add(pair.Key);
            }
        }

        for (int i = 0; i < itemsForRemove.Count; i++)
        {
            var item = uiItems[itemsForRemove[i]];
            ResourceManager.DespawnObject(item.gameObject, PATH_TO_UI_PANEL_PREFAB);
            uiItems.Remove(itemsForRemove[i]);            
        }
    }
    private void OnMouseDrag()
    {
        foreach (var pair in uiItems)
        {
            pair.Value.CheckItemUnderCursor(Input.mousePosition);
        }
    }

    private void OnItemAdded(IItem item)
    {        
        var itemView = InventorySystem.GetViewForItem(item);

        StartCoroutine(MoveObject(itemView,
            itemView.item.LinkedPosition,
            before: () =>
            {
                itemView.EnableIntercative(false);
            },
            after: () =>
            {
                itemView.transform.SetParent(itemsParent);
            }));
        
        uiItems.Add(item, CreateUIItem(item));
    }

    private void OnItemRemoved(IItem item)
    {
        var itemView = InventorySystem.GetViewForItem(item);

        StartCoroutine(MoveObject(itemView,
            new Vector3(UnityEngine.Random.Range(-0.8f, 0.8f), 0.1f, UnityEngine.Random.Range(-0.35f, 0.4f)),
            before: () =>
            {
                itemView.transform.SetParent(Environment.GetItemsRoot());
            },
            after: () =>
            {
                itemView.EnableIntercative(true);
            }));
    }

    private UIItemPanel CreateUIItem(IItem item)
    {
        var uiItemGo = ResourceManager.SpawnObject(PATH_TO_UI_PANEL_PREFAB);

        uiItemGo.transform.SetParent(uiParent, false);
        uiItemGo.transform.localPosition = item.UIPosition;
        uiItemGo.transform.rotation = Quaternion.Euler(18.0f, 0.0f, 0.0f);

        var uiItem = uiItemGo.GetComponent<UIItemPanel>();
        uiItem.Item.image.sprite = ResourceManager.GetSprite(item.iconName);
        uiItem.item = item;

        return uiItem;
    }

    private IEnumerator MoveObject(ItemView sourceObject, Vector3 targetPosition, Action before = null, Action after = null)
    {
        if (before != null)
        {
            before();
        }

        float t = 0.0f;
        float moveDuration = 0.2f;
        Vector3 start = sourceObject.transform.position;
        Vector3 end = targetPosition;

        while (t < moveDuration)
        {
            t += Time.deltaTime;
            sourceObject.transform.position = Vector3.Lerp(start, end, t / moveDuration);

            yield return null;
        }

        sourceObject.transform.position = targetPosition;

        if (after != null)
        {
            after();
        }
    }

}
