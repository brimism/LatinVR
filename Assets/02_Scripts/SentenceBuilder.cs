using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class SentenceBuilder : MonoBehaviour {

    public List<GameObject> words = new List<GameObject>();
    public Text sentenceText;
    int sentLen = 0;

    static int sortByTransform(GameObject g1, GameObject g2)
    {
        return g1.transform.localPosition.x.CompareTo(g2.transform.localPosition.x);
    }

    void detectOrder()
    {
        words.Sort(sortByTransform);
        updateSentence();
    }

    void updateSentence()
    {
        string sent = "";
        if(words.Count>0)
             sent = words[0].name;
        for(int i = 1; i < words.Count; i++)
        {
            sent = sent + " " + words[i].name;
        }
        sentenceText.text = sent;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (sentLen!=0)
        {
            detectOrder();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "word")
        {
           
            words.Add(other.gameObject);
            sentLen++;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "word")
        {
            words.Remove(other.gameObject);
            
            sentLen--;
            if (sentLen == 0)
            {
                detectOrder();
            }
        }
    }
}
