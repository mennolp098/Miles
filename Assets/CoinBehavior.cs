using UnityEngine;
using System.Collections;

public class CoinBehavior : MonoBehaviour {
	void OnTriggerEnter (Collider other) {
		if(other.transform.tag == "Player")
		{
			other.GetComponent<GoldController>().AddGold(1);
			Destroy(this.gameObject);
		}
	}
}
