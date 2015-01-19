
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
    private float _timeLastSubtracted;
    private float _waveTime = 10;
    private bool _spawningWave = false;
    private int _waveTillBoss = 5;
    private bool _spawnWave;
    private int _bossesKilled = 0;
    private int _gropes = 3;
    private bool _spawning = true;
	private bool _winning = false;

	void Start()
	{
		Invoke("StartSpawning",15f);
	}
	void StartSpawning()
	{
		_spawning = true;
	}
    void Update()
    {
		if (_spawning == true && _winning == false)
        {
            if (_spawningWave == false && Time.time >= _timeLastSubtracted + _waveTime)
            {
                if (_waveTillBoss > 0)
                {
                    _spawningWave = true;
                    _waveTillBoss--;
                }
                else
                {
                    Wave(true, 5, 2);
                    _waveTillBoss = 5;
                    _bossesKilled++;
                    if(_bossesKilled >= 2)
                    {
                        _winning = true;
                    }
                }
                _timeLastSubtracted = Time.time;
            }
            if (_spawningWave == true && Time.time >= _timeLastSubtracted + 2)
            {

                if (_gropes > 0)
                {
                    Wave(false, 3, 0);
                    _gropes--;
                }
                else
                {
                    _gropes = 3;
                    _spawningWave = false;
                }
                _timeLastSubtracted = Time.time;
            }
        }
        GameObject[] enemys;
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        if(_winning)
        {
			bool allEnemysDeath = true;
			foreach(GameObject enemy in enemys)
			{
				EnemyBehavior enemyScript = enemy.GetComponent<EnemyBehavior>();
				if(enemyScript.isOnStage)
				{
					allEnemysDeath = false;
					break;
				}
			}
			if(allEnemysDeath)
			{
	            PlayerPrefs.SetString("Level", "Level02");
	            Application.LoadLevel("Traps");
			}
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
                GameObject newEnemy = Instantiate(_enemys[Random.Range(0, _enemys.Length)], new Vector3(transform.position.x + Random.Range(-15, 15), transform.position.y, transform.position.z), transform.rotation) as GameObject;
                newEnemy.transform.parent = allEnemys;
            }
        }
    }
}
