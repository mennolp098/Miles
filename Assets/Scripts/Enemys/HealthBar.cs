using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
	public GameObject greenBar;

	private float initialGreenLength;
	private float health =100;
	private float maxHealth = 100;
	
	void Start(){
		health = GetComponentInParent<EnemyBehavior>().GetHealth();
		maxHealth = health;
		initialGreenLength = greenBar.transform.localScale.x;
	}
	
	void Update(){
		health = GetComponentInParent<EnemyBehavior>().GetHealth();
		if(health >= 1)
		{
			Vector3 newScale = greenBar.transform.localScale;
			newScale.x = initialGreenLength*(health/maxHealth);
			greenBar.transform.localScale = newScale;
		}
	}
}