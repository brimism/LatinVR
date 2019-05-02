using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointingIndicator : MonoBehaviour {

	private float startTime;
	private Transform disk;
	private bool expanding = false;
	private float expandTime;
	private Transform player;
	// Use this for initialization
	void Awake () {
		disk = transform.Find("Disk");
		player = GameObject.Find("CenterEyeAnchor").transform;
	}

	void Update () {
		if(expanding){
			float f = Mathf.Clamp((Time.time-startTime)/expandTime, 0, 1);
			disk.localScale = new Vector3(f*0.2f, f*0.2f, 1f);
			transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
		}
	}

	public void StartExpanding(float f){
		if(!expanding){
			startTime = Time.time;
			disk.localScale = new Vector3(0f,0f,1f);
			expanding = true;
			expandTime = f;
		}
	}

	public void StopExpanding(){
		expanding = false;
	}
}
