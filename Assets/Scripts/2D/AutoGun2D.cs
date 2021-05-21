using UnityEngine;

public class AutoGun2D : MonoBehaviour
{
    public float critChance;
    public string axis;
    public Vector3 leftVector, rightVector;
    public bool enemyLeft, enemyRight;
    public Transform head;
    public Bullet2D bullet;
    public float speed;
    public Transform firePoint;
    public bool done = true;
    public int damage = 1;
    public Animator anim;
    public Explode2D explosion;
    bool detected;
    private void Awake()
    {
        OnEnable();
    }
    private void Start()
    {
        OnEnable();
    }
    private void OnEnable()
    {

        AutoGun2D[] sentries = FindObjectsOfType<AutoGun2D>();
        if (sentries.Length > 2) for (int i = 0; i < sentries.Length; i++) if (i > 2) sentries[i].Explode();

        if (axis == "XY")
        {
            transform.eulerAngles = new Vector3(0, 90, 0);
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }
        else
        {
            transform.eulerAngles = Vector3.zero;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }


    }
    private void Update()
    {
        if (done) anim.SetBool("Shoot", false); //anim.enabled = false;
        if (enemyLeft && done)
        {
            head.localEulerAngles = leftVector;
            anim.SetBool("Shoot", false);
            done = false;
            Shoot();
        }
        else if (enemyRight && done)
        {
            head.localEulerAngles = rightVector;
            anim.SetBool("Shoot", false);
            done = false;
            Shoot();
        }
    }
    void Shoot()
    {

        Instantiate(bullet).SetData(damage, critChance, firePoint.rotation, firePoint.forward, speed, firePoint.position, axis);
        anim.SetBool("Shoot", true);

    }


    public void Explode()
    {
        if (axis == "XY") Instantiate(explosion).Wee(damage, transform.position, new Vector3(0, 0, 0));
        else Instantiate(explosion).Wee(damage, transform.position, new Vector3(0, 90, 0));
        Destroy(gameObject);
    }
}
