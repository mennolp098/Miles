using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthController : MonoBehaviour {
	private float _health;
	private bool _invincible;
	private GameObject spawnPoint;
	private Text respawnText;
	private GameObject respawnCanvas;

	public AudioClip deathSound;
	public GameObject sphere;
	// Use this for initialization
	void Start () {
		_health = 100;
		spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
	}
	public void AddRespawnCanvas(GameObject canvas)
	{
		canvas.SetActive(true);
		respawnCanvas = canvas;
		respawnText = canvas.GetComponentInChildren<Text>();
		canvas.SetActive(false);
	}
	public void AddHealth(float health)
	{
		_health += health;
	}
	public void SubtractHealth(float health)
	{
		if(!_invincible && !GetComponent<PlayerController>().death)
		{
			_health -= health;
			if(GetComponent<PlayerTwo>() == null)
			{
				GameObject.FindGameObjectWithTag("UI").GetComponent<UIScript>().UpdateHealthBar(_health);
			} else {
				GameObject.FindGameObjectWithTag("UI").GetComponent<UIScript>().UpdateHealthBarTwo(_health);
			}
			if(_health <= 0)
			{
				Die();
			}
		}
	}
	public float GetHealth()
	{
		return _health;
	}
	private void Die()
	{
		audio.clip = deathSound;
		audio.Play();
		GetComponent<Animator>().SetTrigger("death");
		GetComponent<PlayerController>().death = true;
		GetComponent<PlayerController>().StopFire();
		GetComponent<TrapBuilder>().ClearTrap();
		Invoke("DisableModel", 0.80f);
		respawnCanvas.SetActive(true);
		StartCoroutine("RespawnCounterCouritine");
	}
	private void DisableModel()
	{
		sphere.SetActive(false);
	}
	private IEnumerator RespawnCounterCouritine()
	{
		int respawnCounter = 5;
		while(GetComponent<PlayerController>().death)
		{
			respawnCounter--; 
			respawnText.text = "Respawning in: " + respawnCounter.ToString();
			if(respawnCounter == 0)
			{
				Respawn();
			}
			yield return new WaitForSeconds(1);
		}
	}
	private IEnumerator InvincibleCounterCouritine()
	{
		float invincibleCounter = 3;
		while(_invincible)
		{
			invincibleCounter -= 0.25f; 
			if(sphere.renderer.material.color.a != 1)
			{
				Color newColor = sphere.renderer.material.color;
				newColor.a = 1;
				sphere.renderer.material.SetColor("_Color", newColor);
			} else {
				Color newColor = sphere.renderer.material.color;
				newColor.a = 0.25f;
				sphere.renderer.material.SetColor("_Color", newColor);
			}
			if(invincibleCounter == 0)
			{
				_invincible = false;
			}
			yield return new WaitForSeconds(0.25f);
		}
	}
	private void Respawn()
	{
		respawnCanvas.SetActive(false);
		//character.SetActive(true);
		sphere.SetActive(true);
		this.transform.localPosition = spawnPoint.transform.localPosition;
		this.transform.eulerAngles = new Vector3(0f,180f,0);
		GetComponent<PlayerController>().death = false;
		_health = 100;
		GameObject.FindGameObjectWithTag("UI").GetComponent<UIScript>().UpdateHealthBar(_health);
		GameObject.FindGameObjectWithTag("GeneralController").GetComponent<GeneralController>().SubtractGold(25f);
		_invincible = true;
		StartCoroutine("InvincibleCounterCouritine");
	}
}
