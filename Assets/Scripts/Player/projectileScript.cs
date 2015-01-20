using UnityEngine;
using System.Collections;

public class projectileScript : MonoBehaviour {
	public GameObject hitExplosionPrefab;
	public float attackDamage;
	// Use this for initialization
	void Start () 
    {
		Destroy(this.gameObject, 10f);
        if (PlayerPrefs.GetInt("multiplayer") == 1)
		{
			attackDamage = 5f;
		} else {
			attackDamage = 10f;
		}
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
				other.GetComponent<EnemyBehavior>().GetDmg(attackDamage);
				GameObject hitExplosion = Instantiate(hitExplosionPrefab,this.transform.position,this.transform.rotation) as GameObject;
				Vector3 newRot = this.transform.eulerAngles;
				newRot.y -= 180;
				hitExplosion.transform.eulerAngles = newRot;
				Destroy(this.gameObject);
			} else if(other.transform.tag != "Player"){
				GameObject hitExplosion = Instantiate(hitExplosionPrefab,this.transform.position,this.transform.rotation) as GameObject;
				Vector3 newRot = this.transform.eulerAngles;
				newRot.y -= 180;
				hitExplosion.transform.eulerAngles = newRot;
				Destroy(this.gameObject);
            }
            else if (PlayerPrefs.GetInt("multiplayer") == 1)
			{
				other.GetComponent<HealthController>().SubtractHealth(attackDamage);
				GameObject hitExplosion = Instantiate(hitExplosionPrefab,this.transform.position,this.transform.rotation) as GameObject;
				Vector3 newRot = this.transform.eulerAngles;
				newRot.y -= 180;
				hitExplosion.transform.eulerAngles = newRot;
				Destroy(this.gameObject);
			}
		}
    }
}
