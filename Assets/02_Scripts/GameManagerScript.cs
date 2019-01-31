using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

	public bool giveJug = false;

	public int currHead = 0;

	[System.Serializable]
	public class Node{
		public string text;
		public bool condition;
	}

	public List<Node> graph = new List<Node>();


	// Use this for initialization
	void Start () {

	}

	public void Reevaluate(){

	}

	// Update is called once per frame
	void Update () {

	}
}
