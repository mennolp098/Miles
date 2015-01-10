using UnityEngine;
using System.Collections;

public class MageSpellBehavior : MonoBehaviour {
	public float destroyTime;
	public float speed;
	public GameObject hitExplosionPrefab;

	private float damage;
	private Transform target;
	// Use this for initialization
	void Start () 
	{
		Destroy(gameObject, destroyTime);
	}
	public void SetDamage(float dmg)
	{
		damage = dmg;
	}
	public void SetTarget(Transform trgt)
	{
		target = trgt;
	}
	void OnTriggerEnter(Collider other) 
	{
		if(other.transform.tag == "Player")
		{
			if(!other.isTrigger)
			{
				other.gameObject.GetComponent<HealthController>().SubtractHealth(damage);
				GameObject hitExplosion = Instantiate(hitExplosionPrefab,this.transform.position,this.transform.rotation) as GameObject;
				Vector3 newRot = this.transform.eulerAngles;
				newRot.y -= 180;
				hitExplosion.transform.eulerAngles = newRot;
				Destroy(this.gameObject);
			}
		}
	}
	void Update () 
	{
		if(target != null)
		{
			Vector3 targetPos = target.position;
			targetPos.y += 1f;
			transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
		}
	}
}
