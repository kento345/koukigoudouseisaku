using System;
using System.Runtime.InteropServices;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerJoinedManager : MonoBehaviour
{
    [SerializeField] private InputAction joinAction = default;
    [SerializeField] private GameObject playerPrefab = default;
    [SerializeField] private int maxPlayers = 4;
    //----------
    [SerializeField] private Text device1text;
    [SerializeField] private Text device2text;
    [SerializeField] private Text device3text;
    [SerializeField] private Text device4text;



    private InputDevice[] joinedDevices;
    private int currentCount = 0;

    private void Awake()
    { 
        joinedDevices = new InputDevice[maxPlayers];
        joinAction.Enable();
        joinAction.performed += OnJoin;

        //-----Text”ñ•\Ž¦-----
        device1text.enabled = false;
        device2text.enabled = false;
        device3text.enabled = false;
        device4text.enabled = false;
    }

    
    private void OnDestroy()
    {
        joinAction.performed -= OnJoin;
        joinAction.Disable();
    }

    private void OnJoin(InputAction.CallbackContext context)
    {
        if(currentCount >= maxPlayers) {return; }

        var device = context.control.device;
        foreach (var d in joinedDevices)
        {
            if(d == device) { return; }
        }

        joinedDevices[currentCount] = device;
        currentCount++;
        

       if(currentCount == 1)
        {
            device1text.enabled = true;
            device1text.text += $"Player {currentCount}: {device.displayName}\n";

        }
        if (currentCount == 2)
        {
            device2text.enabled = true;
            device2text.text += $"Player {currentCount}: {device.displayName}\n";
        }
        if(currentCount == 3)
        {
            device3text.enabled = true;
            device3text.text += $"Player {currentCount}: {device.displayName}\n";
        }
        if (currentCount == 4)
        {
            device4text.enabled = true;
            device4text.text += $"Player {currentCount}: {device.displayName}\n";
        }
    }



    public void OnGameStarte()
    {
        PlayerDataHolder.Instance.SetDevices(joinedDevices,currentCount);
        SceneManager.LoadScene("Main");
    }

}
