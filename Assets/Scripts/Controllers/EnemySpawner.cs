﻿
using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour 
{
    [SerializeField]
	private Transform allEnemys;
    [SerializeField]
    private GameObject[] _enemys;
    [SerializeField]
    private GameObject[] _bosses;
    private float timeLastSubtracted;
    private float waveTime = 10;
    private int wave;
    private bool spawningWave = false;
    private int waveTillBoss = 5;
    private bool spawnWave;
    private int bossesKilled = 0;
    private int gropes = 3;
    private bool spawning = false;
    private bool doneSpawning = false;
	void Start()
	{
		Invoke ("BeginSpawning", 15f);
	}
	void BeginSpawning()
	{
		spawning = true;
	}
    void Update()
    {
        if (spawning == true)
        {
            if (spawningWave == false && Time.time >= timeLastSubtracted + waveTime)
            {
                if (waveTillBoss > 0)
                {
                    spawningWave = true;
                    waveTillBoss--;
                }
                else
                {
                    Wave(true, 5, 2);
                    waveTillBoss = 5;
                    bossesKilled++;
                    if(bossesKilled == 2)
                    {
                        doneSpawning = true;
                    }
                }
                timeLastSubtracted = Time.time;
            }
            if (spawningWave == true && Time.time >= timeLastSubtracted + 2)
            {

                if (gropes > 0)
                {
                    Wave(false, 3, 0);
                    gropes--;
                }
                else
                {
                    gropes = 3;
                    spawningWave = false;
                }
                timeLastSubtracted = Time.time;
            }
        }
        GameObject[] enemys;
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        if(doneSpawning == true && enemys.Length == 0)
        {
            PlayerPrefs.SetString("Level", "Level02");
            Application.LoadLevel("Traps");
        }
    }
    private void Wave(bool bossWave,int numEnemys,int numBosses)
    {
        
        if(bossWave == true)
        {
            for (int a = 0; a < numBosses; a++)
            {
                GameObject newBoss = Instantiate(_bosses[0], new Vector3(transform.position.x + Random.Range(-15, 15), transform.position.y, transform.position.z), transform.rotation) as GameObject;
                newBoss.transform.parent = allEnemys;
            }
            for(int a =0;a < numEnemys;a++)
            {
                GameObject newEnemy = Instantiate(_enemys[0], new Vector3(transform.position.x + Random.Range(-15, 15), transform.position.y, transform.position.z), transform.rotation) as GameObject;
                newEnemy.transform.parent = allEnemys;
            }
        }
        else
        {
            for(int a =0;a < numEnemys;a++)
            {
                GameObject newEnemy = Instantiate(_enemys[Random.Range(0, _enemys.Length-1)], new Vector3(transform.position.x + Random.Range(-15, 15), transform.position.y, transform.position.z), transform.rotation) as GameObject;
                newEnemy.transform.parent = allEnemys;
            }
        }
    }
}
