using UnityEngine;
using System.Collections;

public class PoisonTrapBehavior : MonoBehaviour {
    private ParticleSystem poison;
    private float _attackDamage = 3;
    private float _attackSpeed = 4;
    void Start()
    {
        poison = GetComponent<ParticleSystem>();
        poison.Stop();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
            poison.Play();
        }
    }
	void OnParticleCollision (GameObject other) 
    {
	    if(other.transform.tag == "Enemy")
        {
            other.GetComponent<EnemyBehavior>().SetPoison(_attackDamage,_attackSpeed);
        }
	}
}
