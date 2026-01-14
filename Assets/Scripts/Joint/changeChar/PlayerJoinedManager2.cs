using System;
using Unity.Entities.UniversalDelegates;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerJoinedManager2 : MonoBehaviour
{
    [SerializeField] private InputAction joinAction = default;  //参加するときの入力

    [SerializeField] private GameObject playerPrefab = default;
    [SerializeField] private GameObject[] characters;

    private int curentCharacterIndex = 0;
    //private GameObject curentCharacter;

    private InputDevice[] joinedDevices;                        //参加中のデバイス
    private int currentCount = 0;                               //現在の参加数

    [SerializeField] private int maxPlayers = 4;        //参加上限
    //----------
    [SerializeField] private Text device1text;         //1デバイス名Text
    [SerializeField] private Text device2text;         //2デバイス名Text
    [SerializeField] private Text device3text;         //3デバイス名Text
    [SerializeField] private Text device4text;         //4デバイス名Text

    [SerializeField] private Transform[] pos = default;         //生成位置

  


    private void Awake()
    {
        //最大参加可能数で配列を初期化
        joinedDevices = new InputDevice[maxPlayers];
        // InputActionを有効化し、コールバックを設定
        joinAction.Enable();
        joinAction.performed += OnJoin;

        //-----Text非表示-----
        device1text.enabled = false;
        device2text.enabled = false;
        device3text.enabled = false;
        device4text.enabled = false;
    }

    private void OnDisable()
    {
        joinAction.performed -= OnJoin;
        joinAction.Disable();
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
            if (device == d) { return; }
        }

        if (currentCount >= maxPlayers) return;


         PlayerInput.Instantiate(
            playerPrefab.gameObject,
            pos[currentCount].position,
            Quaternion.identity
         );

            //現在の参加数にデバイスを追加その後カウントを増やす
            joinedDevices[currentCount] = device;
        currentCount++;
        if (currentCount == 1)
        {
            device1text.enabled = true;
            device1text.text += $"Player {currentCount}: {device.displayName}\n";
        }
        if (currentCount == 2)
        {
            device2text.enabled = true;
            device2text.text += $"Player {currentCount}: {device.displayName}\n";
        }
        if (currentCount == 3)
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


    //StartButtonが押されたときのScene移行
    public void OnGameStarte()
    {
        PlayerDataHolder.Instance.SetDevices(joinedDevices, currentCount);
        SceneManager.LoadScene("Main");
    }
}
