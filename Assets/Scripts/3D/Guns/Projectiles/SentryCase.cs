using UnityEngine;

public class SentryCase : Projectile
{
    public GameObject sentry;
    Animator anim;
    private void OnEnable()
    {
        AutoGun[] sentries = FindObjectsOfType<AutoGun>();
        if (sentries.Length > 2) for (int i = 0; i < sentries.Length; i++) if (i > 2) sentries[i].Explode();
        anim = GetComponent<Animator>();
    }
    internal override void Kill()
    {
        //GetComponent<Rigidbody>().velocity = new Vector3(0, 1, 0);
        GetComponent<Rigidbody>().isKinematic = true;
        //transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        anim.SetBool("Dying", true);
    }

}
