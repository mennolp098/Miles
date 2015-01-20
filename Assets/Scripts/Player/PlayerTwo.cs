using UnityEngine;
using System.Collections;

public class PlayerTwo : PlayerController {

	protected override void Start ()
	{
		base.Start ();
		verticalAxisAim = "JoyVertical02";
		verticalAxisMovement = "JoyVertical01";
		horizontalAxisAim = "JoyHorizontal02";
		horizontalAxisMovement = "JoyHorizontal01";
		sensitivityX = 2f;
		sensitivityY = 2f;
	}
	protected override void ShootInput ()
	{
		if(Input.GetAxis("Fire01") >= 0.5f)
		{
			ShootSpell();
		}
	}
}
