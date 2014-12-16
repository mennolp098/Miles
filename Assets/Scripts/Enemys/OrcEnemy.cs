using UnityEngine;
using System.Collections;

public class OrcEnemy : GroundEnemy {

	// Use this for initialization
	protected override void Start () {
		_myGold = 20f;
		_speed = 0.5f;
		sort = 2;
		base.Start();
	}
}
