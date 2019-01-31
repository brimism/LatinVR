using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenturionScript : MonoBehaviour {
	public AudioClip giveJug;
	public AudioClip goodJob;

	private AudioSource aus;
	void Start () {
		aus = GetComponent<AudioSource>();
	}

	void Update () {

	}

	public void OnTriggerEnter(Collider c){
		if(c.gameObject.name == "Jug"){
			aus.clip = goodJob;
			aus.Play();
		}else if(c.transform.root.tag == "Player"){

		}else{
			aus.clip = giveJug;
			aus.Play();
		}
	}
}
