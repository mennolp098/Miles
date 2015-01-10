using UnityEngine;
using System.Collections;

public class ArcherEventHandler : MonoBehaviour {

	AnimationEvent Shoot()
	{
		gameObject.GetComponentInParent<ArcherEnemy>().Shoot();
		return null;
	}
}
