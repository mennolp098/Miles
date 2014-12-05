using UnityEngine;
using System.Collections;

public class TextFadeOut : MonoBehaviour {
	// Update is called once per frame
	void Update () {
		if(this.guiText.color.a <= 0)
		{
			Destroy(this.gameObject);
		} else 
		{
			Color newColor = this.guiText.color;
			newColor.a -= 0.01f;
			this.guiText.color = newColor;
		}
	}
}
