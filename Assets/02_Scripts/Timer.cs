using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

	private float startTime = 0f;
	private bool counting = false;
	private GameManagerScript gm;
	private bool past1s = false;
	void Awake(){
		gm = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
	}
	void Update () {
		if(counting){
			if(Time.time>startTime+1f && !past1s){
				gm.UpdateValue("1s", true);
				gm.Reevaluate();
				gm.UpdateValue("1s", false);
				past1s = true;
			}
			if(Time.time>startTime+3f){
				counting = false;
				gm.UpdateValue("3s", true);
				gm.Reevaluate();
				gm.UpdateValue("3s", false);
			}
		}
	}

	public void StartTimer(){
		past1s = false;
		counting = true;
		startTime = Time.time;
	}
}
