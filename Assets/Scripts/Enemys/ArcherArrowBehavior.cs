using UnityEngine;
using System.Collections;

public class ArcherArrowBehavior : MonoBehaviour {

	public float destroyTime;
	public float speed;
	public GameObject bloodSplatterPrefab;

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
				GameObject bloodSplatter = Instantiate(bloodSplatterPrefab,this.transform.position,Quaternion.identity) as GameObject;
				Vector3 newRot = this.transform.eulerAngles;
				newRot.y -= 180;
				bloodSplatter.transform.eulerAngles = newRot;
				Destroy(this.gameObject);
			}
		}
	}
	void Update () 
	{
		transform.Translate(Vector3.forward * speed);
	}
}
