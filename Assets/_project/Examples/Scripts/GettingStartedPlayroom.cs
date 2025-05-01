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
        [SerializeField] Button InviteButton;
        [SerializeField] Button ReadyTestButton;
        [SerializeField] Button GetUserButton;
        [SerializeField] Button GetUserPlayroomButton;

        // Using Playroomkit for setting up discord auth and hosting the activity.
        PlayroomKit prk;
        bool discordReady;

        // Creating the mappings for the external resources, we will need to add this in discord developer portal as well check : https://discord.com/developers/docs/activities/development-guides#using-external-resources
        List<Mapping> mappings = new()
        {
            new Mapping() { Prefix = "json", Target = "jsonplaceholder.typicode.com", }
        };

        private DiscordSDK discordSDK;

        void Awake()
        {
            prk = new();

            APIButton.onClick.AddListener(APICall);
            ExternalURLButton.onClick.AddListener(() =>
            {
                discordSDK.commands.OpenExternalLink("https://github.com/momintlh/unityCord");
            });
            InviteButton.onClick.AddListener(() =>
            {
                // #if UNITY_WEBGL && !UNITY_EDITOR
                // Commands.InviteButton("https://github.com/momintlh/unityCord");
                discordSDK.commands.OpenInviteDialog();
                // #endif
            });
            ReadyTestButton.onClick.AddListener(() =>
            {
                discordSDK.Ready(() =>
               {
                   Debug.LogWarning("Discord SDK is ready through playroom woohoo!");
                   discordReady = true;
               });
            });
            GetUserButton.onClick.AddListener(() =>
            {
                discordSDK.commands.GetUser(prk.Me().id, (data) =>
                {
                    Debug.LogWarning($"User data: {data}");
                    responseText.text = data;
                });
            });
            GetUserPlayroomButton.onClick.AddListener(() =>
            {
                try
                {
                    var player = prk.MyPlayer();
                    var profile = player.GetProfile();
                    responseText.text = string.Format(
                        "Playroom User Info:\n" +
                        "Name: {0}\n" +
                        "Photo: {1}\n" +
                        "ID: {2}",
                        profile.name,
                        profile.photo,
                        player.id
                    );
                }
                catch (System.Exception e)
                {
                    Debug.LogError("Failed to get player info: " + e.Message);
                    responseText.text = "Error: Could not retrieve player information";
                }
            });
        }

        void Start()
        {
            Utils.PatchUrlMappings(mappings);

            // Initialize Playroom with discord mode, playroom will handle auth for us!
            prk.InsertCoin(new()
            {
                gameId = "cW0r8UJ1aXnZ8v5TPYmv",
                discord = true,
            }, () =>
            {
                discordSDK = prk.GetDiscordClient();
                Debug.LogWarning("Coin Inserted");
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