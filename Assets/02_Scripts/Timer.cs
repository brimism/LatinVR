using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

	private float startTime = 0f;
	private bool counting = false;
	private GameManagerScript gm;

	void Start(){
		gm = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
	}
	void Update () {
		if(counting){
			if(Time.time>startTime+1f){
				counting = false;
				gm.UpdateValue("1s", true);
				gm.Reevaluate();
				gm.UpdateValue("1s", false);
			}
		}
	}

	public void StartTimer(){
		counting = true;
		startTime = Time.time;
	}
}
