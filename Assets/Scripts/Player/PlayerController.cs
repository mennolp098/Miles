using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public Texture2D cursorTexture;
	public Transform playerCamera;
	public Animator animator;
	public bool death;
	public AudioClip[] allSounds = new AudioClip[0];
	public GameObject playerModel;
	public Camera currentCam;

	protected  enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    protected RotationAxes axes = RotationAxes.MouseXAndY;
    protected float sensitivityX = 15F;
    protected float sensitivityY = 15F;
    protected float minimumY = -20F;
    protected float maximumY = 20F;
    protected float rotationY = 0F;
    protected float speed = 10;
	protected float gravity = 25f;
	protected float cooldown;
	protected float shootCooldown = 0.5f;

    [SerializeField]
    protected GameObject spell;
    [SerializeField]
    protected Transform spawn;

	protected string horizontalAxisMovement;
	protected string verticalAxisMovement;
	protected string horizontalAxisAim;
	protected string verticalAxisAim;
	
	protected virtual void OnGUI()
	{
		GUI.DrawTexture(new Rect(currentCam.pixelWidth/2 + currentCam.rect.x * Screen.width -16, currentCam.pixelHeight/2 , 32, 32), cursorTexture);
	}
	void Awake()
	{
		Screen.lockCursor = true;
	}
    protected virtual void Start()
    {
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
		particleSystem.enableEmission = false;
		audio.loop = false;
    }
	void Update ()
	{
		MovementInput();
		ShootInput();
	}
	protected virtual void ShootInput()
	{
		//here comes to shootinput
	}
	protected void MovementInput()
	{
		if(!death && playerModel.activeInHierarchy)
		{
			Vector3 movement = new Vector3(Input.GetAxis(horizontalAxisMovement), 0, Input.GetAxis(verticalAxisMovement));
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
				float rotationX = transform.localEulerAngles.y + Input.GetAxis(horizontalAxisAim) * sensitivityX;
				
				rotationY += Input.GetAxis(verticalAxisAim) * sensitivityY;
				rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
				
				transform.localEulerAngles = new Vector3(0, rotationX, 0);
				playerCamera.localEulerAngles = new Vector3(-rotationY,0,0);
			}
			else if (axes == RotationAxes.MouseX)
			{
				transform.Rotate(0, Input.GetAxis(horizontalAxisAim) * sensitivityX, 0);
			}
			else
			{
				rotationY += Input.GetAxis(verticalAxisAim) * sensitivityY;
				rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
				
				transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
			}
		}
	}
	protected void ShootSpell()
	{
		if(cooldown <= Time.time)
		{
			cooldown = Time.time + shootCooldown;
			animator.SetTrigger("shootTrigger");
			animator.SetBool("isShooting", true);
			Invoke("StopShooting", 1f);
			Instantiate(spell, spawn.position, spawn.rotation);
			audio.clip = allSounds[0];
			audio.Play();
		}
	}
	public void YouLost()
	{
		playerModel.SetActive(false);
		Collider[] colliders = GetComponents<SphereCollider>();
		foreach(Collider collider in colliders)
		{
			Destroy(collider);
		}
		Screen.lockCursor = false;
	}
	protected void StopShooting()
	{
		animator.SetBool("isShooting", false);
	}
	public void SetOnFire()
	{
		if(!particleSystem.enableEmission && !death)
		{
			particleSystem.enableEmission = true;
			StartCoroutine("OnFire");
			Invoke("StopFire", 3f);
		}
	}
	public IEnumerator OnFire()
	{
		while(particleSystem.enableEmission)
		{
			GetComponent<HealthController>().SubtractHealth(1f);
			yield return new WaitForSeconds(0.25f);
		}
	}
	public void StopFire()
	{
		particleSystem.enableEmission = false;
	}
	AnimationEvent Walk()
	{
		audio.clip = allSounds[1];
		audio.Play();
		return null;
	}
}