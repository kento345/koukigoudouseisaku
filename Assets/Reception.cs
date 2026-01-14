using UnityEngine;

public class Reception : MonoBehaviour
{
    [Header("ƒmƒbƒNƒoƒbƒN,–³“GÝ’è")]
    private float knockbackForce = 0;

    private float knockbackTime = 5.0f;
    private float knockbackCounter;

    private Vector2 knockbackDir;
    private bool isKnockback = false;

    [SerializeField] private float StunInvincibleTime = 1.0f; //–³“GŽžŠÔ
    private float invincibilityCounter;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
            /*            else
                        {
                            return;
                        }*/
        }
    }

    /* public void KnockBack(Vector3 pos)
     {
         isKnockback = true;
         knockbackForce = knockbackTime;

         knockbackDir = (transform.position - pos).normalized;

         rb.linearVelocity = Vector3.zero;
         rb.AddForce(knockbackDir * knockbackForce, ForceMode.Impulse);
     }*/
    public void KnockBack(Vector3 pos,float force)
    {
        //ƒmƒbƒNƒoƒbƒNŽžŠÔ•”»’èÝ’è
        knockbackCounter = knockbackTime;
        isKnockback = true;

        //ƒmƒbƒNƒoƒbƒN•ûŒüŽæ“¾•³‹K‰»
        knockbackDir = transform.position - pos;
        knockbackDir.Normalize();
    }

    public void DamagePlahyer()
    {
        //–³“G‚¶‚á‚È‚¢‚Æ‚«UŒ‚‚ðŽó‚¯‚½‚çLayer‚©Tag‚ð•ÏX‚·‚é
        if (invincibilityCounter <= 0)
        {
            invincibilityCounter = StunInvincibleTime;
        }
    }
}
