using UnityEngine;
using System.Collections;

public class PlayerOne : PlayerController {
	protected override void Start ()
	{
		base.Start ();
		verticalAxisAim = "Mouse Y";
		verticalAxisMovement = "Vertical";
		horizontalAxisAim = "Mouse X";
		horizontalAxisMovement = "Horizontal";
	}
	protected override void ShootInput ()
	{
		if(Input.GetKey(KeyCode.Mouse0))
		{
			ShootSpell();
		}
	}
}
