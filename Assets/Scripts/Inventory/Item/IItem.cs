using UnityEngine;

public interface IItem
{
    int id { get; }
    string name { get; }
    float mass { get; }
    int type { get; }
    Vector3 UIPosition { get; }
    Vector3 LinkedPosition { get; }
    string iconName { get; }
    string prefabName { get; }
}
