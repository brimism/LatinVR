using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

	public bool giveJug = false;
	public int currHead = 0;
	public TextScript textBubble;

	[System.Serializable]
	public class Edge{
		public string condition;
		public int toNode;
	}

	[System.Serializable]
	public class Node{
		public string text;
		public List<Edge> edges = new List<Edge>();
	}

	public List<Node> graph = new List<Node>();
	Dictionary<string, bool> boolDict = new Dictionary<string, bool>();

	public void Start(){
		Reevaluate();
	}

	public void UpdateDict(){
		boolDict["give jug"] = giveJug;
	}

	public void Reevaluate(){
		UpdateDict();
		Node n = graph[currHead];
		for(int i=0; i<n.edges.Count; i++){
			if(boolDict[n.edges[i].condition.ToLower()]){
				currHead = n.edges[i].toNode;
				break;
			}
		}
		textBubble.ChangeText(graph[currHead].text);
	}

	void Update () {

	}
}
