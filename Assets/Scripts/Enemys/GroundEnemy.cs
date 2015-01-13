using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GroundEnemy : EnemyBehavior {
	protected override void Start () {
		base.Start();
		target = GameObject.Find ("Waypoint-" + UnityEngine.Random.Range(1,3));
		_navMesh = GetComponent<NavMeshAgent>();
		_navMesh.SetDestination(target.transform.position);
		_navMesh.speed += _speed;
		_oldSpeed = _navMesh.speed;
	}
	protected virtual void Update () {
		if(rigidbody != null)
		{
			rigidbody.velocity = Vector3.zero;
			rigidbody.angularVelocity = Vector3.zero;
		}
		if(target)
		{
			if(Vector2.Distance (new Vector2(transform.position.x,transform.position.z), new Vector2(target.transform.position.x,target.transform.position.z)) < 3f)
			{
				counter++;
				if(counter == 2)
				{
					counter = 3;
				}
				var newWaypointName = "Waypoint-" + counter;
				GameObject newWaypoint = GameObject.Find(newWaypointName);
				target = newWaypoint;
				
				if(target == null)
				{
					//Debug.LogWarning("no waypoints found!");
					Destroy(this.gameObject);
				}
				else
				{
					_navMesh.SetDestination(target.transform.position);
				}
			}
		}
	}
	protected override void Die ()
	{
		base.Die ();
		Destroy(_navMesh);
	}
}
