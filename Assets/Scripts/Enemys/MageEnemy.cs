using UnityEngine;
using System.Collections;

public class MageEnemy : GroundEnemy {
	private Transform _attackTarget;
	private float _cooldownTracker;
	private float _attackDamage;
	private float _shootCooldown;

	public GameObject mageSpellPrefab;
	public Transform spawnpoint;

	// Use this for initialization
	protected override void Start () {
		_health = 30f;
		_speed = 0.5f;
		_myGold = 10f;
		_attackDamage = 10f;
		_shootCooldown = 2f;
		sort = 2;
		base.Start();
	}
	protected override void Update ()
	{
		base.Update ();
		if(_attackTarget != null && !_death)
		{
			bool playerIsDeath = _attackTarget.gameObject.GetComponent<PlayerController>().death;
			if(!playerIsDeath)
			{
				Vector3 relativePos = _attackTarget.position - this.transform.position;
				Quaternion enemyLookAt = Quaternion.LookRotation(relativePos);
				//check rotation relative to the pos to slerp towards enemypos
				this.transform.rotation = Quaternion.Slerp(this.transform.rotation, enemyLookAt, Time.deltaTime * 25f);
				if (Time.time > _cooldownTracker) 
				{
					Shoot ();
				}
			}
		}
	}
	void Shoot() 
	{
		_cooldownTracker = Time.time + _shootCooldown;
		audio.Play();
		GameObject newMageSpell = Instantiate (mageSpellPrefab, spawnpoint.position, spawnpoint.rotation) as GameObject;
		newMageSpell.transform.parent = GameObject.FindGameObjectWithTag("MageSpells").transform;
		MageSpellBehavior newMageSpellScript = newMageSpell.GetComponent<MageSpellBehavior>();
		newMageSpellScript.SetDamage(_attackDamage);
		newMageSpellScript.SetTarget(_attackTarget);
		childAnims.SetTrigger("shoot");
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.transform.tag == "Player")
		{
			_attackTarget = other.transform;
		}
	}
	void OnTriggerExit(Collider other)
	{
		if(other.transform.tag == "Player")
		{
			_attackTarget = null;
		}
	}
}
