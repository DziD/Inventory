using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider))]
public class InventoryCrate : MonoBehaviour
{
    [SerializeField]
    private UIBag uiBag = null;

    public int CrateID = 0;

    public InventoryBag bag = null;
    private void Start()
    {
        bag = InventorySystem.GetBag(CrateID);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<DragObject3D>() != null)
        {
            other.gameObject.SetActive(false);
        }        
    }

    private void OnMouseDown()
    {
        uiBag.OpenBag();        
    }

    private void OnMouseUp()
    {
        if(uiBag.selectedItem != null)
        {
            uiBag.SpawnSelectedItem();
        }

        uiBag.CloseBag();
    }

    private void OnMouseDrag()
    {        
        uiBag.SelectItem(Input.mousePosition);
    }
}
