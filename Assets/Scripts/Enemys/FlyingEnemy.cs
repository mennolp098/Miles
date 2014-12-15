﻿using UnityEngine;
using System.Collections;

public class FlyingEnemy : EnemyBehavior {
	protected float flyingHeight;
	// Use this for initialization
	protected override void Start () {
		base.Start();
		target = GameObject.Find ("FlyWaypoint-1");
		flyingHeight = 14f;
	}
	// Update is called once per frame
	void Update () {
		if(target)
		{
			this.transform.position = Vector3.MoveTowards(this.transform.position,target.transform.position, _speed * Time.deltaTime);
			if(Vector2.Distance (new Vector2(transform.position.x,transform.position.z), new Vector2(target.transform.position.x,target.transform.position.z)) < 1.5f)
			{
				counter++;
				var newWaypointName = "FlyWaypoint-" + counter;
				GameObject newWaypoint = GameObject.Find(newWaypointName);
				target = newWaypoint;
				
				if(target == null)
				{
					//Debug.LogWarning("no waypoints found!");
					Destroy(this.gameObject);
				}
			}
			if(this.transform.position.y <= flyingHeight)
			{
				Vector3 movement = Vector3.zero;
				movement.y = _speed;
				this.transform.position += movement * Time.deltaTime;
			}
		}
		if(_dead && this.transform.position.y >= 2)
		{
			Vector3 fallmovement = Vector3.zero;
			fallmovement.y = -6;
			this.transform.position += fallmovement * Time.deltaTime;
		}
	}
	protected override void Die ()
	{
		base.Die ();
		target = null;
	}
}