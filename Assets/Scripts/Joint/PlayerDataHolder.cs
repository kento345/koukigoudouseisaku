using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDataHolder : MonoBehaviour
{
    public static PlayerDataHolder Instance { get; private set; } //Playerの接続データ

    private InputDevice[] devices;//Playerデバイス
    private int playerCount;//Playerの接続数

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public void SetDevices(InputDevice[] devis, int cout)
    {
        devices = new InputDevice[cout];
        for (int i = 0; i < cout; i++)
        {
            devices[i] = devis[i];
            playerCount = cout;
        }
    }

    
    public InputDevice[] GetDevices() => devices; //Playerのデバイス取得
    public int GetPlayerCount() => playerCount;//Playerの接続数取得
}