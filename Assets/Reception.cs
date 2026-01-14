using UnityEngine;

public class Reception : MonoBehaviour
{
    [Header("ƒmƒbƒNƒoƒbƒN,–³“Gİ’è")]
    private float knockbackTime = 0.3f;
    private float knockbackCounter;

    private Vector3 knockbackDir;
    public bool isKnockback = false;

    [SerializeField] private float StunInvincibleTime = 1.0f; //–³“GŠÔ
    private float invincibilityCounter;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
                rb.linearVelocity = Vector3.zero;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isKnockback)
        {
            rb.linearVelocity = knockbackDir;
        }
    }

    public void KnockBack(Vector3 pos,float force)
    {
        if(invincibilityCounter > 0)return;

        isKnockback = true;
        knockbackCounter = knockbackTime;

        knockbackDir = (transform.position - pos).normalized * force;
        rb.linearVelocity = Vector3.zero;

        invincibilityCounter = StunInvincibleTime;
    }

   /* public void DamagePlahyer()
    {
        //–³“G‚¶‚á‚È‚¢‚Æ‚«UŒ‚‚ğó‚¯‚½‚çLayer‚©Tag‚ğ•ÏX‚·‚é
        if (invincibilityCounter <= 0)
        {
            invincibilityCounter = StunInvincibleTime;
        }
    }*/
}
