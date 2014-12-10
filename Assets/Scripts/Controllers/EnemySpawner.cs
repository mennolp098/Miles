using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour 
{
    [SerializeField]
    private GameObject[] _enemys;
    private int beginningEnemies;
    private int enrmyMultiplayer;
    private int wave;
    void Start()
    {
        Invoke("spawner",3);
    }
    private void spawner()
    {
        
    }
        

}
