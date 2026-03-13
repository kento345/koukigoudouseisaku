using System;
using System.Collections.Generic;
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

                    
    private List<InputDevice> joinDevices = new List<InputDevice>();             //参加中のデバイス

    //カーソル
    [SerializeField] private Canvas uiCanv;                                     //カーソル表示Canvas
    [SerializeField] private GameObject[] cursors;                              //カーソルPrefab
    private List<GameObject> playerCursors = new List<GameObject>();　　　　　　 //生成したカーソル

    private void Awake()
    {
        //最大参加可能数で配列を初期化
        joinDevices = new List<InputDevice>(maxPlayers);
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


    //-----参加-----
    private void OnJoin(InputAction.CallbackContext context)
    {
        //現在の参加数がＭａｘならreturn
        if(joinDevices.Count >= maxPlayers) {return; }

        //押されたデバイスを取得
        var device = context.control.device;
        //重複参加防止
        if (joinDevices.Contains(device)) {return; }

        //参加中の数
        int i = joinDevices.Count;

        //リストにデバイスの追加
        joinDevices.Add(device);
        //カーソルの生成
        var cursor = Instantiate(cursors[i],uiCanv.transform,false);
        //カーソルの保存
        playerCursors.Add(cursor);

        //PlayerDataの更新
        PlayerDataHolder.Instance.SetDevices(joinDevices.ToArray(), joinDevices.Count);

        //UIの更新
        UpdateDeviceTexts();
    }

    //-----退出-----
    void OnLeave(InputAction.CallbackContext context)
    {
        //入力したデバイスの取得
        var device = context.control.device;
        //Index取得
        int index = joinDevices.IndexOf(device);
        //参加していない場合はreturn
        if (index == -1) return;

        //デバイス,カーソルの削除
        joinDevices.RemoveAt(index);
        Destroy(playerCursors[index]);
        //List,PlayerDataの更新
        playerCursors.RemoveAt(index);
        PlayerDataHolder.Instance.SetDevices(joinDevices.ToArray(), joinDevices.Count);

        //UIの更新
        UpdateDeviceTexts();
    }

    //-----UIの更新-----
    void UpdateDeviceTexts()
    {
        Text[] texts = { device1text, device2text, device3text, device4text };

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].enabled = false;
            texts[i].text = "";
        }

        for (int i = 0; i < joinDevices.Count; i++)
        {
            texts[i].enabled = true;
            texts[i].text = $"Player {i + 1}: {joinDevices[i].displayName}";
        }
    }


    //StartButtonが押されたときのScene移行
    public void OnGameStarte()
    {
        PlayerDataHolder.Instance.SetDevices(joinDevices.ToArray(),joinDevices.Count);

        SceneManager.LoadScene("Main");
    }

}
