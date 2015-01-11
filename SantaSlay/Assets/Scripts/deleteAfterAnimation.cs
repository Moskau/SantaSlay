using UnityEngine;
using System.Collections;

public class deleteAfterAnimation : MonoBehaviour {
	public float deleteTime = 10F;
	private float startTime;
	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time >= startTime + deleteTime){
			GameObject.Destroy(gameObject);
		}
	}
}
