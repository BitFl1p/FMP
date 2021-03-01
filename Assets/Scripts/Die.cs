using UnityEngine;

public class Die : MonoBehaviour
{
    public float timer, count;
    private void OnEnable()
    {
        count = timer;
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
