using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health 
{
    float count = 0;
    float damageCount;
    internal override void OnEnable()
    {
        base.OnEnable();
        maxHealth = GetComponent<Stats>().maxHealth;
        currentHealth = maxHealth;
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        damageCount = 0;
    }
    internal override void Update()
    {
        base.Update();
        damageCount += Time.deltaTime;
        maxHealth = GetComponent<Stats>().maxHealth;
        count += Time.deltaTime;
        if (count >= 0.1 && currentHealth < maxHealth && damageCount >= 5)
        {
            currentHealth += GetComponent<Stats>().baseRegen;
            count = 0;
        }
        if (count >= 0.5 && currentHealth < maxHealth && damageCount >= 1)
        {
            currentHealth += GetComponent<Stats>().baseRegen;
            count = 0;
        }
        
        healthSlid.maxValue = maxHealth;
        healthSlid.value = currentHealth;
    }
    

}
