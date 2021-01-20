using UnityEngine;
using UnityEngine.EventSystems;

public class DragEvent : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    int gridID = 0;
    public static int lastID;

    void Start()
    {
        gridID = GetComponentInParent<PickUpDrop>().gridID;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {        
        lastID = gridID;
        PickUpDrop.SwapItem(gridID);
    }
    
    public void OnDrag(PointerEventData eventData)
    {
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        PickUpDrop.SwapItem(gridID);
    }
}