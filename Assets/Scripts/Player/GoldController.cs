using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GoldController : MonoBehaviour {
	private float gold;
    [SerializeField]
    private Text money;
	void Start () {
		gold = 50;
	}
	public void AddGold(float gold)
	{
		this.gold += gold;
	}
	public void SubtractGold(float gold)
	{
		this.gold -= gold;
	}
	public void SetGold(float gold)
	{
		this.gold = gold;
	}
	public float GetGold()
	{
		return gold;
	}
	void OnTriggerStay (Collider other) {
		if(other.transform.tag == "GoldCoin")
		{
			GetGoldCoin(other.gameObject);
		}
	}
    void Update()
    {
        money.text = gold.ToString();
    }
	private void GetGoldCoin(GameObject other)
	{
		if(this.transform != null)
		{
			other.transform.position = Vector3.MoveTowards(other.transform.position,this.transform.position, 12 * Time.deltaTime);
			if(Vector3.Distance(other.transform.position,this.transform.position) <= 3)
			{
				AddGold(5);
				Destroy(other.gameObject);
			}
		}
	}
}
