using System.Linq;
using Unity.Entities;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGameManger : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab = default; //Player
    [SerializeField] private GameObject botPrefab = default;

    [SerializeField] private Transform[] pos = default;         //生成位置

    private GameObject joinobj;

    [SerializeField] private Button button;

    [SerializeField] private string[] connectedDevices;



    void Start()
    {
        joinobj = GameObject.Find("JoinedManager");
        //インスタンスがない場合はreturn
        if(PlayerDataHolder.Instance == null) { return; }

        //インスタンスで保持しているデバイス情報と人数を取得
        var devices = PlayerDataHolder.Instance.GetDevices();
        int count   = PlayerDataHolder.Instance.GetPlayerCount();

        connectedDevices = new string[devices.Count()];

        for (int i = 0; i < devices.Count(); i++)
        {
            if (devices[i] != null)
            {
                connectedDevices[i] =  $"[{i}] {devices[i].displayName} ({devices[i].layout})";
            }
            else
            {
                connectedDevices[i] = $"[{i}] None";
            }
        }
        //------------



        //人数分Playerの生成,PlayerID
        for (int i = 0; i < pos.Length; i++)
        {
            if(i <  count && devices[i] != null)
            {
                // 指定デバイスで PlayerInput を持つプレイヤーを生成
                var obj = PlayerInput.Instantiate(
                    prefab: playerPrefab,
                    playerIndex: i,
                    pairWithDevice: devices[i]
                 );
                //生成後この位置にセット
                obj.transform.position = pos[i].position;
                obj.transform.rotation = pos[i].rotation;

            }
            else
            {
                Instantiate(botPrefab, pos[i].position, pos[i].rotation);
            }
        }
        button.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnReset()
    {
        SceneManager.LoadScene("Start");
        Destroy( joinobj );   
    }
}
