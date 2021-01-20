using UnityEngine;

[System.Serializable]
public struct ItemDesc
{
    public string name;
    public string iconName;
    public string prefabName;
    public int type;
    public float mass;

    public Vector3 uiPosition;
    public Vector3 linkedPosition;
}