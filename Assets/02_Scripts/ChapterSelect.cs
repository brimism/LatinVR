using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChapterSelect : MonoBehaviour {
    int[] chapterSceneIndex;
    public GameObject[] chapterCards = new GameObject[12];
    public GameObject[] chapterButtons = new GameObject[12];
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void loadScene(int chapterNum)
    {
        SceneManager.LoadScene(chapterSceneIndex[chapterNum]);
    }
}
