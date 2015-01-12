using UnityEngine;
using System.Collections;

public class MageSpellBehavior : MonoBehaviour {
	public float destroyTime;
	public float speed;
	public GameObject hitExplosionPrefab;

	private float _damage;
	private Transform _target;
	// Use this for initialization
	void Start () 
	{
		Destroy(gameObject, destroyTime);
	}
	public void SetDamage(float dmg)
	{
		_damage = dmg;
	}
	public void SetTarget(Transform trgt)
	{
		_target = trgt;
	}
	void OnTriggerEnter(Collider other) 
	{
		if(other.transform.tag == "Player")
		{
			if(!other.isTrigger)
			{
				other.gameObject.GetComponent<HealthController>().SubtractHealth(_damage);
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
		if(_target != null)
		{
			Vector3 _targetPos = _target.position;
			_targetPos.y += 1f;
			transform.position = Vector3.MoveTowards(transform.position, _targetPos, speed * Time.deltaTime);
		}
	}
}
