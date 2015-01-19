using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GeneralController : MonoBehaviour {
	public static bool isMultiplayer = true;
	public GameObject RespawnCanvasSingle;
	public GameObject RespawnCanvas01;
	public GameObject RespawnCanvas02;

	private float gold;
	private Text money;
	void Start () {
		gold = 100;
		if(isMultiplayer)
		{
			Instantiate(RespawnCanvas01,RespawnCanvas01.transform.position,RespawnCanvas01.transform.rotation);
			Instantiate(RespawnCanvas02,RespawnCanvas02.transform.position,RespawnCanvas02.transform.rotation);
		} else {
			Instantiate(RespawnCanvasSingle,RespawnCanvasSingle.transform.position,RespawnCanvasSingle.transform.rotation);
		}
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
