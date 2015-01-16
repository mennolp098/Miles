using UnityEngine;
using System.Collections;

public class GateScript : MonoBehaviour     
{
    [SerializeField]
    private int _health;
    [SerializeField]
    private Canvas loseScreen;
	[SerializeField]
	private Canvas uiCanvas;
	void OnTriggerEnter(Collider other)
	{
		if(other.transform.tag == "Enemy")
		{
			hit ();
			other.gameObject.GetComponent<EnemyBehavior>().isOnStage = false;
			Destroy(other.gameObject);
		}
	}
    public void hit()
    {
        _health--;
		GameObject.FindGameObjectWithTag("UI").GetComponent<UIScript>().UpdateGateBar(_health);
        if(_health <= 0)
        {
            loseScreen.gameObject.SetActive(true);
			uiCanvas.gameObject.SetActive(false);
			GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().YouLost();
        }
    }
}
