using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    public GameObject visuals;
    float count = 0;
    float damageCount;
    public bool godMode = false;
    internal override void OnEnable()
    {
        base.OnEnable();
        maxHealth = GetComponent<Stats>().maxHealth;
        currentHealth = maxHealth;
    }
    public override void TakeDamage(int damage)
    {
        if (godMode) { return; }
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
    internal override void Die()
    {
        //GetComponent<CapsuleCollider>().enabled = false;
        //GetComponent<Rigidbody>().isKinematic = true;
        visuals.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
