using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerCon : MonoBehaviour
{
    [Header("ノックバック,無敵設定")]
    [SerializeField] private float WeakKnockbackForce = 2.5f; //弱ブリンクノックバック
    [SerializeField] private float StrongKnockbackForce = 5.0f;//強ブリンクノックバック
    private float curentknockbackForce = 0f;//現在のノックバック力

    private float x = 0;


    private float knockbackTime = 5.0f;
    private float knockbackCounter;

    private Vector2 knockbackDir;
    private bool isKnockback = false;

    [SerializeField] private float StunInvincibleTime = 1.0f; //無敵時間
    private float invincibilityCounter;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;
        }
        if (isKnockback)
        {
            knockbackCounter -= Time.deltaTime;
            //rb.linearVelocity = knockbackDir * curentknockbackForce;
            if (knockbackCounter <= 0)
            {
                isKnockback = false;
            }
            return;
            /*else
              {
                  return;
              }*/
        }
    }

    /*  public void KnockBack(Vector3 pos)
      {
          knockbackCounter = knockbackTime;
          isKnockback = true;

          knockbackDir = transform.position - pos;
          knockbackDir.Normalize();
          knockbackDir.y = 0f;
      }*/
    public void KnockBack(Vector3 pos,float force)
    {
        isKnockback = true;
        knockbackCounter = knockbackTime;
        x = force;

        knockbackDir = (transform.position - pos).normalized;

        rb.linearVelocity = Vector3.zero;
        rb.AddForce(knockbackDir * x, ForceMode.Impulse);
    }

    public void DamagePlahyer()
    {
        //無敵じゃないとき攻撃を受けたらLayerかTagを変更する
        if (invincibilityCounter <= 0)
        {


            invincibilityCounter = StunInvincibleTime;
        }
    }
}
