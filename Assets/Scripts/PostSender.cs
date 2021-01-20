using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;


public class EventData
{
    public int id;
    public string eventName;
}
public class PostSender : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<ItemDesc> itemDesc = new List<ItemDesc>();

        for(int i = 0; i < 5; i++)
        {
            itemDesc.Add(new ItemDesc()
            {
                iconName = string.Format("ui/{0}.png", i),
                mass = 2.0f,
                name = "namesss",
                prefabName = "gameObject.nanan",
                type = 2
            });
        }

        string someText = JsonConvert.SerializeObject(itemDesc);
        Debug.Log(someText);
        File.WriteAllText(@"C:\Work\result.txt", someText);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(TestRequest());
        }
    }

    IEnumerator TestRequest()
    {
        Debug.LogWarning("Start Request");
        WWWForm form = new WWWForm();
        form.AddField("id", 13123);
        form.AddField("eventname", "removed");
        UnityWebRequest www = UnityWebRequest.Post("https://dev3r02.elysium.today/inventory/status", form);
        www.SetRequestHeader("auth", "BMeHG5xqJeB4qCjpuJCTQLsqNGaqkfB6");
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            //callback("null");
        }
        else
        {
            Debug.LogWarning("Receive " + www.downloadHandler.text);
        }
    }
}
