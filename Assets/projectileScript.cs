using UnityEngine;
using System.Collections;

public class projectileScript : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        Invoke("kill", 10);
	}
	
	void Update () 
    {
        transform.Translate(Vector3.forward);
	}
    void kill()
    {
        Destroy(this.gameObject);
    }
}
