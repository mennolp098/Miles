using UnityEngine;
using System.Collections;

public class OrcEnemy : GroundEnemy {

	// Use this for initialization
	protected override void Start () {
		health = 40f;
		_myGold = 20f;
		_speed = 0.5f;
		sort = 2;
		base.Start();
	}
}
