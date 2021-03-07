using UnityEngine;
using UnityEngine.UI;

public class LaserGun : MonoBehaviour
{
    public Slider ammoSlider;
    public ParticleSystem[] steam;
    public Laser laser;
    public Animator anim;
    public int wepNum = 4, damage = 5;
    float fireCount;
    bool cooling = false, animPlaying = false;
    public float maxFire;
    void Update()
    {
        ammoSlider.maxValue = maxFire;
        ammoSlider.value = fireCount;
        anim.SetInteger("WepNum", wepNum);
        
        if (Input.GetButton("Fire1")&&!cooling)
        {
            anim.SetBool("Shoot", true);
            laser.gameObject.SetActive(true);
            laser.damage = damage;
            fireCount += Time.deltaTime;
            if (fireCount >= maxFire)
            {
                cooling = true;
            }
        }
        else
        {
            fireCount -= Time.deltaTime;
            anim.SetBool("Shoot", false);
            laser.gameObject.SetActive(false);
            if (fireCount <= 0)
            {
                fireCount = 0;
                cooling = false;
            }
        }
        if (cooling)
        {
            if (!animPlaying)
            {
                foreach (ParticleSystem current in steam) { current.Play(); }
                animPlaying = true;
            }
            fireCount -= Time.deltaTime;
            if (fireCount <= 0)
            {
                animPlaying = false;
                cooling = false;
            }
        }
        
    }
}
