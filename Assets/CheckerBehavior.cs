
using UnityEngine;
using System.Collections;

public class CheckerBehavior : MonoBehaviour {

	// Use this for initialization
	void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawSphere(this.transform.position, 0.1f);
	}
	void Update () {
		Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 1f);
		for (int i = 0; i < hitColliders.Length; i++) {
			if(hitColliders[i].transform.tag != "BuildTrap")
			{
				GetComponentInParent<BuildTrapBehavior>().hasToRotate = true;
				break;
			} else {
				GetComponentInParent<BuildTrapBehavior>().hasToRotate = false;
			}
		}
	}
}
