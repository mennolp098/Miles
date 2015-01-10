
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
    private float waveTime = 3;
    private int wave;
    private bool spawningWave = false;
    private int waveTillBoss = 5;
    private bool spawnWave;
    private int bossesKilled = 1;
    private int gropes = 3;
    void Update()
    {
        if (spawningWave == false && Time.time >= timeLastSubtracted + waveTime)
        {
            if(waveTillBoss>0)
            {
                spawningWave = true;
                waveTillBoss--;
            }
            else
            {
                Wave(true, 5, 2);
                waveTillBoss = 5;
                bossesKilled++;
            }
            timeLastSubtracted = Time.time;
        }
        if (spawningWave == true && Time.time >= timeLastSubtracted + 2)
        {

            if(gropes>0)
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
    private void Wave(bool bossWave,int numEnemys,int numBosses)
    {
        
        if(bossWave == true)
        {
            for (int a = 0; a < numBosses; a++)
            {
                Debug.Log("boss" + a);
                GameObject newBoss = Instantiate(_bosses[0], new Vector3(transform.position.x + Random.Range(-15, 15), transform.position.y, transform.position.z), transform.rotation) as GameObject;
                newBoss.transform.parent = allEnemys;
            }
            for(int a =0;a < numEnemys;a++)
            {
                Debug.Log("enemy"+ a);
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
