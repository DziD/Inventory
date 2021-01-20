using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIItem : MonoBehaviour
{
    public PickUpDrop[] Items;

    [SerializeField]
    private CanvasGroup canvasGroup = null;

    [HideInInspector]
    public PickUpDrop selectedItem = null;

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

    public bool SelectItem(Vector3 mousePosition)
    {
        selectedItem = null;

        for (int i = 0; i < Items.Length; i++)
        {
            Items[i].image.color = Color.white;
            var parallelogram = UIUtils.GetScreenParallelogramFromRect(Items[i].RectTransform, Camera.main);

            if (parallelogram.Contains(mousePosition))
            {
                selectedItem = Items[i];
                Items[i].image.color = Color.red;
            }
        }

        return selectedItem != null;
    }

    private void OnGUI()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            //Items[i].image.color = Color.white;
            var parallelogram = UIUtils.GetScreenParallelogramFromRect(Items[i].RectTransform, Camera.main);

            Parallelogram.DrawParallelogram(parallelogram);
        }
    }

    public void SpawnSelectedItem()
    {

    }

    public void ChangeAlpha()
    {
    }      
}
