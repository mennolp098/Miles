using UnityEngine;
using System.Collections;

public class EnemyWalkSoundHandler : MonoBehaviour {
	AnimationEvent Walk()
	{
		gameObject.GetComponentInParent<EnemyBehavior>().PlayWalkSound();
		return null;
	}
}
