using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenturionScript : MonoBehaviour {
	// public AudioClip giveJug;
	// public AudioClip goodJob;


	private GameManagerScript gm;
	// private AudioSource aus;

	void Start () {
		gm = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
		// aus = GetComponent<AudioSource>();
	}

	void Update () {

	}



	public void OnTriggerEnter(Collider c){
		if(c.gameObject.tag == "Bread"){
			gm.UpdateValue("give bread", true);
			gm.Reevaluate();
		}else if(c.gameObject.tag == "Jug"){
			gm.UpdateValue("give jug", true);
			gm.Reevaluate();
		}else if(c.gameObject.tag == "Grapes"){
			gm.UpdateValue("give grapes", true);
			gm.Reevaluate();
		}else if(c.transform.root.tag != "Player"){
		}
	}
}
