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
        Debug.Log("Drag");
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        Debug.Log(name + "Game Object Click in Progress");
    }
    
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        Debug.Log(name + "No longer being clicked");
    }
}
