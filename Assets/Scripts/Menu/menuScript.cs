using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class menuScript : MonoBehaviour {
    private Vector3         _middelPoint;
    private Vector2         _buttonSize;
    [SerializeField]
    private GameObject[]    _menus;
	[SerializeField]
	private GameObject		_controllerMouse;
	[SerializeField]
	private GameObject		_level01;
	[SerializeField]
	private GameObject		_level02;
	private float			_clickCooldown;
	private float			_extraTime = 0.5f;
    void Start()
    {
		if(Input.GetJoystickNames().Length >= 1)
		{
			_controllerMouse.SetActive(true);
		}
        _middelPoint = Camera.main.WorldToScreenPoint(GameObject.Find("middelPoint").transform.position);
        _buttonSize = new Vector2(0.1f * Screen.width, .1f * Screen.height);
        for(int i = 1; i < 6;i++)
        {
            _menus[i].SetActive(false);
        }
    }
	void OnGUI()
    {
        if (_menus[1].activeSelf == true)
        {
            if (GUI.Button(new Rect(_middelPoint.x - (_buttonSize.x * 4.2f), _middelPoint.y, _buttonSize.x * 2, _buttonSize.y * 2), "", GUIStyle.none))
            {
                _menus[1].SetActive(false);
                _menus[6].SetActive(true);
            }
            if (GUI.Button(new Rect(_middelPoint.x - (_buttonSize.x * 4.2f), _middelPoint.y + (_buttonSize.y * 2), _buttonSize.x * 2, _buttonSize.y), "", GUIStyle.none))
            {
                _menus[1].SetActive(false);
                _menus[3].SetActive(true);
            }
            if (GUI.Button(new Rect(_middelPoint.x - (_buttonSize.x * 4.2f), _middelPoint.y + (_buttonSize.y * 3), _buttonSize.x * 1.3f, _buttonSize.y), "", GUIStyle.none))
            {
                _menus[1].SetActive(false);
                _menus[5].SetActive(true);
            }
        }
        if(_menus[0].activeSelf == false && _menus[1].activeSelf == false)
        {
            if (GUI.Button(new Rect(_middelPoint.x - (_buttonSize.x * 4.2f), _middelPoint.y + (_buttonSize.y * 2.7f), _buttonSize.x * 1.3f, _buttonSize.y * 1.2f), "", GUIStyle.none))
            {
                if (_menus[4].activeSelf == false)
                {
                    _menus[1].SetActive(true);
                    _menus[2].SetActive(false);
                    _menus[3].SetActive(false);
                    _menus[4].SetActive(false);
                    _menus[5].SetActive(false);
					
                }
                if(_menus[4].activeSelf == true)
                {
                    _menus[3].SetActive(true);
                    _menus[4].SetActive(false);
					
                }
            }
            if (_menus[3].activeSelf == true)
            {
                if (GUI.Button(new Rect(_middelPoint.x + (_buttonSize.x * 3), _middelPoint.y + (_buttonSize.y * 2.7f), _buttonSize.x * 1.3f, _buttonSize.y * 1.2f), "", GUIStyle.none))
                {
                    _menus[4].SetActive(true);
                    _menus[3].SetActive(false);
                }
            }
        }
        if (_menus[6].activeSelf == true)
        {
			if (GUI.Button(new Rect(_middelPoint.x  -(_buttonSize.x /3.5f), _middelPoint.y - (_buttonSize.x/2) , _buttonSize.x , _buttonSize.x), "", GUIStyle.none))
            {
                PlayerPrefs.SetInt("multiplayer", 0);
                _menus[2].SetActive(true);
                _menus[6].SetActive(false);
            }
			if (GUI.Button(new Rect(_middelPoint.x -(_buttonSize.x/1.5f), _middelPoint.y + (_buttonSize.y * 2.5f), _buttonSize.x + (_buttonSize.x / 2), _buttonSize.y + (_buttonSize.y / 2)), "", GUIStyle.none))
            {
                PlayerPrefs.SetInt("multiplayer", 1);
                _menus[2].SetActive(true);
                _menus[6].SetActive(false);
            }
        }
    }
    public void selectLevel(int level)
    {
        PlayerPrefs.SetString("Level", "Level0"+level);
        Application.LoadLevel("Traps");
    }
    void Update()
    {
		Vector2 cursorPos = new Vector2(_controllerMouse.GetComponent<ControllerMouse>().GetX(),_controllerMouse.GetComponent<ControllerMouse>().GetY());
        if (_menus[0].activeSelf == true)
        {
            if(Input.anyKey)
            {
                _menus[0].SetActive(false);
                _menus[1].SetActive(true);
            }
        }
		if(Input.GetKeyDown(KeyCode.Joystick1Button0))
		{
			if (_menus[1].activeSelf == true)
			{
				if(new Rect(_middelPoint.x - (_buttonSize.x * 4.2f), _middelPoint.y, _buttonSize.x * 2, _buttonSize.y * 2).Contains(cursorPos))
				{
					_menus[1].SetActive(false);
					_menus[6].SetActive(true);
				}
				else if (new Rect(_middelPoint.x - (_buttonSize.x * 4.2f), _middelPoint.y + (_buttonSize.y * 2), _buttonSize.x * 2, _buttonSize.y).Contains(cursorPos))
				{
					_menus[1].SetActive(false);
					_menus[3].SetActive(true);
				}
				else if (new Rect(_middelPoint.x - (_buttonSize.x * 4.2f), _middelPoint.y + (_buttonSize.y * 3), _buttonSize.x * 1.3f, _buttonSize.y).Contains(cursorPos))
				{
					_menus[1].SetActive(false);
					_menus[5].SetActive(true);
				}
			}
			if(_menus[0].activeSelf == false && _menus[1].activeSelf == false)
			{
				if (new Rect(_middelPoint.x - (_buttonSize.x * 4.2f), _middelPoint.y + (_buttonSize.y * 2.7f), _buttonSize.x * 1.3f, _buttonSize.y * 1.2f).Contains(cursorPos))
				{
					if (_menus[4].activeSelf == false)
					{
						_menus[1].SetActive(true);
						_menus[2].SetActive(false);
						_menus[3].SetActive(false);
						_menus[4].SetActive(false);
						_menus[5].SetActive(false);
					}
					if(_menus[4].activeSelf == true)
					{
						_menus[3].SetActive(true);
						_menus[4].SetActive(false);
					}
				}
				if (_menus[3].activeSelf == true)
				{
					if (new Rect(_middelPoint.x + (_buttonSize.x * 3), _middelPoint.y + (_buttonSize.y * 2.7f), _buttonSize.x * 1.3f, _buttonSize.y * 1.2f).Contains(cursorPos))
					{
						_menus[4].SetActive(true);
						_menus[3].SetActive(false);
					}
				}
			}
			if (_menus[6].activeSelf == true)
			{
				if (new Rect(_middelPoint.x  -(_buttonSize.x /3.5f), _middelPoint.y - (_buttonSize.x/2) , _buttonSize.x , _buttonSize.x).Contains(cursorPos))
				{
					PlayerPrefs.SetInt("multiplayer", 0);
					_menus[2].SetActive(true);
					_menus[6].SetActive(false);
				}
				if (new Rect(_middelPoint.x -(_buttonSize.x/1.5f), _middelPoint.y + (_buttonSize.y * 2.5f), _buttonSize.x + (_buttonSize.x / 2), _buttonSize.y + (_buttonSize.y / 2)).Contains(cursorPos))
				{
					PlayerPrefs.SetInt("multiplayer", 1);
					_menus[2].SetActive(true);
					_menus[6].SetActive(false);
				}
			}
		}
    }
}
