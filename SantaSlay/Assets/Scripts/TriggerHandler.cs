using UnityEngine;
using System.Collections;



public class TriggerHandler : MonoBehaviour {

	public GameObject thisHouse;
	public Transform houseExplosion;

	public enum TargetType{
		present,
		bomb
	}

	public TargetType targetType;

	private ScoreController scoreController;

	bool triggered = false;

	// Use this for initialization
	void Start () {
		scoreController = GameObject.Find("MyCanvas").GetComponent<ScoreController>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider projectile)
	{
		if(projectile.GetType() == typeof(SphereCollider))
		{
			//bomb!!!
			Vector3 houseTransform = thisHouse.transform.position;
			Quaternion houseRotatoin = thisHouse.transform.rotation;
			switch(targetType)
			{
			case TargetType.present:
				//present was wanted

				//lose points!
				scoreController.addScore(-30);

				//destroy house!!
				GameObject.Destroy(thisHouse);
				Instantiate(houseExplosion, houseTransform, houseRotatoin);
				break;
			case TargetType.bomb:
				//bomb was wanted

				//get points!
				scoreController.addScore(10);

				//destroy house
				GameObject.Destroy(thisHouse);
				Instantiate(houseExplosion, houseTransform, houseRotatoin);
				break;
			}
		}
	}

	void OnTriggerStay(Collider projectile)
	{
		if(!triggered && projectile.rigidbody.velocity.magnitude <= 0.01)
		{
			if(projectile.GetType() == typeof(BoxCollider) && targetType == TargetType.present)
			{
				//present was correctly received!
				triggered = true;
				//add points
				scoreController.addScore(10);
				//remove trigger
				GameObject.Destroy(gameObject);
			}
			else if(projectile.GetType() == typeof(BoxCollider) && targetType == TargetType.bomb)
			{
				//present was incorrectly recieved!
				triggered = true;
				//add points
				scoreController.addScore(-10);
				//remove trigger
				GameObject.Destroy(gameObject);
			}
		}
	}

	void OnTriggerExit(Collider projectile)
	{
	}
}
