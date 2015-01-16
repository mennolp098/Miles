using UnityEngine;
using System.Collections;

public class TrollEnemy : GroundEnemy {
	private Transform _attackTarget;
	private float _attackCooldown;

	public GameObject cloudPrefab;
	public TrollAttack trollAttack;
	public float attackCooldown;
	// Use this for initialization
	protected override void Start () {
		_health = 250f;
		_speed = 0.25f;
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
				if(Vector3.Distance(this.transform.position,_attackTarget.transform.position) <= 4f)
				{
					_speed = 0;
					LookAtTarget();
					if (Time.time > _attackCooldown) 
					{
						Attack ();
					}
				} 
				else 
				{
					_speed = 0;
					LookAtTarget();
					if(Vector3.Dot(this.transform.forward, (_attackTarget.position - this.transform.position).normalized) >= 0.85f)
						this.transform.position = Vector3.MoveTowards(this.transform.position, _attackTarget.position, 3 * Time.deltaTime);
				}
			}
		} 
		else if(!_death)
		{
			_speed = _oldSpeed;
		}
		if(_navMesh != null)
		{
			_navMesh.speed = _speed;
		}
	}
	void LookAtTarget()
	{
		Vector3 relativePos = _attackTarget.position - this.transform.position;
		Quaternion enemyLookAt = Quaternion.LookRotation(relativePos);
		//check rotation relative to the pos to slerp towards enemypos
		this.transform.rotation = Quaternion.Slerp(this.transform.rotation, enemyLookAt, Time.deltaTime * 1f);
	}
	void Attack() 
	{
		_attackCooldown = Time.time + attackCooldown;
		if(!childAnims.GetBool("Attacking"))
		{
			childAnims.SetBool("Attacking", true);
			trollAttack.attacking = true;
			audio.Play();
			Invoke("StopAttacking", 2f);
		}
	}
	private void StopAttacking()
	{
		trollAttack.attacking = false;
		childAnims.SetBool("Attacking", false);
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
	protected override void Die ()
	{
		base.Die ();
		Invoke ("CreateClouds", 2.5f);
		Destroy(this.gameObject, 2.633f);
	}
	private void CreateClouds()
	{
		Instantiate(cloudPrefab,this.transform.position,cloudPrefab.transform.rotation);
	}
}
