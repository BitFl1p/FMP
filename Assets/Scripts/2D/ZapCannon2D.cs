using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZapCannon2D : MonoBehaviour
{
    public float critChance;
    public Transform firePoint;
    public bool done;
    public Slider ammoSlider;
    public ParticleSystem[] steam;
    public BigBall2D laser;
    public Animator anim;
    public int wepNum = 4, damage = 5;
    float fireCount;
    bool cooling = false, animPlaying = false;
    public float maxFire, speed;
    bool fired = false;
    void OnEnable()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        ammoSlider.maxValue = maxFire;
        ammoSlider.value = fireCount;
        

        if (Input.GetKey(KeyCode.C) && !cooling && done)
        {
            firePoint.localScale = Vector3.one * (fireCount / maxFire);
            fireCount += Time.deltaTime;

            if (fireCount >= maxFire && !fired)
            {
                StartCoroutine(Camera.main.GetComponent<CameraShake>().Shake(0.5f, 1));
                fired = true;
                BigBall2D ball = Instantiate(laser);
                ball.SetData(damage, critChance, firePoint.rotation, firePoint.forward, speed, firePoint.position);

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

    }
}
