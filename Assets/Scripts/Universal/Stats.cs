using UnityEngine;
using TMPro;

public class Stats : MonoBehaviour
{
    public float baseRegen;
    public float maxHealth;
    public int baseDamage;
    public float jumpHeight;
    public float moveSpeed;
    public int jumpAmount;
    public float critChance;
    public int Coins2D, Coins3D;

    public TMP_Text CoinText2D, CoinText3D;
    private void Update()
    {
        CoinText2D.text = Coins2D.ToString();
        CoinText3D.text = Coins3D.ToString();
    }
}
