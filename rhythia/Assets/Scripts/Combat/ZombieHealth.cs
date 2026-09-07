using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public int health = 1;
    private bool isDead = false;

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        health -= amount;
        if (health <= 0)
        {
            isDead = true;
            Destroy(gameObject);
        }
    }
}
