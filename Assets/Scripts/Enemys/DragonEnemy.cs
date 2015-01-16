using UnityEngine;
using System.Collections;

public class DragonEnemy : FlyingEnemy {
	public GameObject flames;
	// Use this for initialization
	protected override void Start () {
		_health = 30f;
		_speed = 3f;
		_myGold = 5f;
		sort = 5;
		base.Start();
		flames.particleSystem.enableEmission = false;
		Invoke("FireFlames", Random.Range(3,20));
	}
	private void FireFlames()
	{
		if(!_death)
		{
			childAnims.SetBool("attacking", true);
			flames.particleSystem.enableEmission = true;
			Invoke("StopFlames", 4f);
			audio.Play();
			Invoke("FireFlames", Random.Range(3,20));
		}
	}
	private void StopFlames()
	{
		childAnims.SetBool("attacking", false);
		flames.particleSystem.enableEmission = false;
	}
}
