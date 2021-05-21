using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    public GameObject[] enemyPrefabs3D;
    public GameObject[] enemyPrefabs2D;
    public List<Transform> locations3D;
    public List<Transform> locations2D;
    public List<GameObject> enemiesInScene = new List<GameObject> { };
    public int waveNum = 0;
    float count;
    public float spawnInterval;
    bool waveDone = false;
    public List<int> spawnables = new List<int> { };
    public Wave[] waves = new Wave[10];
    public Material endSkybox;
    public GameObject map;
    public GameObject arena;
    public GameObject vcam;
    public GameObject player;
    public bool start = true;
    bool waveSwitch = false;
    float waveTimer;
    public float waveDelay;
    public List<int[]> he;
    bool geraldSpawned;
    [System.Serializable] public class Wave { public int[] values = new int[8]; }
    private void Awake()
    {
        count = spawnInterval;
        waveTimer = waveDelay;
    }
    private void OnLevelWasLoaded(int level)
    {
        Awake();
    }
    private void FixedUpdate()
    {
        if (waveNum > waves.Length) return;
        
        count -= Time.fixedDeltaTime;
        if (count <= 0)
        {
            SpawnEnemy();
            count = spawnInterval;
        }
        
        if (waveDone)
        {
            if (waveSwitch)
            {
                waveNum++;
                waveDone = false;
                enemiesInScene.Clear();
                waveSwitch = false;
                waveTimer = waveDelay;
                start = true;
                

            }
            else
            {
                waveTimer -= Time.deltaTime;
                if(waveTimer <= 0)
                {
                    waveSwitch = true;
                }
            }
        }
        if (start)
        {
            switch (waveNum)
            {
                case 0:
                case 3:
                case 6:
                    if (start)
                    {
                        FindObjectOfType<AudioManager>().SwitchTrack(new List<int> { 4, 5, 6, 9 });
                    }
                    break;
                case 2:
                    if (start)
                    {
                        FindObjectOfType<AudioManager>().SwitchTrack(7);
                    }
                    break;
                case 5:
                    if (start)
                    {
                        FindObjectOfType<AudioManager>().SwitchTrack(2);
                    }
                    break;
                case 8:
                    if (start)
                    {
                        FindObjectOfType<AudioManager>().SwitchTrack(9);
                    }
                    break;
                case 9:
                    if (start)
                    {
                        geraldSpawned = false;
                        SpawnEnemy();
                    }
                    break;
                    
            }
            start = false;

        }


    }

    private void SpawnEnemy()
    {
        waveDone = true;
        spawnables.Clear();
        if(waveNum == 10)
        {
            if (!geraldSpawned)
            {
                FindObjectOfType<AudioManager>().SwitchTrack(8);
                start = false;
                map.SetActive(false);
                arena.SetActive(true);
                vcam.SetActive(true);
                RenderSettings.skybox = endSkybox;
                player.gameObject.SetActive(true);
                player.transform.position = new Vector3(37, 40, 497);
                player.GetComponent<Rigidbody>().velocity = Vector3.zero;
                geraldSpawned = true; 
                GameObject enemyToSpawn = enemyPrefabs3D[enemyPrefabs3D.Length - 1];
                //waves[waveNum].values[waves[waveNum].values.Length -1]--;
                GameObject instance = Instantiate(enemyToSpawn);
                instance.transform.position = new Vector3(230, 130, 750);
                enemiesInScene.Add(instance);
                waveDone = false;
                return;
            }
            else
            {
                return;
            }
            
        }
        if(Random.Range(0, 4) != 1)
        {
            for (int i = 0; i < waves[waveNum].values.Length; i++)
            {
                if (waves[waveNum].values[i] > 0)
                {
                    spawnables.Add(i);
                    waveDone = false;
                }
            }
            if (waveDone)
            {
                foreach(GameObject enemy in enemiesInScene)
                {
                    if (enemy != null) waveDone = false;
                }
                if (waveDone) start = true;
                return;
            }
            else
            {
                int indexToSpawn = Random.Range(0, spawnables.Count);
                GameObject enemyToSpawn = enemyPrefabs3D[spawnables[indexToSpawn]];
                waves[waveNum].values[spawnables[indexToSpawn]]--;
                GameObject instance = Instantiate(enemyToSpawn);
                instance.transform.position = locations3D[Random.Range(0, locations3D.Count)].position;
                enemiesInScene.Add(instance);
                return;
            }
            
        }
        else
        {
            for (int i = 0; i < waves[waveNum].values.Length; i++)
            {
                if (waves[waveNum].values[i] > 0)
                {
                    spawnables.Add(i);
                    waveDone = false;
                }
            }
            if (waveDone)
            {
                foreach (GameObject enemy in enemiesInScene)
                {
                    if (enemy != null) waveDone = false;
                }
                if(waveDone) start = true;
                return;
            }
            else
            {
                int indexToSpawn = Random.Range(0, spawnables.Count);
                GameObject enemyToSpawn = enemyPrefabs2D[spawnables[indexToSpawn]];
                waves[waveNum].values[spawnables[indexToSpawn]]--;
                GameObject instance = Instantiate(enemyToSpawn);
                Transform spawnLoc = locations2D[Random.Range(0, locations2D.Count-1)];
                instance.transform.position = spawnLoc.position;
                instance.GetComponent<EnemyAI2D>().axis = spawnLoc.GetComponent<AxisHolder>().axis;
                instance.GetComponent<EnemyAI2D>().flip = spawnLoc.GetComponent<AxisHolder>().flip;
                enemiesInScene.Add(instance);
                return;
            }
        }
        
    }
}
