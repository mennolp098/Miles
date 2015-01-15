using UnityEngine;
using System.Collections;

public class FlameTrapFlameBehavior : MonoBehaviour {
	private float _fireDamage = 2f;
	private float _fireSpeed = 0.25f;

	void OnParticleCollision (GameObject other) {
		if(other.transform.tag == "Enemy")
		{
			other.GetComponent<EnemyBehavior>().SetOnFire(_fireDamage, _fireSpeed);
		}
	}
}
