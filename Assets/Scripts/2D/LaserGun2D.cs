using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class LaserGun2D : LaserGun
{
    internal override void Wepnep() { }
    internal override void Update()
    {
        ammoSlider.maxValue = maxFire;
        ammoSlider.value = maxFire - fireCount;
        Wepnep();

        if (InputSystem.GetDevice<Keyboard>().cKey.isPressed && !cooling)
        {
            if (!alreadyPlaying) { Camera.main.gameObject.GetComponentInParent<AudioManager>().sfx[5].Play(); alreadyPlaying = true; }
            anim.SetBool("Shoot", true);
            laser.gameObject.SetActive(true);
            laser.damage = (int)(damage * player.GetComponent<Stats>().baseDamage * damageMultiplier);
            fireCount += Time.deltaTime;
            if (fireCount >= maxFire)
            {
                cooling = true;
            }
        }
        else
        {
            Camera.main.gameObject.GetComponentInParent<AudioManager>().sfx[5].Stop();
            alreadyPlaying = false;
            laser.hits.Clear();
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
            laser.hits.Clear();
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
