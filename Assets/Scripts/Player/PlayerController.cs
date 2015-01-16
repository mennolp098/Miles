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


	private  enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    private RotationAxes axes = RotationAxes.MouseXAndY;
    private float sensitivityX = 15F;
    private float sensitivityY = 15F;

    private float minimumY = -20F;
    private float maximumY = 20F;
    private float rotationY = 0F;
    private float speed = 10;
	private float gravity = 25f;
	private float cooldown;
	private float shootCooldown = 0.5f;

    [SerializeField]
    private GameObject spell;
    [SerializeField]
    private Transform spawn;

	void OnGUI()
	{
		GUI.DrawTexture(new Rect(Screen.width/2 -16, Screen.height/2 , 32, 32), cursorTexture);
	}
	void Awake()
	{
		Screen.lockCursor = true;
	}
    void Start()
    {
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
		particleSystem.enableEmission = false;
		audio.loop = false;
    }
	void Update ()
	{
		if(!death && playerModel.activeInHierarchy)
		{
	        if(Input.GetKey(KeyCode.Mouse0))
	        {
				ShootSpell();
	        }
			Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
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
				float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
				
				rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
				rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
				
				transform.localEulerAngles = new Vector3(0, rotationX, 0);
				playerCamera.localEulerAngles = new Vector3(-rotationY,0,0);
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
	private void ShootSpell()
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
	private void StopShooting()
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