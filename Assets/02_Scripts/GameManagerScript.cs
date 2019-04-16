using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

	public bool giveJug = false;
	public bool pointToTorch = false;
	public bool pointToWheel = false;
	public bool oneSecElapsed = false;
	public int currHead = 0;
	public TextScript textBubble;

	[System.Serializable]
	public class Edge{
		public string condition;
		public int toNode;
	}

	[System.Serializable]
	public class Node{
		public bool isVocabMatchingParent = false;
		public bool isVocabMatchingSuccess = false;
		public bool isVocabMatchingFail = false;
		public int lastSelected;
		public string text = "";
		public AudioClip ac;
		public List<Edge> edges = new List<Edge>();
	}

	public List<Node> graph = new List<Node>();
	Dictionary<string, bool> boolDict = new Dictionary<string, bool>();
	private AudioSource aus;
	private Timer timer;

	public void Start(){
		aus = GetComponent<AudioSource>();
		timer = GameObject.Find("Timer").GetComponent<Timer>();
		SetupDict();
		Reevaluate();
		textBubble.ChangeText(graph[currHead].text);
		if(graph[currHead].ac!=null){
			aus.clip = graph[currHead].ac;
			aus.Play();
		}
	}

	public void UpdateValue(string s, bool b){
		boolDict[s] = b;
	}

	public void SetupDict(){
		boolDict["give jug"] = giveJug;
		boolDict["point to torch"] = pointToTorch;
		boolDict["point to wheel"] = pointToWheel;
		boolDict["1s"] = oneSecElapsed;
	}

	public void Reevaluate(){
		// UpdateDict();
		Node n = graph[currHead];
		for(int i=0; i<n.edges.Count; i++){
			if(boolDict[n.edges[i].condition.ToLower()]){
				currHead = n.edges[i].toNode;
				if(graph[currHead].isVocabMatchingParent){
					if(graph[currHead].edges.Count>1){
						int r = Random.Range(0, graph[currHead].edges.Count-1);
						Debug.Log(r);
						graph[currHead].lastSelected = r;
						currHead = graph[currHead].edges[graph[currHead].lastSelected].toNode;
					}else{
						currHead = graph[currHead].edges[0].toNode;
					}
				}
				if(graph[currHead].isVocabMatchingSuccess){
					Node vmp = graph[graph[currHead].edges[0].toNode];
					vmp.edges.RemoveAt(vmp.lastSelected);
					timer.StartTimer();
				}else if(graph[currHead].isVocabMatchingFail){
					timer.StartTimer();
				}
				textBubble.ChangeText(graph[currHead].text);
				if(graph[currHead].ac!=null){
					aus.clip = graph[currHead].ac;
					aus.Play();
				}
				break;
			}
		}
	}

	void Update () {

	}
}
