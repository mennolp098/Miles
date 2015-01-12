using UnityEngine;
using System.Collections;

public class MageEnemy : GroundEnemy {
	private Transform _attackTarget;
	private float _shootCoolDown;

	public GameObject mageSpellPrefab;
	public Transform spawnpoint;
	public float attackDamage;
	public float shootCooldown;
	// Use this for initialization
	protected override void Start () {
		health = 30f;
		_speed = 0.5f;
		_myGold = 20f;
		sort = 3;
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
				if (Time.time > _shootCoolDown) 
				{
					Shoot ();
				}
			}
		}
	}
	void Shoot() 
	{
		_shootCoolDown = Time.time + shootCooldown;
		audio.Play();
		GameObject newMageSpell = Instantiate (mageSpellPrefab, spawnpoint.position, spawnpoint.rotation) as GameObject;
		newMageSpell.transform.parent = GameObject.FindGameObjectWithTag("MageSpells").transform;
		MageSpellBehavior newMageSpellScript = newMageSpell.GetComponent<MageSpellBehavior>();
		newMageSpellScript.SetDamage(attackDamage);
		newMageSpellScript.SetTarget(_attackTarget);
		childAnims.SetTrigger("shoot");
	}
	protected override void OnTriggerEnter(Collider other)
	{
		base.OnTriggerEnter(other);
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
