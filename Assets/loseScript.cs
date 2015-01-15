using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class loseScript : MonoBehaviour {

    public void replay()
    {
        Application.LoadLevel(PlayerPrefs.GetString("Level"));
    }
    public void menu()
    {
        Application.LoadLevel("Menu");
    }
}
