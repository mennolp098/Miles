using UnityEngine;
using System.Collections;

public class RemoveParticleSystem : MonoBehaviour {
	private ParticleSystem _particleSystem;
	// Use this for initialization
	void Start () {
		_particleSystem = GetComponent<ParticleSystem>();
		Destroy(this.gameObject, _particleSystem.startLifetime);
	}
}
