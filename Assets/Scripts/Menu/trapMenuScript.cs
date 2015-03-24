using UnityEngine;
using System.Collections;
//using System.UI;

public class trapMenuScript : MonoBehaviour {
    private Vector3         _middelPoint;
    private Vector2         _buttonSize;
    private float           _trapButtonsize;
    [SerializeField]
    private Texture2D[]     _Traps;
    private int[]           _hotBar;
    private int             _selectedtrap;
	[SerializeField]
	private GameObject		_controllerCursor;
    void Start()
    {
		Screen.lockCursor = false;
		Screen.showCursor = true;
        _middelPoint = Camera.main.WorldToScreenPoint(GameObject.Find("middelPoint").transform.position);
        _buttonSize = new Vector2(0.1f * Screen.width, .1f * Screen.height);
        _trapButtonsize = 0.1f * Screen.height;
        _hotBar = new int[_Traps.Length -1];
    }

	void OnGUI()
    {
            //menu buttons
        if (GUI.Button(new Rect(_middelPoint.x - (_buttonSize.x * 4.2f), _middelPoint.y + (_buttonSize.y * 3.8f), _buttonSize.x * 1.3f, _buttonSize.y * 1.2f), "", GUIStyle.none))
            {
                Application.LoadLevel("Menu");
            }
        if (GUI.Button(new Rect(_middelPoint.x + (_buttonSize.x * 3), _middelPoint.y + (_buttonSize.y * 3.8f), _buttonSize.x * 1.3f, _buttonSize.y * 1.2f), "Play"))
            {
                for(int c = 0;c < _hotBar.Length;c++)
                {
                    PlayerPrefs.SetInt("hotBar"+c, _hotBar[c] - 1);
                }
                Application.LoadLevel(PlayerPrefs.GetString("Level"));
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
            if (GUI.Button(new Rect(_middelPoint.x - (_trapButtonsize * 7.3f), _middelPoint.y, _trapButtonsize * 2.3f, _trapButtonsize * 2.3f), "", GUIStyle.none))
            {
                _selectedtrap = 1;
            }
            if (GUI.Button(new Rect(_middelPoint.x + _trapButtonsize + (_trapButtonsize * 1.7f), _middelPoint.y, _trapButtonsize * 2.3f, _trapButtonsize * 2.3f),"",GUIStyle.none))
            {
                _selectedtrap = 2;
            }
            if (GUI.Button(new Rect(_middelPoint.x - _trapButtonsize - (_trapButtonsize * 3.8f), _middelPoint.y, _trapButtonsize * 2.3f, _trapButtonsize * 2.3f), "", GUIStyle.none))
            {
                _selectedtrap = 3;
            }
            if (GUI.Button(new Rect(_middelPoint.x - _trapButtonsize + (_trapButtonsize * 1.1f ), _middelPoint.y, _trapButtonsize * 2.3f, _trapButtonsize * 2.3f),"",GUIStyle.none))
            {
                _selectedtrap = 4;
            }
            if (GUI.Button(new Rect(_middelPoint.x - _trapButtonsize - (_trapButtonsize * 1.4f), _middelPoint.y, _trapButtonsize * 2.3f, _trapButtonsize * 2.3f), "", GUIStyle.none))
            {
                _selectedtrap = 5;
            }
            if (GUI.Button(new Rect(_middelPoint.x - _trapButtonsize + (_trapButtonsize * 6.1f), _middelPoint.y, _trapButtonsize * 2.3f, _trapButtonsize * 2.3f), "", GUIStyle.none))
            {
                _selectedtrap = 6;
            }
            //mouse
            if (_selectedtrap != 0)
            {
                GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, _trapButtonsize, _trapButtonsize), _Traps[_selectedtrap]);
            }
       
    }
    void Update()
    {
		/*
		if (GUI.Button(new Rect(_middelPoint.x - (_buttonSize.x * 4.2f), _middelPoint.y + (_buttonSize.y * 3.8f), _buttonSize.x * 1.3f, _buttonSize.y * 1.2f), "", GUIStyle.none))
		{
			Application.LoadLevel("Menu");
		}
		if (GUI.Button(new Rect(_middelPoint.x + (_buttonSize.x * 3), _middelPoint.y + (_buttonSize.y * 3.8f), _buttonSize.x * 1.3f, _buttonSize.y * 1.2f), "Play"))
		{
			for(int c = 0;c < _hotBar.Length;c++)
			{
				PlayerPrefs.SetInt("hotBar"+c, _hotBar[c] - 1);
			}
			Application.LoadLevel(PlayerPrefs.GetString("Level"));
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
		if (GUI.Button(new Rect(_middelPoint.x - (_trapButtonsize * 7.3f), _middelPoint.y, _trapButtonsize * 2.3f, _trapButtonsize * 2.3f), "", GUIStyle.none))
		{
			_selectedtrap = 1;
		}
		if (GUI.Button(new Rect(_middelPoint.x + _trapButtonsize + (_trapButtonsize * 1.7f), _middelPoint.y, _trapButtonsize * 2.3f, _trapButtonsize * 2.3f),"",GUIStyle.none))
		{
			_selectedtrap = 2;
		}
		if (GUI.Button(new Rect(_middelPoint.x - _trapButtonsize - (_trapButtonsize * 3.8f), _middelPoint.y, _trapButtonsize * 2.3f, _trapButtonsize * 2.3f), "", GUIStyle.none))
		{
			_selectedtrap = 3;
		}
		if (GUI.Button(new Rect(_middelPoint.x - _trapButtonsize + (_trapButtonsize * 1.1f ), _middelPoint.y, _trapButtonsize * 2.3f, _trapButtonsize * 2.3f),"",GUIStyle.none))
		{
			_selectedtrap = 4;
		}
		if (GUI.Button(new Rect(_middelPoint.x - _trapButtonsize - (_trapButtonsize * 1.4f), _middelPoint.y, _trapButtonsize * 2.3f, _trapButtonsize * 2.3f), "", GUIStyle.none))
		{
			_selectedtrap = 5;
		}
		if (GUI.Button(new Rect(_middelPoint.x - _trapButtonsize + (_trapButtonsize * 6.1f), _middelPoint.y, _trapButtonsize * 2.3f, _trapButtonsize * 2.3f), "", GUIStyle.none))
		{
			_selectedtrap = 6;
		}
		//mouse
		if (_selectedtrap != 0)
		{
			_controllerCursor.GetComponent<Image>().
		} */
    }
}
