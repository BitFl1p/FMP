using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
public class Health : MonoBehaviour
{
    public float maxHealth, currentHealth;
    public Slider healthSlid, damageSlid;
    private void OnEnable()
    {
        currentHealth = maxHealth;
    }
    private void Update()
    {
        healthSlid.maxValue = maxHealth;
        healthSlid.value = currentHealth;
        damageSlid.maxValue = maxHealth;
    }
    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        StartCoroutine(damageSlide());
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public IEnumerator damageSlide()
    {
        float count = 0;
        while(count >= 0.5 && damageSlid.value > healthSlid.value)
        {
            damageSlid.value -= 0.1f;
            yield return null;
        }
        if(damageSlid.value <= healthSlid.value)
        {
            damageSlid.value = healthSlid.value;
            yield break;
        }

    }
    void Die()
    {
        gameObject.SetActive(false);
    }
}
