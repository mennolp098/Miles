using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpikeTrapBehavior : GroundTrapBehavior {
	protected override void DoAttack ()
	{
		for(int i = 0; i < _enemyScripts.Count; i++)
		{
			_enemyScripts[i].GetDmg(damage);
			_enemyScripts[i].GetStunned(0.5f);
		}

	}
}
