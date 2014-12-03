
using UnityEngine;
using System.Collections;

public class TowerBuilder : MonoBehaviour {

	public GameObject[] buildTowers = new GameObject[0];
	public Transform spawnPoint;
	public GameObject[] allTowers = new GameObject[0];
	public GUIStyle textStyle;
	public GameObject clash;

	private int _towerToBuild = -1;
	private bool _isBuilding = false;
	private GameObject _currentTower;
	
    [SerializeField]
    private Texture2D[] UIButtons;
    [SerializeField]
    private Texture2D UiBack;

	void Update () {
		if(Input.GetMouseButtonDown(0) && _isBuilding)
		{
			if(_currentTower.GetComponent<BuildTowerBehavior>().buildAble)
			{
				Vector3 spawnPos = _currentTower.transform.position;
				spawnPos.y = 0.5f;
				GameObject newTower = Instantiate(allTowers[_towerToBuild], spawnPos,_currentTower.transform.rotation) as GameObject;
				GameObject hierachyTowers = GameObject.FindGameObjectWithTag("AllTowers");
				newTower.transform.parent = hierachyTowers.transform;
				Destroy(_currentTower.gameObject);
				_isBuilding = false;
                _towerToBuild = -1;
			}
		}
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			BuildTower(0);
		} else if(Input.GetKeyDown(KeyCode.Alpha2)) 
		{
			BuildTower(1);
		} else if(Input.GetKeyDown(KeyCode.Alpha3)) 
		{
			BuildTower(2);
		} else if(Input.GetKeyDown(KeyCode.Alpha4)) 
		{
			ClearTower();
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
						if(_currentTower != null)
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
							_currentTower.transform.position = newPos;
							_currentTower.transform.eulerAngles = newRot;
						} else {
							_currentTower = Instantiate(buildTowers[_towerToBuild], Vector3.zero, Quaternion.identity) as GameObject;
						}
					} else {
						if(_currentTower != null)
						{
							Destroy(_currentTower.gameObject);
						}
					}
				} else {
					if(_currentTower != null)
					{
						Destroy(_currentTower.gameObject);
					}
				}
			}
		}
	}
	private void ClearTower()
	{
		if(_currentTower != null)
		{
			Destroy(_currentTower.gameObject);
			_isBuilding = false;
			_towerToBuild = -1;
		}
	}
	private void BuildTower(int towerSort)
	{
		_isBuilding = true;
		_towerToBuild = towerSort;
	}
    void OnGUI()
    {
        GUI.DrawTexture(new Rect(Screen.width /2 - 150, Screen.height - UiBack.height /2, UiBack.width/2, UiBack.height/2), UiBack);
		float totalMaterials = GetComponentInParent<MaterialHandler>().GetMaterials();
        GUI.DrawTexture(new Rect(Screen.width /2 - 150, Screen.height - UiBack.height /2, UiBack.width / 2, UiBack.height / 2), UiBack);
		GUI.TextField(new Rect(Screen.width/2 + 50, Screen.height + UiBack.height - 150, 50,50), "" + totalMaterials, textStyle);
        if (_towerToBuild >= 0)
        {
            GUI.DrawTexture(new Rect(Screen.width / 2 - 150 + 17 + (45 * _towerToBuild), Screen.height - UiBack.height / 2 + 13, 40, 30), UIButtons[_towerToBuild]);
        }
    }
}