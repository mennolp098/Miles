using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public Texture2D cursorTexture;

	private  enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    private RotationAxes axes = RotationAxes.MouseXAndY;
    private float sensitivityX = 15F;
    private float sensitivityY = 15F;

    private float minimumY = -30F;
    private float maximumY = 30F;

    private float rotationY = 0F;
    private float speed = 10;
	private float gravity = 10f;
    [SerializeField]
    private GameObject spell;
    [SerializeField]
    private Transform spawn;

	void OnGUI()
	{
		GUI.DrawTexture(new Rect(Screen.width/2 -16, Screen.height/2 - 16, 32, 32), cursorTexture);
	}
	void Awake()
	{
		Screen.lockCursor = true;
	}
    void Start()
    {
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
    }
	void Update ()
	{
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(spell, spawn.position,transform.rotation);
        }
		Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		movement = transform.TransformDirection(movement);
		movement *= speed;
		movement.y = -gravity;
		CharacterController controller = GetComponent<CharacterController>();
		controller.Move(movement*Time.deltaTime);
		if (axes == RotationAxes.MouseXAndY)
		{
			float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
			
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
			transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
		}
		else if (axes == RotationAxes.MouseX)
		{
			transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
		}
		else
		{
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
			transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
		}
	}
}