using UnityEngine;
using System.Collections;

public class FastEnemy : EnemyBehavior {

	// Use this for initialization
	protected override void Start () {
		_speed = 1f;
		_myGold = 10f;
		sort = 1;
		base.Start();
	}
}
