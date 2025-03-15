using UnityEngine;
using Playroom;
using UnityCord;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class Game : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI responseText;

    PlayroomKit prk;

    List<Mapping> mappings = new() {
        new Mapping() {
            Prefix = "json",
            Target = "jsonplaceholder.typicode.com"
        }
    };

    void Awake()
    {
        prk = new();
    }

    void Start()
    {

#if !UNITY_EDITOR
        Utils.PatchUrlMappings(mappings);
#endif
        prk.InsertCoin(new()
        {
            gameId = "cW0r8UJ1aXnZ8v5TPYmv",
            discord = true,
        }, () => {
            Debug.Log("Coin Inserted");
        });
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.LogWarning("Unchange url, but using patchUrlMapping");
            StartCoroutine(GetRequest("https://jsonplaceholder.typicode.com/todos/1"));
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.LogWarning("Manual Proxy fetch request");
            StartCoroutine(GetRequest(".proxy/json/todos/2"));
        }
    }

    IEnumerator GetRequest(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Response: " + webRequest.downloadHandler.text);
                responseText.text = webRequest.downloadHandler.text;
            }
            else
            {
                Debug.LogError("Error: " + webRequest.error);
                responseText.text = webRequest.error;
            }
        }
    }
}