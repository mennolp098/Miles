using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour 
{
    [SerializeField]
    private GameObject[] _enemys;
     [SerializeField]
    private float _groepMultiplayer;
     [SerializeField]
    private int _groepSize;
    private int _wave;
     [SerializeField]
    private int _waveTimer;
    private int _goepsInWave;
     [SerializeField]
    private int _maxGoepsInWave;
     [SerializeField]
    private float _groepTimer;
    void Start()
    {
        Invoke("spawner", _waveTimer);
    }
    private void spawner()
    {
        for (int a = 0; a < _groepSize;a++)
        {
            Instantiate(_enemys[Random.Range(0, -_enemys.Length)],new Vector3(transform.position.x + Random.Range(-15,15),transform.position.y,transform.position.z),transform.rotation);
        }
        _goepsInWave++;
        if (_goepsInWave <= _maxGoepsInWave)
        {
            Invoke("spawner", _groepTimer);
        }
        else
        {
            _wave++;
            _maxGoepsInWave += Mathf.FloorToInt( _groepMultiplayer * _wave);
            _goepsInWave = 0;
            Invoke("spawner", _waveTimer);
        }
    }
        

}
