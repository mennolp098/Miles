using UnityEngine;
using System.Collections;

public class TrollAttack : MonoBehaviour {
	public bool attacking;

	private float _attackDamage;
	// Use this for initialization
	void Start()
	{
		_attackDamage = 25f;
	}
	void OnTriggerStay(Collider other)
	{
		if(other.transform.tag == "Player" && attacking)
		{
			attacking = false;
			other.GetComponent<HealthController>().SubtractHealth(_attackDamage);
		}
	}
}
