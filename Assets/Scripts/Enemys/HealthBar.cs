using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
	public GameObject greenBar;

	private float initialGreenLength;
	private float health =100;
	private float maxHealth = 100;
	
	void Start(){
		health = GetComponentInParent<EnemyBehavior>().health;
		maxHealth = health;
		initialGreenLength = greenBar.transform.localScale.z;
	}
	
	void Update(){
		health = GetComponentInParent<EnemyBehavior>().health;
		if(health >= 1)
		{
			Vector3 newScale = greenBar.transform.localScale;
			newScale.z = initialGreenLength*(health/maxHealth);
			greenBar.transform.localScale = newScale;
		}
	}
}