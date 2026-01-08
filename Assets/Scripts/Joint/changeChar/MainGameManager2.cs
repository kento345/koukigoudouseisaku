using System.Linq;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGameManager2 : MonoBehaviour
{
    [SerializeField] private GameObject character1 = default;
    [SerializeField] private GameObject character2 = default;
    [SerializeField] private GameObject character3 = default;

    private PlayerInfo[] playerInfos = default;

    [SerializeField] private GameObject joinobj;

    private void Start()
    {
        joinobj = GameObject.Find("JoinedManager");
    }

    public void SetInformation(PlayerInfo[] playerInfos)
    {
        this.playerInfos = playerInfos;
        // 例えばここで生成
        CreateCharacter();
    }

    // 改めてインゲームでキャラクターを生成
    private void CreateCharacter()
    {
        for (int i = 0; i < playerInfos.Length; i++)
        {
            GameObject character = default;

            switch (playerInfos[i].SelectedCharacter)
            {
                case CharacterType.Character1:
                    character = character1;
                    break;

                case CharacterType.Character2:
                    character = character2;
                    break;

                case CharacterType.Character3:
                    character = character3;
                    break;
            }

            PlayerInput.Instantiate(
            prefab: character,
            playerIndex: i,
            pairWithDevice: playerInfos[i].PairWithDevice
            );
        }
    }



  /*  [SerializeField] private GameObject playerPrefab = default; //Player
    [SerializeField] private GameObject botPrefab = default;

    [SerializeField] private Transform[] pos = default;         //生成位置

    [SerializeField] private GameObject joinobj;

    [SerializeField] private Button button;

    [SerializeField] private string[] connectedDevices;



    void Start()
    {
        joinobj = GameObject.Find("JoinedManager");
        //インスタンスがない場合はreturn
        if (PlayerDataHolder.Instance == null) { return; }

        //インスタンスで保持しているデバイス情報と人数を取得
        var devices = PlayerDataHolder.Instance.GetDevices();
        int count = PlayerDataHolder.Instance.GetPlayerCount();

        connectedDevices = new string[devices.Count()];

        for (int i = 0; i < devices.Count(); i++)
        {
            if (devices[i] != null)
            {
                connectedDevices[i] = $"[{i}] {devices[i].displayName} ({devices[i].layout})";
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
            if (i < count && devices[i] != null)
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

    }*/

    public void OnReset()
    {
        SceneManager.LoadScene("Start");
        Destroy(joinobj);
    }
}
