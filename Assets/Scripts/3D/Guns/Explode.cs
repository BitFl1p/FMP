using UnityEngine;

public class Explode : MonoBehaviour
{

    public ParticleSystem part1, part2;
    public float power = 8f, radius = 8f, upForce = 1f;
    public virtual void Wee(int damage, Vector3 pos)
    {
        Camera.main.gameObject.GetComponentInParent<AudioManager>().sfx[4].Play();
        transform.position = pos;

        part1.Play();
        part2.Play();

        Collider[] nearby = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider thisGuy in nearby)
        {
            Rigidbody rb = thisGuy.GetComponent<Rigidbody>();
            if (rb != null && thisGuy.GetComponent<AutoGun>() == null) { if (rb.gameObject.tag != "2D") rb.AddExplosionForce(power, transform.position, radius, upForce, ForceMode.Impulse); }
            if (thisGuy.gameObject.GetComponent<ThirdPersonMovement>() != null)
            {
                thisGuy.gameObject.GetComponent<ThirdPersonMovement>().clampDisabled = true;
                thisGuy.gameObject.GetComponent<ThirdPersonMovement>().knockCount = 0.25f;
            }
            else if (thisGuy.gameObject.GetComponent<CharacterController2D>() != null)
            {
                thisGuy.gameObject.GetComponent<CharacterController2D>().clampDisabled = true;
                thisGuy.gameObject.GetComponent<CharacterController2D>().knockCount = 0.25f;
            }
            Health health = thisGuy.GetComponent<Health>();
            if (health != null && thisGuy.GetComponent<AutoGun>() == null) if (thisGuy.gameObject.tag == "Player") thisGuy.GetComponent<PlayerHealth>().TakeDamage((int)Mathf.Ceil(damage * 0.6f)); else health.TakeDamage(damage);
        }

    }
}
