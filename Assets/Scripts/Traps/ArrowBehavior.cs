using UnityEngine;
using System.Collections;

public class ArrowBehavior : MonoBehaviour {
	public float destroyTime;
	public float speed;

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
		if(!other.isTrigger)
		{
			if(other.transform.tag == "Enemy")
			{
				if(other.transform.name == "FlyingEnemy")
				{
					float amplifiedDamage = _damage * 1.5f;
					other.gameObject.GetComponent<EnemyBehavior>().GetDmg(amplifiedDamage);
				} else 
                {
					other.gameObject.GetComponent<EnemyBehavior>().GetDmg(_damage);
				}
				Destroy(this.gameObject);
			} else 
            {
				Destroy(this.gameObject);
			}
		}
	}
	// Update is called once per frame
	void Update () 
	{
		if(_target != null)
		{
			if(_target.rigidbody != null)
			{
				transform.position = Vector3.MoveTowards(transform.position, _target.position, speed * Time.deltaTime);
			} else 
			{
				transform.Translate(Vector3.forward * speed * Time.deltaTime);
			}
		} else 
		{
			transform.Translate(Vector3.forward * speed * Time.deltaTime);
		}
	}
}
