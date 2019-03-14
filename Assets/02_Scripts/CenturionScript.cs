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
		if(c.gameObject.name == "Jug"){
			gm.giveJug = true;
			gm.Reevaluate();
			Destroy(c.gameObject);
			// aus.clip = goodJob;
			// aus.Play();
		}else if(c.transform.root.tag != "Player"){
			// aus.clip = giveJug;
			// aus.Play();
		}
	}
}
