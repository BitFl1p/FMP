using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryCase : MonoBehaviour
{
    public void SetData(int damage, Quaternion rotation, Vector3 direction, float speed, Vector3 pos)
    {
        this.damage = damage;
        this.rotation = rotation;
        this.direction = direction;
        this.speed = speed;
        this.pos = pos;
        start = true;
    }
    public GameObject sentry;
    public Quaternion rotation;
    public Vector3 direction;
    public float speed;
    public Vector3 pos;
    public Vector3 lastPos;
    bool start;
    Animator anim;
    public int damage;
    private void OnEnable()
    {
        AutoGun[] sentries = FindObjectsOfType<AutoGun>();
        if (sentries.Length > 2)
        {
            for (int i = 0; i < sentries.Length; i++) if (i > 2) sentries[i].Explode();

        }

        anim = GetComponent<Animator>();
    }
    private void LateUpdate()
    {
        AutoGun[] sentries = FindObjectsOfType<AutoGun>();
        if (sentries.Length > 2)
        {
            for (int i = 0; i < sentries.Length; i++) if (i > 2) sentries[i].Explode();

        }
        anim.SetBool("Dying", false);
        if (start)
        {
            speed = speed * 100;
            transform.position = pos;
            GetComponent<Rigidbody>().velocity = direction * speed;

            transform.rotation = rotation;
            start = false;
        }
        
        transform.rotation.SetLookRotation(GetComponent<Rigidbody>().velocity);
        RaycastHit[] hits = Physics.RaycastAll(new Ray(lastPos, (transform.position - lastPos).normalized), (transform.position - lastPos).magnitude);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.isTrigger == false&&hit.collider.gameObject.tag != "Player")
            {
                GetComponent<Rigidbody>().velocity = new Vector3(0,1,0);
                GetComponent<Rigidbody>().isKinematic = true;
                transform.position = new Vector3(transform.position.x, transform.position.y +1, transform.position.z);
                
                anim.SetBool("Dying", true);

                
            }
        }
        lastPos = transform.position;

    }
    
}
