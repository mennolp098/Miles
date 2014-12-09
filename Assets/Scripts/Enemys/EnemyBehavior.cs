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

	protected float _speed = 0.03f;
	protected float _oldSpeed;
	protected float _myGold = 0;
	protected List<Material> allChildrenMaterials = new List<Material>();

	private GameObject target;
	private DateTime TimeAdded;
	private NavMeshAgent _navMesh;
	private float counter = 1;
   
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

	protected virtual void Start () {
		target = GameObject.Find ("Waypoint-" + UnityEngine.Random.Range(1,3));
		thisTransform = this.transform;
		TimeAdded = DateTime.Now;
		isOnStage = true;
        _navMesh = GetComponent<NavMeshAgent>();
        _navMesh.SetDestination(target.transform.position);
		_navMesh.speed += _speed;
		_oldSpeed = _navMesh.speed;
		Renderer[] allChildrenRenderers = GetComponentsInChildren<Renderer>();
		foreach(Renderer renderer in allChildrenRenderers)
		{
			allChildrenMaterials.Add(renderer.material);
		}
	}
	void Update () {
		if(target)
		{
			if(Vector2.Distance (new Vector2(transform.position.x,transform.position.z), new Vector2(target.transform.position.x,target.transform.position.z)) < 1.5f)
			{
				counter++;
				var newWaypointName = "Waypoint-" + counter;
				GameObject newWaypoint = GameObject.Find(newWaypointName);
				target = newWaypoint;
                
				if(target == null)
				{
					Debug.LogWarning("no waypoints found!");
				}
                else
                {
                    _navMesh.SetDestination(target.transform.position);
                }
			}
		}
	}
    private void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.tag == "Portal")
        {
            other.gameObject.GetComponent<GateScript>().hit();
			Destroy(this.gameObject);
			isOnStage = false;
        }
        if (other.gameObject.tag == "spell")
        {
            Destroy(other.gameObject);
            GetDmg(1);
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
		_navMesh.speed = 0;
		Invoke("StopStun", time);
	}
	private void StopStun()
	{
		if(_navMesh != null)
		{
			_navMesh.speed = _oldSpeed;
		}
	}
	public void GetPushed(Quaternion rotation)
	{
		//this.transform.position += 
		//TODO: pushed from rotation angle
	}
	private void Die()
	{
		//TODO: give score or money?
		//audio.Play();
		Destroy(this.rigidbody);
		Destroy(_navMesh);
		Destroy(this.collider);
		Destroy(greenbar.gameObject);
		Destroy(redbar.gameObject);
		isOnStage = false;
	}
}
