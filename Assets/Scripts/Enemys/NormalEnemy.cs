using UnityEngine;
using System.Collections;

public class NormalEnemy : EnemyBehavior {

	// Use this for initialization
	protected override void Start () {
		_speed = 0.5f;
		_myGold = 100f;
		sort = 3;
		base.Start();
	}
}
