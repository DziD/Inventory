using UnityEngine;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour
{   
    private static ResourceManager instance;

    private readonly Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();
    private readonly Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();    

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);        
    }

    public static GameObject SpawnObject(string path)
    {
        if (instance == null)
        {
            return null;
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
}
