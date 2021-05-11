using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform[] locations;
    public float waveNum;
    public struct Wave
    {
        public float vineSpider;
        public float entling;
        public float ent;
        public float brainCase;
        public float mechElf;
        public float teslaSpider;
        public float tank;
        public float geraldsDrone;
        public float gerald;
    }
}
