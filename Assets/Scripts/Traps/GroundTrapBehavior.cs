using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GroundTrapBehavior : MonoBehaviour {
	protected List<EnemyBehavior> _enemyScripts = new List<EnemyBehavior>();
	protected float _coolDown = 0;
	public float coolDown;
	void OnTriggerEnter(Collider other)
	{
		EnemyBehavior enemyScript = other.GetComponent<EnemyBehavior> ();
		if(other.transform.tag == "Enemy" && Time.time > _coolDown)
		{
			_enemyScripts.Add(enemyScript);
			TriggerTrap();
		}
	}
	void OnTriggerExit(Collider other)
	{
		EnemyBehavior enemyScript = other.GetComponent<EnemyBehavior> ();
		if(_enemyScripts.Contains(enemyScript))
		{
			_enemyScripts.Remove(enemyScript);
		}
	}
	void Update()
	{
		for(int i = 0; i < _enemyScripts.Count; i++)
		{
			if(!_enemyScripts[i].isOnStage)
			{
				_enemyScripts.Remove(_enemyScripts[i]);
			}
		}
	}
	protected virtual void TriggerTrap()
	{
		_coolDown = Time.time + coolDown;
		DoAttack();
	}
	protected virtual void DoAttack()
	{
		audio.Play();
	}
}
