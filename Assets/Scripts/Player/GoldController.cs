using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GoldController : MonoBehaviour {
	private GeneralController _generalController;
	void Start()
	{
		_generalController = GameObject.FindGameObjectWithTag("GeneralController").GetComponent<GeneralController>();
	}
	void OnTriggerStay (Collider other) {
		if(other.transform.tag == "GoldCoin")
		{
			GetGoldCoin(other.gameObject);
		}
	}
	private void GetGoldCoin(GameObject other)
	{
		if(this.transform != null)
		{
			other.transform.position = Vector3.MoveTowards(other.transform.position,this.transform.position, 12 * Time.deltaTime);
			if(Vector3.Distance(other.transform.position,this.transform.position) <= 3)
			{
				_generalController.AddGold(2);
				Destroy(other.gameObject);
			}
		}
	}
}
