using UnityEngine;
using System.Collections;

public class FlameTrapFlameBehavior : MonoBehaviour {

	void OnParticleCollision (GameObject other) {
		if(other.transform.tag == "Enemy")
		{
			other.GetComponent<EnemyBehavior>().SetOnFire();
		}
	}
}
