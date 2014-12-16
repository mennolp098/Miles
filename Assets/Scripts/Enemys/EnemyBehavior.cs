using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EnemyBehavior : MonoBehaviour, IComparable<EnemyBehavior> {
	public bool isOnStage;
	public float health;
	public Transform thisTransform;
	public int sort;
	public GameObject greenbar;
	public GameObject redbar;
	public GameObject goldPrefab;
	public Animator childAnims;

	protected bool _dead;
	protected float _speed = 0.03f;
	protected float _oldSpeed;
	protected float _myGold = 0;
	protected List<Material> allChildrenMaterials = new List<Material>();
	protected NavMeshAgent _navMesh;
	protected Transform allCoins;
	protected GameObject target;
	protected DateTime TimeAdded;
	protected float counter = 1;
   
	public int CompareTo(EnemyBehavior other)
	{
		if(this.health < other.health)
		{
			return this.health.CompareTo(other.health);
		} 
		else
		{
			if(other.sort == this.sort)
			{
				return this.TimeAdded.CompareTo(other.TimeAdded);
			}
			return other.sort.CompareTo(this.sort);
		}
	}
	protected virtual void Start () 
	{
		allCoins = GameObject.FindGameObjectWithTag("allCoins").transform;
		thisTransform = this.transform;
		TimeAdded = DateTime.Now;
		isOnStage = true;
		_oldSpeed = _speed;
		Renderer[] allChildrenRenderers = GetComponentsInChildren<Renderer>();
		foreach(Renderer renderer in allChildrenRenderers)
		{
			allChildrenMaterials.Add(renderer.material);
		}
	}
    protected virtual void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.tag == "Portal")
        {
            other.gameObject.GetComponent<GateScript>().hit();
			Destroy(this.gameObject);
			isOnStage = false;
        }
    }
	public void SetHealth(float newHealth)
	{
		health = newHealth;
	}
	public void GetDmg(float dmg)
	{
		health -= dmg;
		if(health <= 0)
		{
			Die();
		}
	}
	public void GetStunned(float time)
	{
		if(_navMesh != null)
		{
			_navMesh.speed = 0;
		} else {
			_speed = 0;
		}
		Invoke("StopStun", time);
	}
	private void StopStun()
	{
		if(_navMesh != null)
		{
			_navMesh.speed = _oldSpeed;
		} else {
			_speed = _oldSpeed;
		}
	}
	protected virtual void Die()
	{
		//TODO: give score or money?
		for(int i = 0; i < _myGold/5; i++)
		{
			Vector3 randomSpawnPos = this.transform.position;
			randomSpawnPos.x += UnityEngine.Random.Range(-3,3);
			randomSpawnPos.z += UnityEngine.Random.Range(-3,3);
			randomSpawnPos.y += UnityEngine.Random.Range(1,5);
			GameObject newGoldCoin = Instantiate(goldPrefab, randomSpawnPos,Quaternion.identity) as GameObject;
			newGoldCoin.rigidbody.AddExplosionForce(200f,this.transform.position,20f);
			newGoldCoin.transform.parent = allCoins;
		}
		//audio.Play();
		childAnims.SetTrigger("dead");
		_dead = true;
		Destroy(this.rigidbody);
		Destroy(this.collider);
		Destroy(greenbar.gameObject);
		Destroy(redbar.gameObject);
		Destroy(this.gameObject, 20f);
		isOnStage = false;
	}
}
