using UnityEngine;

public class Explode2D : MonoBehaviour
{

    public float power = 8f, radius = 3f, upForce = 1f;
    public void Wee(int damage, Vector3 pos, Vector3 rot)
    {
        transform.position = pos;
        transform.eulerAngles = rot;
        Collider[] nearby = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider thisGuy in nearby)
        {
            Rigidbody rb = thisGuy.GetComponent<Rigidbody>();
            if (rb != null && thisGuy.GetComponent<AutoGun>() == null) if (rb.gameObject.tag == "2D") rb.AddExplosionForce(power, transform.position, radius, upForce, ForceMode.Impulse);

            Health health = thisGuy.GetComponent<Health>();
            if (health != null && thisGuy.GetComponent<AutoGun>() == null) { health.TakeDamage(damage); }
        }

    }
}
