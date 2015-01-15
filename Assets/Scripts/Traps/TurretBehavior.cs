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
	public Transform crossbow;
	public GameObject arrowPrefab;
	void Start () {
		Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 40f);
		for (int i = 0; i < hitColliders.Length; i++) 
		{
			string colliderTag = hitColliders[i].transform.tag;
			if(colliderTag == "Enemy")
			{
				EnemyBehavior enemyScript = hitColliders[i].gameObject.GetComponent<EnemyBehavior>();
				_enemyScripts.Add(enemyScript);
				_enemyScripts.Sort();
			}
		}
	}
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
					Vector3 relativePos = _enemyScripts[0].thisTransform.position - crossbow.position;
					Quaternion enemyLookAt = Quaternion.LookRotation(relativePos);
					//check rotation relative to the pos to slerp towards enemypos
					crossbow.rotation = Quaternion.Slerp(crossbow.rotation, enemyLookAt, Time.deltaTime * rotationSpeed);
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
		//if enemy enters trigger
		if(other.transform.tag == "Enemy")
		{
			//raycast to check if it hits the enemy.
			Vector3 raycastDirection = other.transform.position - transform.position;
			Ray ray = new Ray(transform.position, raycastDirection);
			Debug.DrawRay(transform.position, raycastDirection);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit, 20f))
			{
				if(hit.transform.tag == "Enemy")
				{
					//add enemys in list while they enter the ray hit
					EnemyBehavior enemyScript = other.GetComponent<EnemyBehavior> ();
					_enemyScripts.Add(enemyScript);
					_enemyScripts.Sort();
				}
			}
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
		GameObject newBullet = Instantiate (arrowPrefab, spawnpoint.position, spawnpoint.rotation) as GameObject;
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
