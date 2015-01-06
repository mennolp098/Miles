using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIScript : MonoBehaviour {
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Sprite[] traps;
    private Image[] images;
    private Slider healthBar;
	// Use this for initialization
	void Start () 
    {
        images = gameObject.GetComponentsInChildren<Image>();
        healthBar = gameObject.GetComponentInChildren<Slider>();
        for (int i = 0; i < 4;i++ )
        {
            images[i + 1].sprite = traps[PlayerPrefs.GetInt("hotBar"+i)];
        }
	}
    void Update ()
    {
        healthBar.value = player.GetComponent<HealthController>()._health;
    }
}
