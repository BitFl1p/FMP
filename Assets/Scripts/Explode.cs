using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public ParticleSystem part1;
    public ParticleSystem part2;
    void Start()
    {
        part1.Play();
        part2.Play();
    }
}
