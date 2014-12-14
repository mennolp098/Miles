using UnityEngine;
using System.Collections;

public class GoldController : MonoBehaviour {
	private float gold;
	void Start () {
		gold = 0;
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
}
