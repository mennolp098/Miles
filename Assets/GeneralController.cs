using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GeneralController : MonoBehaviour {
	public static bool isMultiplayer = true;
	private float gold;
	[SerializeField]
	private Text money;
	void Start () {
		gold = 100;
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
	void Update()
	{
		money.text = gold.ToString();
	}
}
