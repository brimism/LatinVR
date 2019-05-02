using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

	// public bool giveJug = false;
	// public bool pointToTorch = false;
	// public bool pointToWheel = false;
	// public bool oneSecElapsed = false;
	// public bool threeSecElapsed = false;
	// public bool hiAtticus = false;
	// public bool hiQuintus = false;
	public int currHead = 0;
	public List<TextScript> textBubbles;
	public List<AudioSource> auses;

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
		public bool startTimer = false;
		public int lastSelected;
		public bool changeText = true;
		public int textBubbleNumber;
		public string text = "";
		public List<GameObject> toEnable = new List<GameObject>();
		public List<GameObject> toDisable = new List<GameObject>();
		public List<string> bools2Reset = new List<string>();
		public AudioClip ac;
		public int ausNumber;
		public List<Edge> edges = new List<Edge>();
	}

	public List<Node> graph = new List<Node>();
	Dictionary<string, bool> boolDict = new Dictionary<string, bool>();
	private AudioSource aus;
	private Timer timer;

	public void Start(){
		timer = GameObject.Find("Timer").GetComponent<Timer>();
		SetupDict();
		textBubbles[graph[currHead].textBubbleNumber].ChangeText(graph[currHead].text);
		if(graph[currHead].ac!=null){
			auses[graph[currHead].ausNumber].clip = graph[currHead].ac;
			auses[graph[currHead].ausNumber].Play();
		}
		if(graph[currHead].startTimer){
			timer.StartTimer();
		}
	}

	public void SetupDict(){
		boolDict["give jug"] = false;
		boolDict["point to torch"] = false;
		boolDict["point to wheel"] = false;
		boolDict["1s"] = false;
		boolDict["3s"] = false;
		boolDict["hi atticus"] = false;
		boolDict["hi quintus"] = false;
		boolDict["well enough"] = false;
		boolDict["poorly"] = false;
		boolDict["point to bread"] = false;
		boolDict["point to vase"] = false;
		boolDict["point to grapes"] = false;
		boolDict["take my money"] = false;
	}

	public void UpdateValue(string s, bool b){
		boolDict[s] = b;
	}

	public void Reevaluate(){
		// UpdateDict();
		Node n = graph[currHead];
		for(int i=0; i<n.edges.Count; i++){
			if(boolDict[n.edges[i].condition.ToLower()]){
				currHead = n.edges[i].toNode;
				foreach(string s in graph[currHead].bools2Reset){
					UpdateValue(s, false);
				}
				if(graph[currHead].isVocabMatchingParent){
					if(graph[currHead].edges.Count>1){
						int r = Random.Range(0, graph[currHead].edges.Count-1);
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
				if(graph[currHead].startTimer){
					timer.StartTimer();
				}
				foreach(GameObject g in graph[currHead].toEnable){
					g.SetActive(true);
				}
				foreach(GameObject g in graph[currHead].toDisable){
					g.SetActive(false);
				}
				if(graph[currHead].changeText){
					textBubbles[graph[currHead].textBubbleNumber].ChangeText(graph[currHead].text);
				}
				if(graph[currHead].ac!=null){
					auses[graph[currHead].ausNumber].clip = graph[currHead].ac;
					auses[graph[currHead].ausNumber].Play();
				}
				break;
			}
		}
	}
}
