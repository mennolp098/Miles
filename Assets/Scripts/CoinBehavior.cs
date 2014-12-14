using UnityEngine;
using System.Collections;

public class CoinBehavior : MonoBehaviour {
	GoldController goldScript;
	void Start()
	{
		goldScript = GameObject.FindGameObjectWithTag("Player").GetComponent<GoldController>();
		Invoke("GetGold", Random.Range(2.5f,5f));
	}
	void OnTriggerStay (Collider other) {
		if(other.transform.tag == "Player")
		{
			GetGold();
		}
	}
	private void GetGold()
	{
		if(this.transform != null)
		{
			goldScript.AddGold(1);
			Destroy(this.gameObject);
		}
	}
}
