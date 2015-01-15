using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpikeTrapBehavior : GroundTrapBehavior {
	private float _attackDamage = 10f;
	private float _stunTime = 2.5f;

	protected override void DoAttack ()
	{
		base.DoAttack();
		GetComponentInChildren<Animator>().SetTrigger("shoot");
		for(int i = 0; i < _enemyScripts.Count; i++)
		{
			_enemyScripts[i].GetDmg(_attackDamage);
			_enemyScripts[i].GetStunned(_stunTime);
		}

	}
}

