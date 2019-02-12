using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextScript : MonoBehaviour {
	public TextMeshPro tmp;
	// Use this for initialization
	void Start () {
	}

	public void ChangeText(string t){
		tmp.SetText(t);
	}

	// Update is called once per frame
	void Update () {

	}
}
