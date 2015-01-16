using UnityEngine;
using System.Collections;

public class ArcherEnemy : GroundEnemy {
	private Transform _attackTarget;
	private bool _bowIsDrawn;
	private float _attackDamage;

	public GameObject archerModel;
	public GameObject arrowPrefab;
	public Transform spawnpoint;

	// Use this for initialization
	protected override void Start () {
		_health = 35f;
		_speed = 0.75f;
		_myGold = 10f;
		_attackDamage = 20f;
		sort = 1;
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
				Vector3 fixedEulerRot = new Vector3(0,180,0);
				Vector3 lerpRotation = Vector3.Lerp(archerModel.transform.localEulerAngles,fixedEulerRot, 0.5f * Time.deltaTime);
				archerModel.transform.localEulerAngles = lerpRotation;
				//draw bow if it isn't drawn
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
		} 
		else if(!_death)
		{
			if(_bowIsDrawn)
			{
				childAnims.SetTrigger("withdrawBow");
				Invoke ("setWithdrawBow", 0.5f);
			} else {
				_navMesh.speed = _oldSpeed;
			}
			//fixed rotation because model is rotated
			Vector3 fixedEulerRot = new Vector3(0,120,0);
			Vector3 lerpRotation = Vector3.Lerp(archerModel.transform.localEulerAngles,fixedEulerRot, 0.5f * Time.deltaTime);
			archerModel.transform.localEulerAngles = lerpRotation;
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
	//shoot function gets triggered by event handler
	public void Shoot() 
	{
		audio.Play();
		GameObject newArcherArrow = Instantiate (arrowPrefab, spawnpoint.position, spawnpoint.rotation) as GameObject;
		newArcherArrow.transform.parent = GameObject.FindGameObjectWithTag("ArcherArrows").transform;
		ArcherArrowBehavior newArcherArrowScript = newArcherArrow.GetComponent<ArcherArrowBehavior>();
		newArcherArrowScript.SetDamage(_attackDamage);
		childAnims.SetTrigger("shoot");
	}
	void OnTriggerEnter(Collider other)
	{
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
