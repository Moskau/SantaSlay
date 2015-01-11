using UnityEngine;
using System.Collections;

public class GenerateHouses : MonoBehaviour {

	public Transform[] house;
	public Transform road;
	public Transform mainRoad;
	public Transform ground;

	//private Random rand;
	// Use this for initialization
	void Start () {
		//rand = new Random();
		float centerY = 0;
		float centerX = -135*2F;
		//level1
		InstantiateBlock(centerX, centerY + 51,false,true,true);
		InstantiateBlock(centerX, centerY - 51,true,false,false);
		centerX += 135F;
		InstantiateBlock(centerX, centerY + 51,false,true,false);
		InstantiateBlock(centerX, centerY - 51,true,false,false);
		centerX += 135F;
		InstantiateBlock(centerX, centerY + 51,false,true,true);
		InstantiateBlock(centerX, centerY - 51,true,false,false);
		centerX += 135F;
		InstantiateBlock(centerX, centerY + 51,false,true,false);
		InstantiateBlock(centerX, centerY - 51,true,false,true);
		centerX += 135F;
		InstantiateBlock(centerX, centerY + 51,false,true,false);
		InstantiateBlock(centerX, centerY - 51,true,false,false);
		centerX += 135F;
		InstantiateBlock(centerX, centerY + 51,false,true,false);
		InstantiateBlock(centerX, centerY - 51,true,false,false);

		centerY = 102*2;
		centerX = -135*2F;
		//InstantiateBlock(centerX, centerY + 51,false,false,false);
		InstantiateBlock(centerX, centerY - 51,false,false,false);
		centerX += 135F;
		//InstantiateBlock(centerX, centerY + 51,false,false,false);
		InstantiateBlock(centerX, centerY - 51,false,false,false);
		centerX += 135F;
		//InstantiateBlock(centerX, centerY + 51,false,false,false);
		InstantiateBlock(centerX, centerY - 51,false,false,false);
		centerX += 135F;
		//InstantiateBlock(centerX, centerY + 51,false,false,false);
		InstantiateBlock(centerX, centerY - 51,false,false,false);
		centerX += 135F;
		//InstantiateBlock(centerX, centerY + 51,false,false,false);
		InstantiateBlock(centerX, centerY - 51,false,false,true);
		centerX += 135F;
		//InstantiateBlock(centerX, centerY + 51,false,false,false);
		InstantiateBlock(centerX, centerY - 51,false,false,false);

		centerY = -102*2;
		centerX = -135*2F;
		InstantiateBlock(centerX, centerY + 51,false,false,false);
		//InstantiateBlock(centerX, centerY - 51,false,false,false);
		centerX += 135F;
		InstantiateBlock(centerX, centerY + 51,false,false,false);
		//InstantiateBlock(centerX, centerY - 51,false,false,false);
		centerX += 135F;
		InstantiateBlock(centerX, centerY + 51,false,false,false);
		//InstantiateBlock(centerX, centerY - 51,false,false,false);
		centerX += 135F;
		InstantiateBlock(centerX, centerY + 51,false,false,false);
		//InstantiateBlock(centerX, centerY - 51,false,false,false);
		centerX += 135F;
		InstantiateBlock(centerX, centerY + 51,false,false,false);
		//InstantiateBlock(centerX, centerY - 51,false,false,false);
		centerX += 135F;
		InstantiateBlock(centerX, centerY + 51,false,false,true);
		//InstantiateBlock(centerX, centerY - 51,false,false,false);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void InstantiateBlock(float x0, float z0, bool leftRoad, bool rightRoad, bool includeHouses)
	{
		Vector3 rotate180 = new Vector3(0,180,0);
		if(includeHouses){
			for(int x = -1; x<= 1; x++){
				for (int z = -2; z <= 2; z++) {
					Instantiate(GetRandomHouseType(), new Vector3(x0 + x*45+4, 0, z0 + z*17+8), Quaternion.identity);
					Instantiate(road, new Vector3(x0 + x*45, 0, z0), Quaternion.identity);
					Instantiate(GetRandomHouseType(), new Vector3(x0 + x*45-4, 0, z0 + z*17-8), Quaternion.Euler(rotate180));
				}
			}
		}
		if(leftRoad){
			Instantiate(mainRoad, new Vector3(x0 + 0, 0, z0 + 48), Quaternion.identity);
		}
		if(rightRoad){
			Instantiate(mainRoad, new Vector3(x0 + 0, 0, z0 - 48), Quaternion.identity);
		}
		Instantiate(ground, new Vector3(x0, -2, z0), Quaternion.identity);
	}

	Transform GetRandomHouseType()
	{
		return house[Random.Range(0,house.Length)];
	}
}
