using UnityEngine;
using System.Collections;

public class menuScript : MonoBehaviour {
    private Vector3         _middelPoint;
    private Vector2         _buttonSize;
    private float           _trapButtonsize;
    [SerializeField]
    private GameObject[]    _menus;
    [SerializeField]
    private Texture2D[]     _Traps;
    private int             _firstTrap;
    private int[]           _hotBar;
    private int             _selectedtrap;
    void Start()
    {
        _middelPoint = Camera.main.WorldToScreenPoint(GameObject.Find("middelPoint").transform.position);
        _buttonSize = new Vector2(0.1f * Screen.width, .1f * Screen.height);
        _trapButtonsize = 0.1f * Screen.height;
        _firstTrap = 1;
        _hotBar = new int[_Traps.Length -1];
        for(int i = 1; i < 4;i++)
        {
            _menus[i].SetActive(false);
        }
    }

	void OnGUI()
    {
        if (_menus[0].activeSelf == true)
        {
            if (GUI.Button(new Rect(_middelPoint.x - (_buttonSize.x / 2), _middelPoint.y, _buttonSize.x, _buttonSize.y), "Play"))
            {
                _menus[0].SetActive(false);
                _menus[1].SetActive(true);
            }
            if (GUI.Button(new Rect(_middelPoint.x - (_buttonSize.x / 2), _middelPoint.y + (_buttonSize.y * 1.5f), _buttonSize.x, _buttonSize.y), "help"))
            {
                _menus[0].SetActive(false);
                _menus[2].SetActive(true);
            }
            if (GUI.Button(new Rect(_middelPoint.x - (_buttonSize.x / 2), _middelPoint.y + (_buttonSize.y * 3), _buttonSize.x, _buttonSize.y), "cedits"))
            {
                _menus[0].SetActive(false);
                _menus[3].SetActive(true);
            }
        }
        if(_menus[1].activeSelf == true)
        {
            //menu buttons
            if (GUI.Button(new Rect(_middelPoint.x - Screen.width /2,_middelPoint.y,_buttonSize.x,_buttonSize.y), "back"))
            {
                _menus[0].SetActive(true);
                _menus[1].SetActive(false);
            }
            if (GUI.Button(new Rect(_middelPoint.x + Screen.width / 2 - _buttonSize.x, _middelPoint.y, _buttonSize.x, _buttonSize.y), "Play"))
            {
                for(int c = 0;c < _hotBar.Length;c++)
                {
                    PlayerPrefs.SetInt("hotBar"+c, _hotBar[c-1]);
                }
                Application.LoadLevel("Game");
            }
            //hotbar
            if (GUI.Button(new Rect(_middelPoint.x, _middelPoint.y + Screen.height / 2 - _trapButtonsize, _trapButtonsize, _trapButtonsize), _Traps[_hotBar[2 ]]))
            {
                _hotBar[2] = _selectedtrap;
                _selectedtrap = 0;
            }
            if (GUI.Button(new Rect(_middelPoint.x + _trapButtonsize, _middelPoint.y + Screen.height / 2 - _trapButtonsize, _trapButtonsize, _trapButtonsize), _Traps[_hotBar[3]]))
            {
                _hotBar[3] = _selectedtrap;
                _selectedtrap = 0;
            }
            if (GUI.Button(new Rect(_middelPoint.x - _trapButtonsize, _middelPoint.y + Screen.height / 2 - _trapButtonsize, _trapButtonsize, _trapButtonsize), _Traps[_hotBar[1]]))
            {
                _hotBar[1] = _selectedtrap;
                _selectedtrap = 0;
            }
            if (GUI.Button(new Rect(_middelPoint.x - (_trapButtonsize * 2), _middelPoint.y + Screen.height / 2 - _trapButtonsize, _trapButtonsize, _trapButtonsize), _Traps[_hotBar[0]]))
            {
                _hotBar[0] = _selectedtrap;
                _selectedtrap = 0;
            }
            //selctionbuttions
            if (GUI.Button(new Rect(_middelPoint.x - (_trapButtonsize / 2), _middelPoint.y, _trapButtonsize, _trapButtonsize), _Traps[_firstTrap +1]))
            {
                _selectedtrap = _firstTrap + 1;
            }
            if (GUI.Button(new Rect(_middelPoint.x + _trapButtonsize - (_trapButtonsize / 2), _middelPoint.y, _trapButtonsize, _trapButtonsize), _Traps[_firstTrap+2]))
            {
                _selectedtrap = _firstTrap + 2;
            }
            if (GUI.Button(new Rect(_middelPoint.x - _trapButtonsize - (_trapButtonsize / 2), _middelPoint.y, _trapButtonsize, _trapButtonsize), _Traps[_firstTrap]))
            {
                _selectedtrap = _firstTrap;
            }
            //selction arrows
            if (GUI.Button(new Rect(_middelPoint.x + (_trapButtonsize * 2) - (_trapButtonsize / 2), _middelPoint.y + _trapButtonsize / 4, _trapButtonsize / 2, _trapButtonsize / 2), ""))
            {
                if (_firstTrap +3 < _Traps.Length)
                {
                    _firstTrap++;
                }
            }
            if (GUI.Button(new Rect(_middelPoint.x - (_trapButtonsize * 1.5f) - (_trapButtonsize / 2), _middelPoint.y + _trapButtonsize/4, _trapButtonsize / 2, _trapButtonsize / 2), ""))
            {
                if (_firstTrap-1 > 0)
                {
                    _firstTrap--;
                }
            }
            //mouse
            if (_selectedtrap != 0)
            {
                GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, _trapButtonsize, _trapButtonsize), _Traps[_selectedtrap]);
            }
        }
    }
    void Update()
    {
        if (_menus[0].activeSelf == false && _menus[1].activeSelf == false)
        {
            if(Input.anyKey)
            {
                _menus[0].SetActive(true);
                _menus[1].SetActive(false);
                _menus[2].SetActive(false);
                _menus[3].SetActive(false);
            }
            
        }
    }
}
