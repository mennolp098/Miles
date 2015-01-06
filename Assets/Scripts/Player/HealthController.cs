using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour {
	public float _health;
	// Use this for initialization
	void Start () {
		_health = 100;
	}
	public void AddHealth(float health)
	{
		_health += health;
	}
	public void SubtractHealth(float health)
	{
		_health -= health;
		if(_health <= 0)
		{
			Debug.LogError("Your death fucker!");
		}
	}
}
