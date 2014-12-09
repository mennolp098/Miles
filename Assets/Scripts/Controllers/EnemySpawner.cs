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
        Invoke("spawner",5);
    }
    private void spawner()
    {
        for(int a = 0;a < beginningEnemies +(enrmyMultiplayer * wave);a++)
        {
            Debug.Log(beginningEnemies + (enrmyMultiplayer * wave));
        }
    }
        

}
