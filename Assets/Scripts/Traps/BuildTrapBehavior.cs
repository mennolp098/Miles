using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildTrapBehavior : MonoBehaviour {
	public bool buildAble;

	private bool hitting = false;
	private List<Material> allChildrenMaterials = new List<Material>();
	void Start()
	{
		Renderer[] allChildrenRenderers = GetComponentsInChildren<Renderer>();
		foreach(Renderer renderer in allChildrenRenderers)
		{
			allChildrenMaterials.Add(renderer.material);
		}
	}
	void Update()
	{
		CheckCollision();

		if(!hitting)
		{
			ChangeColor(Color.green);
			buildAble = true;
		} else {
			ChangeColor(Color.red);
			buildAble = false;
		}
	}
	private void CheckCollision()
	{
		Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 2f);
		int wallcounter = 0;
		for (int i = 0; i < hitColliders.Length; i++) {
			string colliderTag = hitColliders[i].transform.tag;
			if(colliderTag != "Wall" && colliderTag != "WallTrap" && colliderTag != "Floor" && colliderTag != "FloorTrap" && colliderTag != "Enemy" && colliderTag != "GoldCoin")
			{
				if(hitColliders[i].isTrigger == false)
				{
					hitting = true;
					break;
				}
			} else {
				hitting = false;
			}
			if(colliderTag == "Wall") {
				wallcounter++;
				if(wallcounter > 2)
				{
					hitting = true;
				}
			} 
		}
	}
	private void ChangeColor(Color color)
	{
		Color newColor = color;
		newColor.a = 0.5f;
		foreach(Material material in allChildrenMaterials)
		{
			material.color = newColor;
		}
	}
}
