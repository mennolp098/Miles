using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIScript : MonoBehaviour {
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Sprite[] traps;
    private Image[] images;
	[SerializeField]
    private Slider healthBar;
	[SerializeField]
	private Slider gateBar;
	// Use this for initialization
	void Start () 
    {
        images = gameObject.GetComponentsInChildren<Image>();
        for (int i = 0; i < 4;i++ )
        {
            images[i + 1].sprite = traps[PlayerPrefs.GetInt("hotBar"+i)];
        }
	}
	public void UpdateHealthBar (float newHealth)
    {
        healthBar.value = newHealth;
    }
	public void UpdateGateBar(float newHealth)
	{
		gateBar.value = newHealth;
	}
}
