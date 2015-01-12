using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class loseScript : MonoBehaviour {

    public void replay()
    {
        Application.LoadLevel("levelOne");
    }
    public void menu()
    {
        Application.LoadLevel("Menu");
    }
}
