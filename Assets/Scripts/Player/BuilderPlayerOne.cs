using UnityEngine;
using System.Collections;

public class BuilderPlayerOne : TrapBuilder {

	// Use this for initialization
	protected override void Start ()
	{
		base.Start ();
		key01 = KeyCode.Alpha1;
		key02 = KeyCode.Alpha2;
		key03 = KeyCode.Alpha3;
		key04 = KeyCode.Alpha4;
	}
	protected override void BuildInput ()
	{
		if(Input.GetMouseButtonDown(0) && isBuilding)
		{
			if(_currentTrap != null)
			{
				if(_currentTrap.GetComponent<BuildTrapBehavior>().buildAble)
				{
					SpawnTrap();
				} else {
					ClearTrap();
				}
			}
			else
			{
				ClearTrap();
			}
		}
	}
}
