using UnityEngine;
using System.Collections;

public class ArcherArrowBehavior : MonoBehaviour {

	public float destroyTime;
	public float speed;

	private float _damage;
	// Use this for initialization
	void Start () 
	{
		Destroy(gameObject, destroyTime);
	}
	public void SetDamage(float dmg)
	{
		_damage = dmg;
	}
	void OnTriggerEnter(Collider other) 
	{
		if(other.transform.tag == "Player")
		{
			if(!other.isTrigger)
			{
				other.gameObject.GetComponent<HealthController>().SubtractHealth(_damage);
				Destroy(this.gameObject);
			}
		}
	}
	void Update () 
	{
		transform.Translate(Vector3.forward * speed);
	}
}
