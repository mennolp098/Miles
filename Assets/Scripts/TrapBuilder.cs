
using UnityEngine;
using System.Collections;

public class TrapBuilder : MonoBehaviour {


	public GUIStyle textStyle;
	
	private int _trapToBuild = -1;
	private bool _isBuilding = false;
	private GameObject _currentTrap;
	private GameObject[] buildTraps = new GameObject[3];
	private GameObject[] allTraps = new GameObject[3];

    [SerializeField]
    private Texture2D[] UIButtons;
    [SerializeField]
    private Texture2D UiBack;

	void Start()
	{
		if(buildTraps[0] == null)
		{
			Debug.LogError("No buildtraps assigned!");
		}
		if(allTraps[0] == null)
		{
			Debug.LogError("No traps assigned!");
		}
	}

	void Update () {
		if(Input.GetMouseButtonDown(0) && _isBuilding)
		{
			Vector3 spawnPos = _currentTrap.transform.position;
			spawnPos.y = 0.5f;
			GameObject newTrap = Instantiate(allTraps[_trapToBuild], _currentTrap.transform.position,_currentTrap.transform.rotation) as GameObject;
			GameObject hierachyTraps = GameObject.FindGameObjectWithTag("AllTraps");
			newTrap.transform.parent = hierachyTraps.transform;
			Destroy(_currentTrap.gameObject);
			_isBuilding = false;
       		_trapToBuild = -1;
		}
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
			ClearTrap();
		}
		if(_isBuilding)
		{
			RaycastHit hit;
			Ray ray;
			
			ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width/2,Screen.height/2));
			ray.origin = Camera.main.transform.position;
			if(Physics.Raycast(ray, out hit))
			{
				if(hit.transform.tag == "Wall")
				{
					if(hit.distance <= 3f)
					{
						if(_currentTrap != null)
						{
							Vector3 newPos = hit.point;
							newPos.y = 3f;
							Vector3 newRot = hit.transform.eulerAngles;
							if(hit.transform.name == "leftWall")
							{
								newRot.y = 270;
							} else if(hit.transform.name == "rightWall")
							{
								newRot.y = 90;
							} else if(hit.transform.name == "frontWall")
							{
								newRot.y = 0;
							} else {
								newRot.y = 180;
							}
							//newRot.y = 
							_currentTrap.transform.position = newPos;
							_currentTrap.transform.eulerAngles = newRot;
						} else {
							_currentTrap = Instantiate(buildTraps[_trapToBuild], Vector3.zero, Quaternion.identity) as GameObject;
						}
					} else {
						if(_currentTrap != null)
						{
							Destroy(_currentTrap.gameObject);
						}
					}
				} else {
					if(_currentTrap != null)
					{
						Destroy(_currentTrap.gameObject);
					}
				}
			}
		}
	}
	private void ClearTrap()
	{
		if(_currentTrap != null)
		{
			Destroy(_currentTrap.gameObject);
			_isBuilding = false;
			_trapToBuild = -1;
		}
	}
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
    void OnGUI()
    {
        GUI.DrawTexture(new Rect(Screen.width /2 - 150, Screen.height - UiBack.height /2, UiBack.width/2, UiBack.height/2), UiBack);
        GUI.DrawTexture(new Rect(Screen.width /2 - 150, Screen.height - UiBack.height /2, UiBack.width / 2, UiBack.height / 2), UiBack);
        if (_trapToBuild >= 0)
        {
            GUI.DrawTexture(new Rect(Screen.width / 2 - 150 + 17 + (45 * _trapToBuild), Screen.height - UiBack.height / 2 + 13, 40, 30), UIButtons[_trapToBuild]);
        }
    }
}