using UnityEngine;
using System.Collections;

public class TrapBuilder02 : TrapBuilder {

	// Use this for initialization
	protected override void Update ()
	{
		KeyInput();
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
		if(isBuilding)
		{
			CheckWhereToBuild();
		}
	}
	protected override void KeyInput ()
	{
		if(Input.GetKeyDown(KeyCode.JoystickButton0))
		{
			BuildTrap(0);
		} else if(Input.GetKeyDown(KeyCode.JoystickButton1)) 
		{
			BuildTrap(1);
		} else if(Input.GetKeyDown(KeyCode.JoystickButton2)) 
		{
			BuildTrap(2);
		} else if(Input.GetKeyDown(KeyCode.JoystickButton3)) 
		{
			BuildTrap(3);
		}
	}
}
