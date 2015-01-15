﻿using UnityEngine;
using System.Collections;

public class menuScript : MonoBehaviour {
    private Vector3         _middelPoint;
    private Vector2         _buttonSize;
    [SerializeField]
    private GameObject[]    _menus;
    void Start()
    {
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
                _menus[2].SetActive(true);
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
    }
    public void selectLevel(int level)
    {
        PlayerPrefs.SetString("Level", "Level0"+level);
        Application.LoadLevel("Traps");
    }
    void Update()
    {
        if (_menus[0].activeSelf == true)
        {
            if(Input.anyKey)
            {
                _menus[0].SetActive(false);
                _menus[1].SetActive(true);
            }
            
        }
    }
}
