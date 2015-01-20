using UnityEngine;
using System.Collections;

public class BuilderPlayerTwo : TrapBuilder {

	// Use this for initialization
	protected override void Start ()
	{
		base.Start ();
		key01 = KeyCode.Joystick1Button0;
		key02 = KeyCode.Joystick1Button1;
		key03 = KeyCode.Joystick1Button2;
		key04 = KeyCode.Joystick1Button3;
	}
	protected override void BuildInput ()
	{
		if(Input.GetAxis("Fire01") >= 0.5f && isBuilding)
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
