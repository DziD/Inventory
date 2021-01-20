using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider))]
public class InventoryCrate : MonoBehaviour
{
    [SerializeField]
    private List<UIItem> uiItems = new List<UIItem>();

    public int CrateID = 0;

    public InventoryBag bag = null;
    private void Start()
    {
        bag = InventorySystem.GetBag(CrateID);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<DragObject3D>() != null)
        {
            other.gameObject.SetActive(false);
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
        for (int i = 0; i < uiItems.Count; i++)
        {
            if (uiItems[i].selectedItem != null)
            {
                uiItems[i].SpawnSelectedItem();
            }

            uiItems[i].Hide();
        }
    }

    private void OnMouseDrag()
    {
        for (int i = 0; i < uiItems.Count; i++)
        {
            uiItems[i].SelectItem(Input.mousePosition);
        }
    }
}
