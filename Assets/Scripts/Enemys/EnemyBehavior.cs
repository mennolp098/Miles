using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EnemyBehavior : MonoBehaviour, IComparable<EnemyBehavior> {
	public bool isOnStage;

	public Transform thisTransform;
	public int sort;
	public GameObject greenbar;
	public GameObject redbar;
	public GameObject goldPrefab;
	public Animator childAnims;
	public AudioClip[] allSounds = new AudioClip[0];

	protected NavMeshAgent _navMesh;
	protected bool _death;
	protected float _health;
	protected float _speed = 0.03f;
	protected float _oldSpeed;
	protected float _myGold = 0;
	protected List<Material> allChildrenMaterials = new List<Material>();
	protected GameObject target;
	protected DateTime TimeAdded;
	protected float counter = 1;
    protected bool poison =false;
    protected Renderer[] allChildrenRenderers;
	private Transform _allCoins;
	public int CompareTo(EnemyBehavior other)
	{
		if(this._health < other._health)
		{
			return this._health.CompareTo(other._health);
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
		thisTransform = this.transform;
		TimeAdded = DateTime.Now;
		isOnStage = true;
		audio.loop = false;
		audio.clip = allSounds[0];

		_allCoins = GameObject.FindGameObjectWithTag("allCoins").transform;
		_oldSpeed = _speed;

        allChildrenRenderers = GetComponentsInChildren<Renderer>();

	}
	public void SetHealth(float newHealth)
	{
		_health = newHealth;
	}
	public float GetHealth()
	{
		return _health;
	}
	public void GetDmg(float dmg)
	{
		_health -= dmg;
		if(_health <= 0)
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
		if(!_death)
		{
			if(_navMesh != null)
			{
				_navMesh.speed = _oldSpeed;
			} else {
				_speed = _oldSpeed;
			}
		}
	}
	public void SetOnFire(float fireDamage, float damageSpeed)
	{
		if(this.GetComponent<GroundEnemy>())
		{
			if(!particleSystem.enableEmission)
			{
				particleSystem.enableEmission = true;
				StartCoroutine(OnFire(fireDamage, damageSpeed));
				Invoke("StopFire", 3f);
			}
		}
	}
	public IEnumerator OnFire(float fireDamage, float damageSpeed)
	{
		while(particleSystem.enableEmission)
		{
			if(!_death)
			{
				GetDmg(fireDamage);
			} else {
				StopFire();
				break;
			}
			yield return new WaitForSeconds(damageSpeed);
		}
	}
    public void SetPoison(float attackDamage, float attackSpeed)
    {
        if (!poison)
        {
            foreach (Renderer renderer in allChildrenRenderers)
            {
                renderer.material.color = Color.green;
            }
            poison = true;
            StartCoroutine(Poisoned(attackDamage, attackSpeed));
            Invoke("stopPoison", 12f);
        }
    }
    protected void stopPoison()
    {
		if(!_death)
		{
	        foreach (Renderer renderer in allChildrenRenderers)
	        {
	            allChildrenMaterials.Add(renderer.material);
	            renderer.material.color = new Color(1,1,1,1);
	        }
		}
		poison = false;
    }
    protected IEnumerator Poisoned(float damage,float speed)
    {
        while (poison == true)
        {
            if (!_death)
            {
                GetDmg(damage);
            }
            else
            {
                stopPoison();
            }
            yield return new WaitForSeconds(speed);
        }
    }
	public void StopFire()
	{
		particleSystem.enableEmission = false;
	}
	protected virtual void Die()
	{
		SpawnGold();
		DestroyBody();
		audio.clip = allSounds[1];
		audio.Play();
	}
	public void PlayWalkSound()
	{
		audio.clip = allSounds[2];
		audio.Play();
	}
	private void DestroyBody()
	{
		childAnims.SetTrigger("dead");
		_death = true;
		isOnStage = false;
		Destroy(this.rigidbody);
		Destroy(this.collider);
		Destroy(greenbar.gameObject);
		Destroy(redbar.gameObject);
		Destroy(this.gameObject, 20f);
	}
	private void SpawnGold()
	{
		for(int i = 0; i < _myGold/2; i++)
		{
			Vector3 randomSpawnPos = this.transform.position;
			randomSpawnPos.x += UnityEngine.Random.Range(-3,3);
			randomSpawnPos.z += UnityEngine.Random.Range(-3,3);
			randomSpawnPos.y += UnityEngine.Random.Range(1,5);
			GameObject newGoldCoin = Instantiate(goldPrefab, randomSpawnPos,Quaternion.identity) as GameObject;
			newGoldCoin.rigidbody.AddExplosionForce(200f,this.transform.position,20f);
			newGoldCoin.transform.parent = _allCoins;
		}
	}
}
