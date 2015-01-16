using UnityEngine;
using System.Collections;

public class OrcEnemy : GroundEnemy {

	// Use this for initialization
	protected override void Start () {
		_health = 40f;
		_myGold = 10f;
		_speed = 0.5f;
		sort = 0;
		base.Start();
	}
}
