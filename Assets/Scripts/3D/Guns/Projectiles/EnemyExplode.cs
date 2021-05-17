using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplode : Explode
{
    public override void Wee(int damage, Vector3 pos)
    {
        Camera.main.gameObject.GetComponentInParent<AudioManager>().sfx[4].Play();
        transform.position = pos;

        if(part1 != null) part1.Play();
        if (part1 != null) part2.Play();

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
            if (health != null && thisGuy.GetComponent<AutoGun>() == null) if (!(thisGuy.gameObject.tag == "Enemy")) health.TakeDamage(damage);
        }

    }
}
