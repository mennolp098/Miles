using UnityEngine;
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
		if(rigidbody != null)
		{
			rigidbody.velocity = Vector3.zero;
			rigidbody.angularVelocity = Vector3.zero;
		}
		if(target)
		{
			this.transform.position = Vector3.MoveTowards(this.transform.position,target.transform.position, _speed * Time.deltaTime);
			if(Vector2.Distance (new Vector2(transform.position.x,transform.position.z), new Vector2(target.transform.position.x,target.transform.position.z)) < 3f)
			{
				if(counter == 1)
				{
					counter = Random.Range(2,4);
				} else if(counter == 2)
				{
					counter = 4;
				} else if(counter == 3)
				{
					counter = 5;
				} else if(counter == 4)
				{
					counter = 6;
				} else {
					counter++;
				}
				var newWaypointName = "FlyWaypoint-" + counter;
				GameObject newWaypoint = GameObject.Find(newWaypointName);
				target = newWaypoint;
				
				if(target == null)
				{
					isOnStage = false;
					GameObject.FindGameObjectWithTag("Portal").GetComponent<GateScript>().hit();
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
		if(_death && this.transform.position.y >= 3)
		{
			Vector3 fallmovement = Vector3.zero;
			fallmovement.y = -10;
			this.transform.position += fallmovement * Time.deltaTime;
		}
	}
	protected override void Die ()
	{
		base.Die ();
		target = null;
	}
}
