using UnityEngine;

public class UIItem : MonoBehaviour
{
    public PickUpDrop[] Items;

    [SerializeField]
    private CanvasGroup canvasGroup = null;

    [HideInInspector]
    public PickUpDrop itemUnderCursor = null;

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

        for (int i = 0; i < Items.Length; i++)
        {
            Items[i].image.color = Color.white;
            var parallelogram = UIUtils.GetScreenParallelogramFromRect(Items[i].RectTransform, mainCamera);

            if (parallelogram.Contains(mousePosition))
            {
                itemUnderCursor = Items[i];
                Items[i].image.color = Color.red;
            }
        }

        return itemUnderCursor != null;
    }

    //private void OnGUI()
    //{
    //    for (int i = 0; i < Items.Length; i++)
    //    {            
    //        var parallelogram = UIUtils.GetScreenParallelogramFromRect(Items[i].RectTransform, mainCamera);
    //        Parallelogram.DrawParallelogram(parallelogram);
    //    }
    //}
}
