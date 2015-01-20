using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TrapBuilder : MonoBehaviour {

	public GameObject[] arrowTrap = new GameObject[0];
	public GameObject[] buildArrowTrap = new GameObject[0];
	public float[] allTrapPrices = new float[0];
	public bool isBuilding = false;
	public GUIStyle textStyle;
	public Camera currentCam;

	protected float _spawnY;
	protected int _trapToBuild = -1;
	protected GameObject _currentTrap;
	protected List<float> _trapPrices = new List<float>();
	protected List<GameObject> _buildTraps = new List<GameObject>();
	protected List<GameObject> _allTraps = new List<GameObject>();
	protected GeneralController _generalController;
	protected Text costText;
	protected KeyCode key01;
	protected KeyCode key02;
	protected KeyCode key03;
	protected KeyCode key04;
	protected virtual void Start()
	{
		_generalController = GameObject.FindGameObjectWithTag("GeneralController").GetComponent<GeneralController>();

		if(GetComponent<PlayerTwo>() == null)
		{
			costText = GameObject.Find ("CostText").GetComponent<Text>();
		} else {
			costText = GameObject.Find ("CostTextTwo").GetComponent<Text>();
		}

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

	protected virtual void Update () {
        KeyInput();
		BuildInput();
		if(isBuilding)
		{
			CheckWhereToBuild();
		}
	}
	protected virtual void BuildInput()
	{
		//here comes buildinput
	}
	protected void KeyInput()
	{
		if(Input.GetKeyDown(key01))
		{
            BuildTrap(0);
		} else if(Input.GetKeyDown(key02)) 
		{
            BuildTrap(1);
		} else if(Input.GetKeyDown(key03)) 
		{
            BuildTrap(2);
		} else if(Input.GetKeyDown(key04)) 
		{
            BuildTrap(3);
		}
	}
	protected void CheckWhereToBuild()
	{
		RaycastHit hit;
		Ray ray;

		ray = currentCam.ScreenPointToRay(new Vector2(currentCam.pixelWidth/2 + currentCam.rect.x * Screen.width,currentCam.pixelHeight/2));
		ray.origin = currentCam.transform.position;
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
	protected void ChangeTrapPos(RaycastHit hit)
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
	protected void SpawnTrap()
	{
		if(_trapPrices[_trapToBuild] <= _generalController.GetGold())
		{
			_generalController.SubtractGold(_trapPrices[_trapToBuild]);
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
	public void ClearTrap()
	{
		if(_currentTrap != null)
		{
			Destroy(_currentTrap.gameObject);
		}
		isBuilding = false;
		_trapToBuild = -1;
	}
	//check wich trap to build
	protected void BuildTrap(int trapSort)
	{
		if(_buildTraps[trapSort] != null && !GetComponent<PlayerController>().death)
		{
			isBuilding = true;
			_trapToBuild = trapSort;
			costText.text = "Cost: " + _trapPrices[_trapToBuild];
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
			_trapPrices.Add(allTrapPrices[trapId]);
		}
	}
}