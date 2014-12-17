using UnityEngine;
using System.Collections;

public class ArrowBarrageBehavior : MonoBehaviour {
	private float _shootCooldown = 5;
	private float _timeStamp = 0;
	void Start()
	{
		particleSystem.enableEmission = false;
	}

	void OnTriggerStay(Collider other)
	{
		if(other.transform.tag == "Enemy" && _timeStamp <= Time.time)
		{
			ShootBarrage();
		}
	}
	private void ShootBarrage()
	{
		_timeStamp = Time.time + _shootCooldown;
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
			other.GetComponent<EnemyBehavior>().GetDmg(1);
		}
	}
}
