using UnityEngine;
using System.Collections;

public class GateScript : MonoBehaviour     
{
    private int _health;
    public void hit()
    {
        _health--;
        if(_health == 0)
        {
        Debug.Log("die");
        }
    }
}
