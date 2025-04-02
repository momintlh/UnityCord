using UnityEngine;
using Playroom;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;

namespace UnityCord
{
    public class GettingStartedPlayroom : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] TextMeshProUGUI responseText;
        [SerializeField] Button APIButton;
        [SerializeField] Button ExternalURLButton;

        // Using Playroomkit for setting up discord auth and hosting the activity.
        PlayroomKit prk;
        bool discordReady;

        // Creating the mappings for the external resources, we will need to add this in discord developer portal as well check : https://discord.com/developers/docs/activities/development-guides#using-external-resources
        List<Mapping> mappings = new()
        {
            new Mapping() { Prefix = "json", Target = "jsonplaceholder.typicode.com", }
        };

        void Awake()
        {
            prk = new();
            APIButton.interactable = false;
            ExternalURLButton.interactable = false;

            APIButton.onClick.AddListener(APICall);
            ExternalURLButton.onClick.AddListener(() =>
            {
#if UNITY_WEBGL && !UNITY_EDITOR
                Commands.OpenExternalLink("https://github.com/momintlh/unityCord");
#endif
            });
        }

        void Start()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            Utils.PatchUrlMappings(mappings);
#endif
            // Initialize Playroom with discord mode, playroom will handle auth for us!
            prk.InsertCoin(new()
            {
                gameId = "cW0r8UJ1aXnZ8v5TPYmv",
                discord = true,
            }, () =>
            {
                Debug.Log("Coin Inserted");
                prk.GetDiscordClient().Ready(() =>
                {
                    Debug.LogWarning("Discord SDK is ready through playroom woohoo!");
                    discordReady = true;
                    APIButton.interactable = true;
                    ExternalURLButton.interactable = true;
                });
            });
        }


        #region API Call
        private void APICall()
        {
            // Unchanged url, but using patchUrlMapping should fix this automatically, usefull if the request is made by a third party package
            StartCoroutine(GetRequest("https://jsonplaceholder.typicode.com/todos/1"));
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
        #endregion
    }
}