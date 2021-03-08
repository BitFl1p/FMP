﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZapCannon : MonoBehaviour
{
    public Transform firePoint;
    public bool done;
    public Slider ammoSlider;
    public ParticleSystem[] steam;
    public BigBall laser;
    public Animator anim;
    public int wepNum = 4, damage = 5;
    float fireCount;
    bool cooling = false, animPlaying = false;
    public float maxFire, speed;
    
    
    void Update()
    {
        ammoSlider.maxValue = maxFire;
        ammoSlider.value = fireCount;
        anim.SetInteger("WepNum", wepNum);

        if (Input.GetButton("Fire1") && !cooling && done)
        {
            fireCount += Time.deltaTime;
            firePoint.localScale = Vector3.one * fireCount;
            
            if (fireCount >= maxFire)
            {
                BigBall ball = Instantiate(laser);
                ball.SetData(damage, firePoint.rotation, firePoint.forward, speed, firePoint.position);
                anim.SetBool("Shoot", true);
                fireCount += Time.deltaTime;
                cooling = true;
                done = false;
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
