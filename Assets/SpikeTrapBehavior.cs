using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpikeTrapBehavior : MonoBehaviour {
	private List<EnemyBehavior> _enemyScripts = new List<EnemyBehavior>();
	private float _spikeCooldown = 0;
	public float spikeCooldown;
	public float spikeDmg;
	void OnTriggerEnter(Collider other)
	{
		EnemyBehavior enemyScript = other.GetComponent<EnemyBehavior> ();
		if(other.transform.tag == "Enemy" && Time.time > _spikeCooldown)
		{
			_enemyScripts.Add(enemyScript);
			TriggerSpikes();
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
	void TriggerSpikes()
	{
		_spikeCooldown = Time.time + spikeCooldown;
		for(int i = 0; i < _enemyScripts.Count; i++)
		{
			_enemyScripts[i].GetDmg(spikeDmg);
			_enemyScripts[i].GetStunned(0.5f);
		}
	}
}
