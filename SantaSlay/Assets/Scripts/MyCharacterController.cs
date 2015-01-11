using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MyCharacterController : MonoBehaviour {

	public float sleighSpeed = 5;
	public MyInputController myInput;
	public GameObject frontObject;
	public GameObject midObject;
	public GameObject backObject;
	public float maxPitch = 40; //maximum Pitch in which the sleigh acts normally, before it stops climbing due to the pitchThreshold
	public float pitchThreshold = 5; //threshold over the maxPitch at which the sleigh stops increasing pitch (makes a smooth stop to climbing animation)
	public float zeroAngleThreshold = 2F; //threshold for just driving straight (makes it easier to drive straight)

	public float fogCreep = 0.001F;

	public float maxX = 500;
	public float minX = 500;
	public float maxY = 500;
	public float minY = 500;
	public float maxZ = 500;
	public float minZ = 500;

	private float rollAngle;
	private float pitchAngle;
	
	//collider
	public SphereCollider[] characterColliders;
	private int worldCollidersLayer = 0;

	private Vector3 playerSpeed;
	private Vector3 oldPosition;

	// Use this for initialization
	void Start () {
		rollAngle =0;
		pitchAngle=0;
		playerSpeed=Vector3.zero;
		worldCollidersLayer = 1 << 9;
		worldCollidersLayer += 1 << 10;
		worldCollidersLayer += 1 << 13;
		RenderSettings.fog = true;
	}
	
	// Update is called once per frame
	void Update () {
		oldPosition = transform.position;
		rollAngle = myInput.GetRollAngle();
		pitchAngle = myInput.GetPitchAngle();

		if(rollAngle < zeroAngleThreshold && rollAngle > -zeroAngleThreshold)
		{
			rollAngle = 0;
		}
		if(pitchAngle < zeroAngleThreshold && pitchAngle > -zeroAngleThreshold)
		{
			pitchAngle = 0;
		}

		//adjust yaw
		transform.Rotate(Vector3.up * Time.deltaTime * rollAngle, Space.World);

		//adjust pitch
		float x = transform.localRotation.eulerAngles.x;
		if(x > 180 && x < 360-maxPitch && pitchAngle < 0)
		{
			pitchAngle = pitchAngle + ((360-maxPitch-x)*myInput.GetMaxValue()/pitchThreshold);
			if(pitchAngle >= -0.1F){
				pitchAngle = 0;
			}
		}
		else if(x < 180 && x > maxPitch && pitchAngle >0)
		{
			pitchAngle = pitchAngle - ((x-maxPitch)*myInput.GetMaxValue()/pitchThreshold);
			if(pitchAngle <= 0.1F){
				pitchAngle = 0;
			}
		}
		transform.Rotate(Vector3.right * Time.deltaTime * pitchAngle, Space.Self);

		//move sleigh forward
		transform.position += transform.forward * Time.deltaTime * sleighSpeed;

		//adjust sleigh geometry positions
		frontObject.transform.localEulerAngles = new Vector3(pitchAngle/4, rollAngle/4, -rollAngle/2);
		midObject.transform.localEulerAngles = new Vector3(0, 0, -rollAngle/2);
		backObject.transform.localEulerAngles = new Vector3(-pitchAngle/4, -rollAngle/4, -rollAngle/2);

		//check for collisions
		foreach (SphereCollider sleighCol in characterColliders)
		{	
			foreach (Collider col in Physics.OverlapSphere(sleighCol.gameObject.transform.position, sleighCol.radius, worldCollidersLayer))
			{
				Vector3 contactPoint = col.ClosestPointOnBounds(sleighCol.gameObject.transform.position);
				
				Vector3 v = sleighCol.gameObject.transform.position - contactPoint;
				
				transform.position += Vector3.ClampMagnitude(v, Mathf.Clamp(sleighCol.radius - v.magnitude, 0, sleighCol.radius));
			}
		}

		if(transform.position.x > maxX){
			transform.position = new Vector3(maxX,transform.position.y,transform.position.z);
		}else if(transform.position.x < minX){
			transform.position = new Vector3(minX,transform.position.y,transform.position.z);
		}
		if(transform.position.y > maxY){
			transform.position = new Vector3(transform.position.x,maxY,transform.position.z);
		}else if(transform.position.y < minY){
			transform.position = new Vector3(transform.position.x,minY,transform.position.z);
		}
		if(transform.position.z > maxZ){
			transform.position = new Vector3(transform.position.x,transform.position.y,maxZ);
		}else if(transform.position.z < minZ){
			transform.position = new Vector3(transform.position.x,transform.position.y,minZ);
		}

		playerSpeed = (transform.position - oldPosition)/Time.deltaTime;

		//RenderSettings.fogDensity = Vector3.Distance(transform.position, Vector3.zero)*fogCreep;
	}

	public Vector3 getCharacterVelocity ()
	{
		return playerSpeed;
	}

	public void gameOver ()
	{
		sleighSpeed = 0;
	}
}
