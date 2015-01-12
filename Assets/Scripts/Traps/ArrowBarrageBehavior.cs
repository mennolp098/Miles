using UnityEngine;
using System.Collections;

public class ArrowBarrageBehavior : MonoBehaviour {
	public GameObject bloodSplatterPrefab;

	private float _shootCooldown = 5;
	private float _timeStamp = 0;
	private float _attackDamage;
	void Start()
	{
		particleSystem.enableEmission = false;
		_attackDamage = 2.5f;
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
			other.GetComponent<EnemyBehavior>().GetDmg(_attackDamage);
			GameObject bloodSplatter = Instantiate(bloodSplatterPrefab,this.transform.position,Quaternion.identity) as GameObject;
			Vector3 newRot = this.transform.eulerAngles;
			newRot.y -= 180;
			bloodSplatter.transform.eulerAngles = newRot;
		}
	}
}
