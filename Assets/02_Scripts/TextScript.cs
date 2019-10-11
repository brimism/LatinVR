using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextScript : MonoBehaviour {
	public TextMeshPro tmp;
	public Transform player;

	public void ChangeText(string t){
        // Change text to new string
		tmp.SetText(t);
	}

	void Update () {
        // Makes the text face the right way
		transform.LookAt(player);
		transform.RotateAround(transform.position, Vector3.up, -155f);
	}
}
