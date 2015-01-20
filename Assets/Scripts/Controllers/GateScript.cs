using UnityEngine;
using System.Collections;

public class GateScript : MonoBehaviour     
{
    [SerializeField]
    private int _health;
    [SerializeField]
    private Canvas _loseScreen;

	private GameObject _uiCanvas;
	void Start()
	{
		_uiCanvas = GameObject.FindGameObjectWithTag("UI");
	}
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
		_uiCanvas.GetComponent<UIScript>().UpdateGateBar(_health);
        if(_health <= 0)
        {
            _loseScreen.gameObject.SetActive(true);
			_uiCanvas.gameObject.SetActive(false);
			GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
			foreach(GameObject player in allPlayers)
			{
				player.GetComponent<PlayerController>().YouLost();
			}
        }
    }
}
