using UnityEngine;
using System.Collections;

public class FlameTrapBehavior : GroundTrapBehavior {
	public GameObject flameParticleSystem;

	private float _fireTime = 2f;
	protected override void TriggerTrap ()
	{
		base.TriggerTrap ();
		flameParticleSystem.SetActive(true);
		Invoke("StopEmitting", _fireTime);
	}
	private void StopEmitting()
	{
		flameParticleSystem.SetActive(false);
	}
}
