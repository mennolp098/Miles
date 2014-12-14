using UnityEngine;
using System.Collections;

public class ArrowBehavior : MonoBehaviour {
	public float destroyTime;
	public float speed;
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
		if(other.transform.tag == "Enemy")
		{
			other.gameObject.GetComponent<EnemyBehavior>().GetDmg(damage);
			Destroy(this.gameObject);
		} else {
			Destroy(this.gameObject);
		}
	}
	// Update is called once per frame
	void Update () 
	{
		if(target.rigidbody != null)
		{
			transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
		} else {
			transform.Translate(Vector3.forward * speed * Time.deltaTime);
		}
	}
}
