using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MainGameManger : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab = default;

    void Start()
    {
        if(PlayerDataHolder.Instance == null) { return; }

        var devices = PlayerDataHolder.Instance.GetDevices();
        int count   = PlayerDataHolder.Instance.GetPlayerCount();

        for (int i = 0; i < count; i++)
        {
          var obj = PlayerInput.Instantiate(
                prefab: playerPrefab,
                playerIndex: i,
                pairWithDevice: devices[i]
             );

            var ui = obj.GetComponent<PlayerUI>();
            ui.SetCount( count );
        }

       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
