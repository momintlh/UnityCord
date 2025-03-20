using UnityEngine;
using UnityCord;

public class GettingStarted : MonoBehaviour
{
    DiscordSDK discordSDK;

    public string clientId;

    void Awake()
    {
        discordSDK = new(clientId);
    }

    void Start()
    {
        discordSDK.Ready(() =>
        {
            Debug.LogWarning("Discord SDK is ready");
        });
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            discordSDK.Ready(() =>
            {
                Debug.LogWarning("Discord SDK is ready");
            });
        }
    }
}