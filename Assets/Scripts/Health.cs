using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 5, currentHealth;

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        gameObject.SetActive(false);
    }
}
