using System;
using System.Runtime.InteropServices;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerJoinedManager : MonoBehaviour
{
    [SerializeField] private InputAction joinAction = default;  //参加するときの入力
    [SerializeField] private InputAction leaveAction = default;  //参加するときの入力

    [SerializeField] private int maxPlayers = 4;        //参加上限
    //----------
    [SerializeField] private Text device1text;         //1デバイス名Text
    [SerializeField] private Text device2text;         //2デバイス名Text
    [SerializeField] private Text device3text;         //3デバイス名Text
    [SerializeField] private Text device4text;         //4デバイス名Text

    
    private InputDevice[] joinedDevices;                        //参加中のデバイス
    private int currentCount = 0;                               //現在の参加数




    private void Awake()
    {
        //最大参加可能数で配列を初期化
        joinedDevices = new InputDevice[maxPlayers];
        // InputActionを有効化し、コールバックを設定
        joinAction.Enable();
        joinAction.performed += OnJoin;

        leaveAction.Enable();
        leaveAction.performed += OnLeave;

        //-----Text非表示-----
        device1text.enabled = false;
        device2text.enabled = false;
        device3text.enabled = false;
        device4text.enabled = false;
    }


    private void OnDestroy()
    {
        joinAction.performed -= OnJoin;
        joinAction.Disable();

        leaveAction.performed -= OnLeave;
        leaveAction.Disable();
    }

    private void OnJoin(InputAction.CallbackContext context)
    {
        //現在の参加数がＭａｘならreturn
        if (currentCount >= maxPlayers) { return; }

        //押されたデバイスを取得
        var device = context.control.device;
        //Debug.Log(device);
        //参加中のデバイスの中に今押したデバイスがある場合return(重複防止)
        foreach (var d in joinedDevices)
        {
            if (d == device) { return; }
        }

        //現在の参加数にデバイスを追加その後カウントを増やす
        joinedDevices[currentCount] = device;
        currentCount++;
       UpdateDeviceTexts();
    }

    void OnLeave(InputAction.CallbackContext context)
    {
        var device = context.control.device;
        Debug.Log("退出");
        int index = -1;
        for (int i = 0;i < currentCount; i++)
        {
            if (joinedDevices[i] == device)
            {
                index = i;
                break;
            }
        }
        if(index == -1)return;

        for(int i = index; i < currentCount - 1; i++)
        {
            joinedDevices[i] = joinedDevices[i + 1];
        }

        joinedDevices[currentCount - 1] = null;
        currentCount--;
        UpdateDeviceTexts();
    }

    void UpdateDeviceTexts()
    {
        Text[] texts = { device1text, device2text, device3text, device4text };

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].enabled = false;
            texts[i].text = "";
        }

        for (int i = 0; i < currentCount; i++)
        {
            texts[i].enabled = true;
            texts[i].text = $"Player {i + 1}: {joinedDevices[i].displayName}";
        }
    }


    //StartButtonが押されたときのScene移行
    public void OnGameStarte()
    {
        PlayerDataHolder.Instance.SetDevices(joinedDevices,currentCount);
        SceneManager.LoadScene("Main");
    }

}
