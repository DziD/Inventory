using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class PostSenderService : MonoBehaviour
{    
    private void Awake()
    {
        var storages = InventorySystem.GetAllStorages();
        if (storages != null)
        {
            for (var i = 0; i < storages.Count; i++)
            {
                OnAddedStorage(storages[i].StorageID);
            }
        }

        InventorySystem.onAddedStorage = OnAddedStorage;
        InventorySystem.onRemovedStorage = OnRemoveStorage;
    }
    private void OnDestroy()
    {
        InventorySystem.onAddedStorage = null;
        InventorySystem.onRemovedStorage = null;
    }    

    private void OnAddedStorage(int storageId)
    {
        var inventoryStorage = InventorySystem.GetStorage(storageId);
        inventoryStorage.onItemAdded.AddListener(OnItemAdded);
        inventoryStorage.onItemRemoved.AddListener(OnItemRemoved);
    }

    private void OnItemAdded(IItem item)
    {
        var storageId = item.StorageId;
        var itemId = item.Id;

        StartCoroutine(SendPostRequest(
                storageID: storageId,
                itemId: itemId,
                operationName: "added",
                errorCallback: delegate (string msg)
                {
                    Debug.LogErrorFormat("Error while add {0} to {1} with message {2}", itemId, storageId, msg);
                },
                successCallback: delegate (string msg)
                {
                    Debug.LogFormat("Successfully added {0} to {1} with message {2}", itemId, storageId, msg);
                }));
    }

    private void OnItemRemoved(IItem item)
    {
        var storageId = item.StorageId;
        var itemId = item.Id;
        
        StartCoroutine(SendPostRequest(
                storageID: storageId,
                itemId: itemId,
                operationName: "removed",
                errorCallback: delegate (string msg)
                {
                    Debug.LogErrorFormat("Error while remove {0} from {1} with message {2}", itemId, storageId, msg);
                },
                successCallback: delegate (string msg)
                {
                    Debug.LogFormat("Successfully removed {0} from {1} with message {2}", itemId, storageId, msg);
                }));
    }

    private void OnRemoveStorage(int storageId)
    {
        var inventoryStorage = InventorySystem.GetStorage(storageId);
        inventoryStorage.onItemAdded.RemoveListener(OnItemAdded);
        inventoryStorage.onItemRemoved.RemoveListener(OnItemRemoved);
    }


    private IEnumerator SendPostRequest(int storageID, int itemId, string operationName, Action<string> errorCallback, Action<string> successCallback)
    {
        WWWForm form = new WWWForm();
        form.AddField("storage", storageID.ToString());
        form.AddField("item", itemId.ToString());
        form.AddField("eventname", operationName);
        UnityWebRequest www = UnityWebRequest.Post("https://dev3r02.elysium.today/inventory/status", form);
        
        www.SetRequestHeader("auth", "BMeHG5xqJeB4qCjpuJCTQLsqNGaqkfB6");
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {            
            if(errorCallback != null)
            {
                errorCallback(www.error);
            }
        }
        else
        {            
            if (successCallback != null)
            {
                successCallback(www.downloadHandler.text);
            }            
        }
    }
}
