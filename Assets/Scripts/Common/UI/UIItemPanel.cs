using UnityEngine;

public class UIItemPanel : MonoBehaviour
{
    public UIItem Item;

    [SerializeField]
    private CanvasGroup canvasGroup = null;

    [HideInInspector]
    public UIItem itemUnderCursor = null;

    public IItem item;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Environment.GetCamera();
    }

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0.0f;
    }

    public void Show()
    {
        canvasGroup.alpha = 1.0f;
    }

    public void Hide()
    {
        canvasGroup.alpha = 0.0f;
    }

    public bool CheckItemUnderCursor(Vector3 mousePosition)
    {
        itemUnderCursor = null;

        var parallelogram = UIUtils.GetScreenParallelogramFromRect(Item.RectTransform, mainCamera);
        Item.image.color = Color.white;

        if (parallelogram.Contains(mousePosition))
        {
            itemUnderCursor = Item;
            Item.image.color = Color.red;
        }

        return itemUnderCursor != null;
    }   
}
