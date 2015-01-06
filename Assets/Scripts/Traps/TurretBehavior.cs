using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TurretBehavior : MonoBehaviour {
	private List<EnemyBehavior> _enemyScripts = new List<EnemyBehavior>();
	private float _shootCoolDown = 0f;
	private float shootCooldown = 1f;
	private float rotationSpeed = 3;
	private float attackDamage = 4f;

	public Transform spawnpoint;
	public Transform cannon;
	public GameObject bulletPrefab;
	// Update is called once per frame
	void Update () {
		CheckTargets();
	}
	private void CheckTargets()
	{
		//check if enemys are in the list to attack
		if(_enemyScripts.Count != 0)
		{
			for(int i = 0; i < _enemyScripts.Count; i++)
			{
				//check first enemy in list
				if(_enemyScripts[0].thisTransform)
				{
					Vector3 relativePos = _enemyScripts[0].thisTransform.position - cannon.position;
					Quaternion enemyLookAt = Quaternion.LookRotation(relativePos);
					//check rotation relative to the pos to slerp towards enemypos
					cannon.rotation = Quaternion.Slerp(cannon.rotation, enemyLookAt, Time.deltaTime * rotationSpeed);
					if (Time.time > _shootCoolDown) 
					{
						Shoot ();
					}
				}
				//if enemy is not onstage remove out of list
				if(!_enemyScripts[i].isOnStage)
				{
					RemoveTarget(_enemyScripts[i]);
				}
			}
		}
	}
	public void RemoveTarget(EnemyBehavior script)
	{
		_enemyScripts.Remove(script);
		_enemyScripts.Sort();
	}
	void OnTriggerEnter(Collider other) 
	{
		//add enemys in list while they enter the trigger
		EnemyBehavior enemyScript = other.GetComponent<EnemyBehavior> ();
		if(other.transform.tag == "Enemy")
		{
			_enemyScripts.Add(enemyScript);
			_enemyScripts.Sort();
		}
	}
	void OnTriggerExit(Collider other) 
	{
		//remove enemys in list while they exit the trigger
		EnemyBehavior enemyScript = other.GetComponent<EnemyBehavior> ();
		if(_enemyScripts.Contains(enemyScript))
		{
			_enemyScripts.Remove(enemyScript);
			_enemyScripts.Sort();
		}
	}
	void Shoot() 
	{
		audio.Play();
		_shootCoolDown = Time.time + shootCooldown;
		GameObject newBullet = Instantiate (bulletPrefab, spawnpoint.position, spawnpoint.rotation) as GameObject;
		newBullet.transform.parent = GameObject.FindGameObjectWithTag("Bullets").transform;
		ArrowBehavior newBulletScript = newBullet.GetComponent<ArrowBehavior>();
		newBulletScript.SetDamage(attackDamage);
		newBulletScript.SetTarget(_enemyScripts[0].thisTransform);

		/*
		animator.SetTrigger("shoot");
		audio.clip = sounds[1];
		audio.Play(); */
	}
}
