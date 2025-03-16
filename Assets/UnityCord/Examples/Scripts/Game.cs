using UnityEngine;
using Playroom;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Networking;
using TMPro;

namespace UnityCord
{
    public class Game : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI responseText;


        // Using Playroomkit for setting up discord auth and hosting the activity.
        PlayroomKit prk;

        // Creating the mappings for the external resources, we will need to add this in discord developer portal as well check : https://discord.com/developers/docs/activities/development-guides#using-external-resources
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

#if UNITY_WEBGL
            Utils.PatchUrlMappings(mappings);

#endif

            // initialize playroom with discord mode
            prk.InsertCoin(new()
            {
                gameId = "cW0r8UJ1aXnZ8v5TPYmv",
                discord = true,
            }, () =>
            {
                Debug.Log("Coin Inserted");
            });
        }


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                // Unchanged url, but using patchUrlMapping should fix this automatically, usefull if the request is made by a third party package
                StartCoroutine(GetRequest("https://jsonplaceholder.typicode.com/todos/1"));
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                //manual test by adding ./proxy, this should also work inside discord, usefull if you can't use patchUrlMapping or can edit the url.
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
}