using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour
{
    public Material presentMaterial;
    public Material bombMaterial;
	public RectTransform followPowerBar;
	public RectTransform elfPowerBar;
	public MyCharacterController characterController;
	public float power = 1500F;
	public float startingUpVeloctiy = 2F;
	public Transform landingObject;

	public AudioClip giftSound;
	public AudioClip bombSound;
	
	private const string giftButton = "ONE";
	private const string bombButton = "TWO";
	private const int giftMouseButton = 0;
	private const int bombMouseButton = 1;

	private Vector3 landingPosition = Vector3.zero;	
	private Vector3 landingNormal = Vector3.zero;	
	// Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
		if (SixenseInput.Controllers[1].Enabled) //SixenseInput.Controllers[1] != null && 
		{
        	foreach (SixenseButtons button in System.Enum.GetValues(typeof(SixenseButtons)))
			{
            	if (SixenseInput.Controllers[1].GetButtonDown(button))
            	{
					if (button.ToString() == giftButton)
					{
						FireGift();
					}
					if (button.ToString() == bombButton)
					{
						FireBomb();
                	}
				}
			}
		}
		else
		{
			if (Input.GetMouseButtonDown(giftMouseButton))
			{
				FireGift();
			}
			if (Input.GetMouseButtonDown(bombMouseButton))
			{
				FireBomb();
			}
		}

		Vector3 v = characterController.getCharacterVelocity() + Vector3.up*startingUpVeloctiy + gameObject.transform.forward*power*Time.fixedDeltaTime;
		SetLandingPosition(v, gameObject.transform.position + gameObject.transform.forward*0.5F);
		landingObject.position = landingPosition;
		landingObject.up = landingNormal;
	}

	void FireGift()
	{
		AudioSource.PlayClipAtPoint(giftSound, transform.position, 1.5f);
		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.transform.position = gameObject.transform.position + gameObject.transform.forward*0.5F;
		cube.transform.localScale = new Vector3(0.2F, 0.2F, 0.2F);
		cube.renderer.material = presentMaterial;
		cube.layer = 14;
		Rigidbody cubeRigidBody = cube.AddComponent("Rigidbody") as Rigidbody;
		cubeRigidBody.velocity = characterController.getCharacterVelocity() + Vector3.up*startingUpVeloctiy;
		cubeRigidBody.AddForce(gameObject.transform.forward*power);
	}

	void FireBomb()
	{
		AudioSource.PlayClipAtPoint(bombSound, transform.position, 1.5f);
		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		cube.transform.position = gameObject.transform.position + gameObject.transform.forward*0.5F;
		cube.transform.localScale = new Vector3(0.2F, 0.2F, 0.2F);
		cube.renderer.material = bombMaterial;
		cube.layer = 14;
		Rigidbody cubeRigidBody = cube.AddComponent("Rigidbody") as Rigidbody;
		cubeRigidBody.velocity = characterController.getCharacterVelocity() + Vector3.up*startingUpVeloctiy;
		cubeRigidBody.AddForce(gameObject.transform.forward*power);
	}

	void SetLandingPosition(Vector3 velocity, Vector3 position)
	{
		float timeDelta = 0.1F;
		float maxTravelDistance = 10000;
		Vector3 currentPosition = position;
		Vector3 oldPosition = position;
		Vector3 currentVelocity = velocity;
		Vector3 normal = Vector3.up;
		Vector3 gravity = Physics.gravity;
		float traveledDistance = 0.0F;
		bool collided = false;
		int layerMask = 1;
		layerMask += 1 << 10;
		layerMask += 1 << 13;
		RaycastHit collisionInfo;

		while(!collided && traveledDistance < maxTravelDistance)
		{
			currentPosition += currentVelocity * timeDelta + 0.5f * gravity * timeDelta * timeDelta;
			currentVelocity += gravity * timeDelta;
			collided = Physics.Linecast(oldPosition,currentPosition, out collisionInfo, layerMask);
			traveledDistance += (currentPosition-oldPosition).magnitude;
			if(collided)
			{
				currentPosition = collisionInfo.point;
				if(collisionInfo.collider.gameObject.layer == 13){
					normal = Vector3.up;
				}else{
					normal = collisionInfo.normal;
				}
			}
		}

		landingPosition = currentPosition;
		landingNormal = normal;
	}
}
