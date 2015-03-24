using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControllerMouse : MonoBehaviour {
	// Update is called once per frame
	private string _horizontalAxisMovement;
	private string _verticalAxisMovement;
	private float _speed;
	void Start ()
	{
		_verticalAxisMovement = "JoyVertical01";
		_horizontalAxisMovement = "JoyHorizontal01";
		_speed = 300f;
	}
	void Update () 
	{
		Vector3 movement = new Vector3(Input.GetAxis(_horizontalAxisMovement), Input.GetAxis(_verticalAxisMovement), 0);
		movement = transform.TransformDirection(movement);
		movement *= _speed;
		CharacterController controller = GetComponent<CharacterController>();
		controller.Move(movement*Time.deltaTime);
	}
	void OnTriggerStay(Collider other)
	{
		if(other.transform.tag == "Level01Button")
		{
			if(Input.GetKeyDown(KeyCode.Joystick1Button0))
			{
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<menuScript>().selectLevel(1);
			}
		} else if(other.transform.tag == "Level02Button")
		{
			if(Input.GetKeyDown(KeyCode.Joystick1Button0))
			{
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<menuScript>().selectLevel(2);
			}
		}
	}
	public float GetX()
	{
		return this.GetComponent<RectTransform>().position.x;
	}
	public float GetY()
	{
		return (this.GetComponent<RectTransform>().position.y-600)*-1;
	}
	public float GetZ()
	{
		return this.GetComponent<RectTransform>().position.z;
	}
}
