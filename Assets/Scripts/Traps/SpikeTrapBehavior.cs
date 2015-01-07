using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpikeTrapBehavior : GroundTrapBehavior {
	protected override void DoAttack ()
	{
		base.DoAttack();
		GetComponentInChildren<Animator>().SetTrigger("shoot");
		for(int i = 0; i < _enemyScripts.Count; i++)
		{
			_enemyScripts[i].GetDmg(damage);
			_enemyScripts[i].GetStunned(0.5f);
		}

	}
}
