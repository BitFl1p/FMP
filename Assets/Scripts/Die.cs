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
            Deth();
        }
    }
    public void Deth()
    {
        
        transform.localScale -= Vector3.one * 0.1f * transform.localScale.x;
        foreach (Transform child in transform)
        {
            child.localScale -= Vector3.one * 0.1f * child.localScale.x;
        }
        if (transform.localScale.magnitude <= 0.01)
        {
            Destroy(gameObject);
        }

    }
}
