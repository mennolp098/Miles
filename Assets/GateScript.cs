using UnityEngine;
using System.Collections;

public class GateScript : MonoBehaviour     
{
    [SerializeField]
    private int _health;
    [SerializeField]
    private Canvas loseScreen;
    public void hit()
    {
        _health--;
        if(_health == 0)
        {
            loseScreen.active = true;
        }
    }
}
