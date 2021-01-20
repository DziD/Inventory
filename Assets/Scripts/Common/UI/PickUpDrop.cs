using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PickUpDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public int gridID;

    public RectTransform RectTransform;

    public Image image = null;

    public static void SwapItem(int gridID)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {        
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {        
    }
    
    public void OnPointerUp(PointerEventData pointerEventData)
    {        
    }
}
