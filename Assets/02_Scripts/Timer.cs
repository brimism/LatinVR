using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

	private float startTime = 0f;
	private bool counting = false;
	private GameManagerScript gm;
	private bool past1s = false;
	private float toCount = -1f;
	private float builtInAudioBuffer = 1f;

	void Awake(){
		gm = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
	}

	void Update () {
		if(counting){
			if(toCount>0){
                // If the amount of time to count is set, count for that much time, then send an "audio finished" signal
				if(Time.time>startTime+toCount){
					gm.UpdateValue("audio finished", true);
					gm.Reevaluate();
					gm.UpdateValue("audio finished", false);
				}
			}else{
                // If the amount of time to count is not set...
				if(Time.time>startTime+1f && !past1s){
                    // And we dont want to count past one second, send a "1s" signal when 1 second passes
					gm.UpdateValue("1s", true);
					gm.Reevaluate();
					gm.UpdateValue("1s", false);
					past1s = true;
				}
				if(Time.time>startTime+3f){
                    // And we want to count past 1 second, send a "3s" signal when three seconds pass
					counting = false;
					gm.UpdateValue("3s", true);
					gm.Reevaluate();
					gm.UpdateValue("3s", false);
				}
			}
		}
	}

	public void StartTimer(){
		past1s = false;
		counting = true;
		startTime = Time.time;
		toCount = -1f;
	}

	public void StartTimer(float t){
		past1s = false;
		counting = true;
		startTime = Time.time;
		toCount = t+builtInAudioBuffer;
	}
}
