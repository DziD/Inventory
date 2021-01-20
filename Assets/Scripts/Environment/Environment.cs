using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    private static Environment instance;

    [SerializeField]
    private Camera mainCamera = null;

    [SerializeField]
    private Transform itemsRoot = null;

    [SerializeField]
    private Transform cratesRoot = null;

    private void Awake()
    {
        instance = this;
    }

    public static Camera GetCamera()
    {
        if (instance == null)
        {
            return null;
        }

        return instance.mainCamera;
    }
    public static Transform GetItemsRoot()
    {
        if (instance == null)
        {
            return null;
        }

        return instance.itemsRoot;
    }
    public static Transform GetCratesRoot()
    {
        if (instance == null)
        {
            return null;
        }

        return instance.cratesRoot;
    }
}
