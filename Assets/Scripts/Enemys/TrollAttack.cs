using UnityEngine;
using System.Collections;

public class TrollAttack : MonoBehaviour {
	public bool attacking;
	// Use this for initialization
	void OnTriggerStay(Collider other)
	{
		if(other.transform.tag == "Player" && attacking)
		{
			attacking = false;
			Debug.Log("Troll hits you!");
			other.GetComponent<HealthController>().SubtractHealth(5);
		}
	}
}
