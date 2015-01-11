using UnityEngine;
using System.Collections;

public class RotatingPresent : MonoBehaviour {

	public float speed = 80F;
	private GameObject target;

	// Use this for initialization
	void Start () {
		target = GameObject.Find("Elf");
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(target.transform,transform.up);
		//transform.forward = Vector3.Normalize(target.transform.position-transform.position);
		transform.Rotate(-1* Vector3.forward * Time.deltaTime * speed);
	}
}
