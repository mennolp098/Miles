﻿
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrapBuilder : MonoBehaviour {

	public GameObject[] arrowTrap = new GameObject[0];
	public GameObject[] buildArrowTrap = new GameObject[0];

	public GUIStyle textStyle;

	private float spawnY;
	private int _trapToBuild = -1;
	private bool _isBuilding = false;
	private GameObject _currentTrap;
	private List<GameObject> buildTraps = new List<GameObject>();
	private List<GameObject> allTraps = new List<GameObject>();

    [SerializeField]
    private Texture2D[] UIButtons;
    [SerializeField]
    private Texture2D UiBack;

	void Start()
	{
		GetTraps();
		if(buildTraps[0] == null)
		{
			Debug.LogError("No buildtraps assigned!");
		}
		if(allTraps[0] == null)
		{
			Debug.LogError("No traps assigned!");
		}
        for (int i = 0; i <= 3; i++)
        {
            Debug.Log(PlayerPrefs.GetInt("hotBar" + i));
        }
	}

	void Update () {
            KeyInput();
		if(Input.GetMouseButtonDown(0) && _isBuilding)
		{
			if(_currentTrap != null)
			{
				if(_currentTrap.GetComponent<BuildTrapBehavior>().buildAble)
				{
					SpawnTrap();
				}
			}
		}
		if(_isBuilding)
		{
			CheckWhereToBuild();
		}
	}
	private void KeyInput()
	{
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
            BuildTrap(PlayerPrefs.GetInt("hotBar0"));
		} else if(Input.GetKeyDown(KeyCode.Alpha2)) 
		{
            BuildTrap(PlayerPrefs.GetInt("hotBar1"));
		} else if(Input.GetKeyDown(KeyCode.Alpha3)) 
		{
            BuildTrap(PlayerPrefs.GetInt("hotBar2"));
		} else if(Input.GetKeyDown(KeyCode.Alpha4)) 
		{
            BuildTrap(PlayerPrefs.GetInt("hotBar3"));
		}
	}
	private void CheckWhereToBuild()
	{
		RaycastHit hit;
		Ray ray;

		ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width/2,Screen.height/2));
		ray.origin = Camera.main.transform.position;
		if(Physics.Raycast(ray, out hit))
		{
			if(hit.transform.tag == "Wall")
			{
				if(hit.distance <= 10f) //is in range of wall
				{
					if(_currentTrap == null)
					{
						_currentTrap = Instantiate(buildTraps[_trapToBuild], Vector3.zero, Quaternion.identity) as GameObject;
					}
					ChangeTrapPos(hit);
				} else if(_currentTrap != null){
					Destroy(_currentTrap.gameObject);
				}
			} 
			else if(_currentTrap != null) 
			{
				Destroy(_currentTrap.gameObject);
			}
		}
	}
	private void ChangeTrapPos(RaycastHit hit)
	{
		//check hit position for the wall
		Vector3 newPos = hit.point;
		spawnY = 2.5f + this.transform.position.y; 
		newPos.y = spawnY;
		Vector3 newRot = hit.transform.eulerAngles;
		newRot.y -= 90;
		//change position and rotation to respective wall direction
		_currentTrap.transform.position = newPos;
		_currentTrap.transform.eulerAngles = newRot;
	}
	//spawn new trap
	private void SpawnTrap()
	{
		Vector3 spawnPos = _currentTrap.transform.position;
		spawnPos.y = spawnY;
		GameObject newTrap = Instantiate(allTraps[_trapToBuild], _currentTrap.transform.position,_currentTrap.transform.rotation) as GameObject;
		GameObject hierachyTraps = GameObject.FindGameObjectWithTag("AllTraps");
		newTrap.transform.parent = hierachyTraps.transform;
		Destroy(_currentTrap.gameObject);
		_isBuilding = false;
		_trapToBuild = -1;
	}
	//clear building
	private void ClearTrap()
	{
		if(_currentTrap != null)
		{
			Destroy(_currentTrap.gameObject);
			_isBuilding = false;
			_trapToBuild = -1;

		}
	}
	//check wich trap to build
	private void BuildTrap(int trapSort)
	{
		_isBuilding = true;
		_trapToBuild = trapSort;
		if(_currentTrap != null)
		{
			Destroy(_currentTrap.gameObject);
			_currentTrap = Instantiate(buildTraps[_trapToBuild], Vector3.zero, Quaternion.identity) as GameObject;
		}
	}
	//get all traps from playerprefs
	void GetTraps()
	{
		for(int i = 0; i < 4; i++)
		{
			int trapId = PlayerPrefs.GetInt("trap-" + i);
			allTraps.Add(arrowTrap[trapId]);
			buildTraps.Add(buildArrowTrap[trapId]);
		}
	}
	/*
	//UI
    void OnGUI()
    {
        GUI.DrawTexture(new Rect(Screen.width /2 - 150, Screen.height - UiBack.height /2, UiBack.width/2, UiBack.height/2), UiBack);
        GUI.DrawTexture(new Rect(Screen.width /2 - 150, Screen.height - UiBack.height /2, UiBack.width / 2, UiBack.height / 2), UiBack);
        if (_trapToBuild >= 0)
        {
            GUI.DrawTexture(new Rect(Screen.width / 2 - 150 + 17 + (45 * _trapToBuild), Screen.height - UiBack.height / 2 + 13, 40, 30), UIButtons[_trapToBuild]);
        }
    } */
}