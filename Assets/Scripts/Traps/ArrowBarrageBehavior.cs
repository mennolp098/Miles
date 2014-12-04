﻿using UnityEngine;
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
		Debug.Log(other.transform.tag);
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
		EnemyBehavior enemyScript = other.GetComponent<EnemyBehavior>();
	}
}
