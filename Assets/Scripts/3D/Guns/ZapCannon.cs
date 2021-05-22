using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ZapCannon : GunBase
{
    public float critChance;
    public GameObject player;
    public Transform firePoint;
    public Slider ammoSlider;
    public ParticleSystem[] steam;
    public BigBall laser;
    public Animator anim;
    float fireCount;
    bool cooling = false, animPlaying = false;
    public float maxFire, speed;
    bool fired = false;


    void Update()
    {
        ammoSlider.maxValue = maxFire;
        ammoSlider.value = fireCount;
        anim.SetInteger("WepNum", wepNum);
        firePoint.localScale = Vector3.one * 8f * (fireCount / maxFire);

        if (InputSystem.GetDevice<Mouse>().leftButton.isPressed && !cooling && done)
        {
            fireCount += Time.deltaTime;

            if (fireCount >= maxFire && !fired)
            {
                StartCoroutine(Camera.main.GetComponent<CameraShake>().Shake(0.5f, 1));
                fired = true;
                BigBall ball = Instantiate(laser);
                ball.SetData((int)(damage * player.GetComponent<Stats>().baseDamage * damageMultiplier), player.GetComponent<Stats>().critChance, firePoint.rotation, firePoint.forward, speed, firePoint.position);

                anim.SetBool("Shoot", true);
                fireCount -= Time.deltaTime;
                cooling = true;
                done = false;
            }

        }
        else if (done)
        {
            firePoint.localScale = Vector3.one * (fireCount / maxFire);
            fireCount -= Time.deltaTime;
            anim.SetBool("Shoot", false);
            if (fireCount <= 0)
            {
                fireCount = 0;
                cooling = false;
            }
            fired = false;
        }
        if (cooling)
        {
            firePoint.localScale = Vector3.zero;
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
        /*else if(done)
        {
            fireCount -= Time.deltaTime;
            anim.SetBool("Shoot", false);
            if (fireCount <= 0)
            {
                fireCount = 0;
                cooling = false;
            }
            fired = false;
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
        }*/

    }
}
