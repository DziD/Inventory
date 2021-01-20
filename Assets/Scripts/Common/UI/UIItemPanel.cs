using UnityEngine;

public class UIItemPanel : MonoBehaviour
{
    [HideInInspector]
    public UIItem ItemUnderCursor = null;

    public IItem Item { get; private set; }

    [SerializeField]
    private UIItem uiItem;
    [SerializeField]
    private CanvasGroup canvasGroup = null;

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

    public void UpdateItem(IItem item)
    {
        this.Item = item;
        uiItem.image.sprite = ResourceManager.GetSprite(item.IconName);
    }

    public bool CheckItemUnderCursor(Vector3 mousePosition)
    {
        ItemUnderCursor = null;

        var parallelogram = UIUtils.GetScreenParallelogramFromRect(uiItem.RectTransform, mainCamera);
        uiItem.image.color = Color.white;

        if (parallelogram.Contains(mousePosition))
        {
            ItemUnderCursor = uiItem;
            uiItem.image.color = Color.red;
        }

        return ItemUnderCursor != null;
    }   
}
