using UnityEngine;

public class KnockbackReceiver : MonoBehaviour
{
    public bool isInvincible = false;

    public void ReceiveKnockback(Vector3 dir, float force)
    {
        if (isInvincible) return;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            dir.y = 0f;
            rb.AddForce(dir.normalized * force, ForceMode.Impulse);
        }
    }

    public void StartInvincible(float time)
    {
        if (isInvincible) return;

        isInvincible = true;
        Invoke(nameof(EndInvincible), time);
    }

    private void EndInvincible()
    {
        isInvincible = false;
    }
}
