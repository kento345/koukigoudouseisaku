using System.Diagnostics.Contracts;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
//using static System.Net.Mime.MediaTypeNames;

public class PlayerController : MonoBehaviour
{
    [Header("移動設定")]
   
    [SerializeField] private float speed = 5.0f; //移動スピード
    [SerializeField] private float ChargeMoveSpeedRate = 0.3f; //チャージ・硬直中の速度倍率
    private float speed2 = 0; //チャージ中のスピード
    private float curentSpeed = 0;  //現在のスピード
    [SerializeField] private float rotSpeed = 10.0f; //旋回スピード
    [SerializeField] private float ChargeRotateSpeedRate = 0.7f; //チャージ・硬直中の旋回倍率
    private float rotSpeed2 = 0;　//チャージ中旋回スピード
    private float curentRotSpeed = 0;//現在の旋回スピード

    private Vector2 inputVer;


    [Header("パンチ設定")]

    [SerializeField] private BoxCollider box;
    //[SerializeField] private float Power = 10.0f;
    [SerializeField] private float WeakKnockbackForce = 0.5f; //弱パンチノックバック
    [SerializeField] private float StrongKnockbackForce = 5.0f;//強パンチノックバック
    private float curentknockbackForce = 0f;//現在のノックバック力
    //private float tackleCooldown = 1.0f;//タックルのクールダウン時間
    [SerializeField] private float HitDuration = 0.2f; //攻撃判定の持続時間
    [SerializeField] private float wait = 0.25f;

    [SerializeField] private float invincibleTime = 1.0f; //無敵
    private bool isInvincible = false;

    [SerializeField] private float StrongRecoveryTime = 1.0f; //硬直時間
    private float curentRecoveryTime;
    private bool isfinish = false;


    private Rigidbody rb;

    [HideInInspector] public bool isPrese = false; //押されているかフラグ
    private bool isMax = false;//チャージがMaxかのフラグ



    private int playerID;
    private PlayerInput playerInput;
    [SerializeField] private Text IDtext;

    private float y = -5.0f;


    private void Awake()
    {
        speed2 = speed * ChargeMoveSpeedRate;
        rotSpeed2 = rotSpeed * ChargeRotateSpeedRate;
        curentRecoveryTime = StrongRecoveryTime;
    }
    

    public void OnMove(InputAction.CallbackContext context)
    {
        inputVer = context.ReadValue<Vector2>();
    }
    
    public void OnTackle(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isfinish = false;
            isPrese = true;
        }
        if (context.canceled )
        {
            isPrese = false ;
            Invoke("Panti", wait);
        }
    }

    public void SetCharge(bool a)
    {
        isMax = a;
    }

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            playerID = playerInput.playerIndex;
        }
        
        IDtext.text += $"Player {playerID + 1}\n";
       
        rb = GetComponent<Rigidbody>();
       
        box.enabled = false;
    }

    void FixedUpdate()
    {
            Move();


        if (isfinish)
        {
            if (curentRecoveryTime > 0)
            {
                curentRecoveryTime -= Time.deltaTime;
            }
            if (curentRecoveryTime <= 0)
            {
                isfinish = false;
                curentRecoveryTime = StrongRecoveryTime;
            }
        }

    }

    void Move()
    {
        if (isfinish) { return; }
        if (isPrese)
        {
            curentSpeed = speed2;
            curentRotSpeed = rotSpeed2;
        }
        if(!isPrese)
        {
            curentSpeed = speed;
            curentRotSpeed = rotSpeed;
        }
        Vector3 move = new Vector3(inputVer.x, 0f, inputVer.y) * curentSpeed * Time.deltaTime;
        //rb.MovePosition(rb.position + move);
        transform.position += move;

        if (move != Vector3.zero)
        {
            Quaternion Rot = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation,Rot,curentRotSpeed * Time.deltaTime);
            //rb.MoveRotation(Quaternion.Slerp(rb.rotation, Rot, curentRotSpeed * Time.fixedDeltaTime));
        }
    }

    void Panti()
    {
        if (isfinish) { return; }

        box.enabled = true;

        Invoke("EndTackle", HitDuration);

    }

   

    void EndTackle()
    {

        //ここで硬直処理
        if (isMax)
        {
            isfinish = true;
        }
        box.enabled = false;
        isMax = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody enemyrb = other.gameObject.GetComponent<Rigidbody>();
        if (enemyrb != null)
        {
            if (isMax)
            {
                curentknockbackForce = StrongKnockbackForce;
            }
            else
            {
                curentknockbackForce = WeakKnockbackForce;
            }

            Vector3 knockBackDir = other.transform.position - transform.position;
            knockBackDir.y = 0.0f; // 垂直ノックバックを付けたくない場合
            //Debug.Log(curentknockbackForce);
            enemyrb.AddForce(knockBackDir.normalized * curentknockbackForce, ForceMode.Impulse);
        }

        AtackhitBox hitbox = other.GetComponent<AtackhitBox>();
        if (hitbox != null)
        {
            if (isInvincible) return;

            StartInvincible();

            PlayerController attacker = hitbox.owner;

            //Debug.Log("攻撃を受けた！ " + attacker.name);
        }
    }
    void StartInvincible()
    {
        if (isInvincible) return;

        isInvincible = true;
        Invoke("EndInvincible", invincibleTime);
    }

    void EndInvincible()
    {
        isInvincible = false;
    }
}
