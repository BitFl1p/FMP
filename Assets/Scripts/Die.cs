using UnityEngine;

public class Die : MonoBehaviour
{
    float count;
    public float timer;
    private void OnEnable()
    {
        count = timer;
    }
    private void Update()
    {
        count -= Time.deltaTime;
        if (count <= 0)
        {
            Destroy(gameObject);
        }
    }
}
