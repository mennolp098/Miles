using UnityEngine;
using System.Collections;

public class ArcherEnemy : GroundEnemy {
	private Transform _attackTarget;
	private bool _bowIsDrawn;

	public GameObject arrowPrefab;
	public Transform spawnpoint;
	public float attackDamage;
	// Use this for initialization
	protected override void Start () {
		_speed = 0.5f;
		_myGold = 20f;
		sort = 3;
		base.Start();
	}
	protected override void Update ()
	{
		base.Update ();
		if(_attackTarget != null && !_dead)
		{

			if(!_bowIsDrawn)
			{
				childAnims.SetTrigger("drawBow");
				Invoke("setDrawBow", 1f);
			}
			_navMesh.speed = 0;
			Vector3 relativePos = _attackTarget.position - this.transform.position;
			Quaternion enemyLookAt = Quaternion.LookRotation(relativePos);
			//check rotation relative to the pos to slerp towards enemypos
			this.transform.rotation = Quaternion.Slerp(this.transform.rotation, enemyLookAt, Time.deltaTime * 25f);
		} 
		else if(!_dead)
		{
			if(_bowIsDrawn)
			{
				childAnims.SetTrigger("withdrawBow");
				Invoke ("setWithdrawBow", 0.5f);
			} else {
				_navMesh.speed = _oldSpeed;
			}
			Quaternion newRot = this.transform.rotation;
			newRot.y += 270;
			this.transform.rotation = newRot;
		}
	}
	void setWithdrawBow()
	{
		_bowIsDrawn = false;
	}
	void setDrawBow()
	{
		_bowIsDrawn = true;
	}
	public void Shoot() 
	{
		audio.Play();
		GameObject newArcherArrow = Instantiate (arrowPrefab, spawnpoint.position, spawnpoint.rotation) as GameObject;
		newArcherArrow.transform.parent = GameObject.FindGameObjectWithTag("ArcherArrows").transform;
		ArcherArrowBehavior newArcherArrowScript = newArcherArrow.GetComponent<ArcherArrowBehavior>();
		newArcherArrowScript.SetDamage(attackDamage);
		childAnims.SetTrigger("shoot");
	}
	protected override void OnTriggerEnter(Collider other)
	{
		base.OnTriggerEnter(other);
		if(other.transform.tag == "Player")
		{
			_attackTarget = other.transform;
			childAnims.SetBool("foundPlayer", true);
		}
	}
	void OnTriggerExit(Collider other)
	{
		if(other.transform.tag == "Player")
		{
			_attackTarget = null;
			childAnims.SetBool("foundPlayer", false);
		}
	} 
}
