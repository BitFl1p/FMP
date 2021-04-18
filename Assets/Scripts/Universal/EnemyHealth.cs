using UnityEngine;
public class EnemyHealth : Health
{
    public RectTransform healthCanvas;
    internal override void Update()
    {
        base.Update();
        healthCanvas.LookAt(Camera.main.transform.position);
        if (currentHealth == maxHealth) healthCanvas.gameObject.SetActive(false);
        else healthCanvas.gameObject.SetActive(true);

    }
}
