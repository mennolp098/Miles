using UnityEngine;
using System.Collections;

public class Player02Controller : PlayerController {

	// Use this for initialization
	protected override void Start()
	{
		base.Start();
		sensitivityX = 2f;
		sensitivityY = 2f;
	}
	protected override void MovementInput () {
		if(!death && playerModel.activeInHierarchy)
		{
			if(Input.GetAxis("Fire01") >= 0.5f)
			{
				ShootSpell();
			}
			Vector3 movement = new Vector3(Input.GetAxis("JoyHorizontal01"), 0, Input.GetAxis("JoyVertical01"));
			movement = transform.TransformDirection(movement);
			movement *= speed;
			movement.y = -gravity;
			CharacterController controller = GetComponent<CharacterController>();
			controller.Move(movement*Time.deltaTime);
			if(movement.x > 0 || movement.z > 0 || movement.x < 0 || movement.z < 0)
			{
				animator.SetBool("isWalking", true);
			} else {
				animator.SetBool("isWalking", false);
			}
			if (axes == RotationAxes.MouseXAndY)
			{
				float rotationX = transform.localEulerAngles.y + Input.GetAxis("JoyHorizontal02") * sensitivityX;
				
				rotationY += Input.GetAxis("JoyVertical02") * sensitivityY;
				rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
				
				transform.localEulerAngles = new Vector3(0, rotationX, 0);
				playerCamera.localEulerAngles = new Vector3(-rotationY,0,0);
			}
			else if (axes == RotationAxes.MouseX)
			{
				transform.Rotate(0, Input.GetAxis("JoyHorizontal02") * sensitivityX, 0);
			}
			else
			{
				rotationY += Input.GetAxis("JoyVertical02") * sensitivityY;
				rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
				
				transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
			}
		}
	}
}
