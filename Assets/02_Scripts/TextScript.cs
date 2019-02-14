using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextScript : MonoBehaviour {
	public TextMeshPro tmp;
	public Transform player;
	// Use this for initialization
	void Start () {
	}

	public void ChangeText(string t){
		tmp.SetText(t);
	}

	// Update is called once per frame
	void Update () {
		transform.LookAt(player);
		transform.RotateAround(transform.position, Vector3.up, -155f);
	}
}
