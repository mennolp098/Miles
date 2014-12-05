using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
    [SerializeField]
    private GameObject[] _enemys;
    [SerializeField]
    private float _enemyCoolDown;
    [SerializeField]
    private float _waveCoolDown;
    [SerializeField]
    private int _startingEnemys;
    [SerializeField]
    private int _enemyMultiplayer;
    private int _maxEnemys;
    private int _currentEnemys;
    private int _wave;
	// Use this for initialization
	void Start () 
    {
        Invoke("startWave", _waveCoolDown);
        _maxEnemys = _startingEnemys;
	}
	
    void startWave()
    {
        Invoke("spawnEnemy", _enemyCoolDown);
    }
	void spawnEnemy () 
    {
        _currentEnemys++;
		float newHealth;
		int random = Random.Range (0, _enemys.Length);
		GameObject newEnemy = Instantiate(_enemys[random].gameObject,this.transform.position,this.transform.rotation) as GameObject;
		newEnemy.transform.parent = GameObject.FindGameObjectWithTag ("Enemys").transform;
        if (random == 0)
        {
			newHealth = 3 * _wave + 7 - (_wave/4);
			newEnemy.GetComponent<EnemyBehavior>().SetHealth(newHealth);
        }
        if (random == 1)
        {
			newHealth = 2.5f* _wave + 5 - (_wave/4);
			newEnemy.GetComponent<EnemyBehavior>().SetHealth(newHealth);
        }
        if (random == 2)
        {
			newHealth = 5 *_wave + 10 - (_wave/4);
			newEnemy.GetComponent<EnemyBehavior>().SetHealth(newHealth);
        }
        if(_currentEnemys == _maxEnemys)
        {
            if(_wave == 10)
            {
                Application.LoadLevel("win");
            }
            _currentEnemys = 0;
            _wave++;
			Debug.Log(_wave);
            _maxEnemys += _enemyMultiplayer * _wave;
            
            Invoke("startWave", _waveCoolDown);
        }
        else
        {
            Invoke("spawnEnemy", _enemyCoolDown);
        }
        
	}
}
