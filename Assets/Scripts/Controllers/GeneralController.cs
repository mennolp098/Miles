using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GeneralController : MonoBehaviour {
	public GameObject RespawnCanvasSingle;
	public GameObject RespawnCanvas01;
	public GameObject RespawnCanvas02;
	public GameObject TwoPlayerCanvas;
	public GameObject OnePlayerCanvas;
	public GameObject PlayerSingle;
	public GameObject PlayerOne;
	public GameObject PlayerTwo;

	private float gold;
	private Text money;
	void Start () {
		gold = 100;
		if(PlayerPrefs.GetInt("multiplayer") == 1)
		{
			GameObject respawnCanvas01 = Instantiate(RespawnCanvas01,RespawnCanvas01.transform.position,RespawnCanvas01.transform.rotation) as GameObject;
			GameObject respawnCanvas02 = Instantiate(RespawnCanvas02,RespawnCanvas02.transform.position,RespawnCanvas02.transform.rotation) as GameObject;
			Instantiate(TwoPlayerCanvas,TwoPlayerCanvas.transform.position,TwoPlayerCanvas.transform.rotation);
			GameObject playerOne = Instantiate(PlayerOne,PlayerOne.transform.localPosition,PlayerOne.transform.rotation) as GameObject;
			playerOne.transform.parent = GameObject.FindGameObjectWithTag("Entitys").transform;
			GameObject playerTwo = Instantiate(PlayerTwo,PlayerTwo.transform.localPosition,PlayerTwo.transform.rotation) as GameObject;
			playerTwo.transform.parent = GameObject.FindGameObjectWithTag("Entitys").transform;
			playerOne.GetComponent<HealthController>().AddRespawnCanvas(respawnCanvas01);
			playerTwo.GetComponent<HealthController>().AddRespawnCanvas(respawnCanvas02);
		} else {
			GameObject respawnCanvas01 = Instantiate(RespawnCanvasSingle,RespawnCanvasSingle.transform.position,RespawnCanvasSingle.transform.rotation) as GameObject;
			Instantiate(OnePlayerCanvas,OnePlayerCanvas.transform.position,OnePlayerCanvas.transform.rotation);
			GameObject playerOne = Instantiate(PlayerSingle,PlayerSingle.transform.position,PlayerSingle.transform.rotation) as GameObject;
			playerOne.transform.parent = GameObject.FindGameObjectWithTag("Entitys").transform;
			playerOne.GetComponent<HealthController>().AddRespawnCanvas(respawnCanvas01);
		}
		money = GameObject.FindGameObjectWithTag("MoneyText").GetComponent<Text>();
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
