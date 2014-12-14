using UnityEngine;
using System.Collections;

public class FlameTrapBehavior : GroundTrapBehavior {
	public GameObject flameParticleSystem;
	protected override void TriggerTrap ()
	{
		base.TriggerTrap ();
		flameParticleSystem.SetActive(true);
		Invoke("StopEmitting", 2f);
	}
	private void StopEmitting()
	{
		flameParticleSystem.SetActive(false);
	}
}
