using UnityEngine;
using System.Collections;

public class ArrowBarrageBehavior : MonoBehaviour {
	private float shootCooldown = 5;
	private float timeStamp = 0;

	void Start()
	{
		particleSystem.enableEmission = false;
	}

	void OnTriggerStay(Collider other)
	{
		if(other.transform.tag == "Enemy" && timeStamp <= Time.time)
		{
			ShootBarrage();
		}
	}
	private void ShootBarrage()
	{
		timeStamp = Time.time + shootCooldown;
		particleSystem.enableEmission = true;
		Invoke("StopBarrage", 1f);
	}
	private void StopBarrage()
	{
		particleSystem.enableEmission = false;
	}
	void OnParticleCollision(GameObject other)
	{
		if(other.transform.tag == "Enemy")
		{
			EnemyBehavior enemyScript = other.GetComponent<EnemyBehavior>();
			enemyScript.GetDmg(1f);
		}
	}
}
