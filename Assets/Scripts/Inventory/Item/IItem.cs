using UnityEngine;

public interface IItem
{
    int Id { get; }
    int StorageId { get; }
    string Name { get; }
    float mass { get; }
    int Type { get; }
    Vector3 UIPosition { get; }
    Vector3 LinkedPosition { get; }
    string IconName { get; }
    string PrefabName { get; }

    void SetStorageId(int storageId);
}
