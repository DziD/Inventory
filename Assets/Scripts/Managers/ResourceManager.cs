using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;

public class ResourceManager : MonoBehaviour
{
    private static ResourceManager instance;

    private readonly Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();
    private readonly Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();

    public static readonly Dictionary<int, ItemDesc> ItemsDataBase = new Dictionary<int, ItemDesc>();

    private readonly Dictionary<string, List<GameObject>> poolObjects = new Dictionary<string, List<GameObject>>();

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);

        LoadItemsDB();
    }
    private void LoadItemsDB()
    {
        TextAsset itemsDB = Resources.Load("ItemsDB") as TextAsset;
        var items = JsonConvert.DeserializeObject<List<ItemDesc>>(itemsDB.text);
        for (int i = 0; i < items.Count; i++)
        {
            ItemsDataBase.Add(items[i].type, items[i]);
        }
    }
    public static GameObject SpawnObject(string path)
    {
        if (instance == null)
        {
            return null;
        }        

        if (instance.poolObjects.ContainsKey(path) && instance.poolObjects[path].Count > 0)
        {
            GameObject objectFromPool = instance.poolObjects[path][0];
            objectFromPool.SetActive(true);
            instance.poolObjects[path].RemoveAt(0);

            return objectFromPool;
        }

        if (instance.prefabs.TryGetValue(path, out GameObject prefab))
        {

        }
        else
        {
            prefab = Resources.Load<GameObject>(path);
            instance.prefabs.Add(path, prefab);
        }

        return prefab != null ? Instantiate(prefab) : null;
    }

    public static Sprite GetSprite(string path)
    {
        if (instance == null)
        {
            return null;
        }

        if (instance.sprites.TryGetValue(path, out Sprite requestedSprite))
        {
            return requestedSprite;
        }
        else
        {
            requestedSprite = Resources.Load<Sprite>(path);
        }

        return requestedSprite;
    }

    public static void DespawnObject(GameObject objectToDespawn, string pathToprefab = "")
    {
        if (instance == null)
        {
            return;
        }

        if(string.IsNullOrEmpty(pathToprefab))
        {
            Destroy(objectToDespawn);
        }

        if(!instance.poolObjects.ContainsKey(pathToprefab))
        {
            instance.poolObjects.Add(pathToprefab, new List<GameObject>());
        }

        objectToDespawn.transform.SetParent(instance.transform);
        objectToDespawn.SetActive(false);
        instance.poolObjects[pathToprefab].Add(objectToDespawn);
    }
}
