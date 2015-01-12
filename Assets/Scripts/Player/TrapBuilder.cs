﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TrapBuilder : MonoBehaviour {

	public GameObject[] arrowTrap = new GameObject[0];
	public GameObject[] buildArrowTrap = new GameObject[0];
	public float[] trapPrices = new float[0];
	public bool isBuilding = false;
	public GUIStyle textStyle;
	public Text costText;

	private float _spawnY;
	private int _trapToBuild = -1;
	private GameObject _currentTrap;
	private List<GameObject> _buildTraps = new List<GameObject>();
	private List<GameObject> _allTraps = new List<GameObject>();

	void Start()
	{
		GetTraps();
		int trapcounter = 0;
		foreach(GameObject trap in _buildTraps)
		{
			if(trap == null)
			{
				trapcounter++;
			}
		}
		if(trapcounter >= 4)
		{
			Debug.LogError("No buildtraps assigned!");
		}
	}

	void Update () {
        KeyInput();
		if(Input.GetMouseButtonDown(0) && isBuilding)
		{
			if(_currentTrap != null)
			{
				if(_currentTrap.GetComponent<BuildTrapBehavior>().buildAble)
				{
					SpawnTrap();
				} else {
					ClearTrap();
				}
			}
			else
			{
				ClearTrap();
			}
		}
		if(isBuilding)
		{
			CheckWhereToBuild();
		}
	}
	private void KeyInput()
	{
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
            BuildTrap(0);
		} else if(Input.GetKeyDown(KeyCode.Alpha2)) 
		{
            BuildTrap(1);
		} else if(Input.GetKeyDown(KeyCode.Alpha3)) 
		{
            BuildTrap(2);
		} else if(Input.GetKeyDown(KeyCode.Alpha4)) 
		{
            BuildTrap(3);
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
			if(hit.transform.tag == "Wall" && _buildTraps[_trapToBuild].transform.tag == "WallTrap")
			{
				if(hit.distance <= 10f) //is in range of wall
				{
					if(_currentTrap == null)
					{
						_currentTrap = Instantiate(_buildTraps[_trapToBuild], Vector3.zero, Quaternion.identity) as GameObject;
					}
					ChangeTrapPos(hit);
				} else if(_currentTrap != null){
					Destroy(_currentTrap.gameObject);
				}
			} 
			else if(hit.transform.tag == "Floor" && _buildTraps[_trapToBuild].transform.tag == "FloorTrap")
			{
				if(hit.distance <= 10f) //is in range of wall
				{
					if(_currentTrap == null)
					{
						_currentTrap = Instantiate(_buildTraps[_trapToBuild], Vector3.zero, Quaternion.identity) as GameObject;
					}
					ChangeTrapPos(hit);
				} else if(_currentTrap != null)
				{
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
		Vector3 newRot = hit.transform.eulerAngles;
		newRot.y -= 90;
		//change position and rotation to respective wall direction
		_currentTrap.transform.position = newPos;
		_currentTrap.transform.eulerAngles = newRot;
	}
	//spawn new trap
	private void SpawnTrap()
	{
		if(trapPrices[_trapToBuild] <= this.GetComponent<GoldController>().GetGold())
		{
			GetComponent<GoldController>().SubtractGold(trapPrices[_trapToBuild]);
			GameObject newTrap = Instantiate(_allTraps[_trapToBuild], _currentTrap.transform.position,_currentTrap.transform.rotation) as GameObject;
			GameObject hierachyTraps = GameObject.FindGameObjectWithTag("AllTraps");
			newTrap.transform.parent = hierachyTraps.transform;
			Destroy(_currentTrap.gameObject);
			isBuilding = false;
			_trapToBuild = -1;
		} else {
			Destroy(_currentTrap.gameObject);
			isBuilding = false;
			_trapToBuild = -1;
		}
	}
	//clear building
	private void ClearTrap()
	{
		if(_currentTrap != null)
		{
			Destroy(_currentTrap.gameObject);
		}
		isBuilding = false;
		_trapToBuild = -1;
	}
	//check wich trap to build
	private void BuildTrap(int trapSort)
	{
		if(_buildTraps[trapSort] != null)
		{
			isBuilding = true;
			_trapToBuild = trapSort;
			costText.text = "Cost: " + trapPrices[_trapToBuild];
			if(_currentTrap != null)
			{
				Destroy(_currentTrap.gameObject);
				_currentTrap = Instantiate(_buildTraps[_trapToBuild], Vector3.zero, Quaternion.identity) as GameObject;
			}
		} else {
			ClearTrap();
		}
	}
	//get all traps from playerprefs
	void GetTraps()
	{
		for(int i = 0; i < 4; i++)
		{
			int trapId = PlayerPrefs.GetInt("hotBar"+i);
			_allTraps.Add(arrowTrap[trapId]);
			_buildTraps.Add(buildArrowTrap[trapId]);
		}
	}
}