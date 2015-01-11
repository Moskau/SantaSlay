using UnityEngine;
using System.Collections;

public class PushWindow : MonoBehaviour {

	// Use this for initialization
	void Start () {
		rigidbody.AddForce(transform.right*-1F*Random.Range(200,1000));
	}
	
	// Update is called once per frame
	void Update () {

	}
}
