using UnityEngine;
using System.Collections;

public class projectileScript : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
		Destroy(this.gameObject, 10f);
	}
	
	void Update () 
    {
        transform.Translate(Vector3.forward);
	}
    void OnTriggerEnter(Collider other)
    {
		if(!other.isTrigger)
		{
	        if(other.transform.tag == "Enemy")
			{
				other.GetComponent<EnemyBehavior>().GetDmg(1);
				Destroy(this.gameObject);
			}
		}
    }
}
