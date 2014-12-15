using UnityEngine;
using System.Collections;

public class FastEnemy : FlyingEnemy {

	// Use this for initialization
	protected override void Start () {
		_speed = 3f;
		_myGold = 10f;
		sort = 1;
		base.Start();
	}
}
