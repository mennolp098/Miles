using UnityEngine;
using System.Collections;

public class FlameBehavior : MonoBehaviour {

	// Use this for initialization
	void OnParticleCollision (GameObject other) {
		if(other.transform.tag == "Player")
		{
			other.GetComponent<PlayerController>().SetOnFire();
		}
	}
}
