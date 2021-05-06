using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
    public GameObject self;
    public float maxHealth, currentHealth;
    public Slider healthSlid, damageSlid;
    internal virtual void OnEnable()
    {
        currentHealth = maxHealth;
    }
    internal virtual void Update()
    {
        healthSlid.maxValue = maxHealth;
        healthSlid.value = currentHealth;
        damageSlid.maxValue = maxHealth;
    }
    public virtual void TakeDamage(int damage)
    {
        damageSlid.value = currentHealth;
        currentHealth -= damage;
        healthSlid.value = currentHealth;
        if (currentHealth <= 0)
        {
            Die();
            return;
        }
        StartCoroutine(damageSlide());

    }

    public IEnumerator damageSlide()
    {
        float amount = 0.1f;
        float count = 0;
        while (damageSlid.value > healthSlid.value)
        {
            if (count < 0.5) { count += Time.deltaTime; }
            else if (count >= 0.5 && damageSlid.value > healthSlid.value) { damageSlid.value -= amount; amount += 0.1f; }
            yield return null;
        }
        if (damageSlid.value <= healthSlid.value)
        {
            damageSlid.value = healthSlid.value;
            yield break;
        }
    }
    internal virtual void Die()
    {
        Destroy(self);
    }
}
