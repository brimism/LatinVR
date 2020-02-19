using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryController : MonoBehaviour
{
    struct DictEntry {
        public string latin;
        public string english;
        public AudioClip clip;
        public DictEntry(string l, string e, AudioClip aud) {
            latin = l;
            english = e;
            clip = aud;
        }
    }
    public enum VisibleState {NONE, LEFT, RIGHT};
    public VisibleState dominantHand;
    public AudioSource audioSource;
    public TMPro.TextMeshPro latinText;
    public TMPro.TextMeshPro englishText;
    public GameObject dictionary;
    public GameObject RightHandDictionarySlot;
    public GameObject LeftHandDictionarySlot;
    public TextAsset dictionaryFile;
    private GameManager gameManager;
    private int pgNum = -1;
    private Dictionary<string, DictEntry> dict;
    private SortedList<string, DictEntry> foundEntries;
    private VisibleState visible = VisibleState.NONE;
    // Start is called before the first frame update
    void Start()
    {
        dict = new Dictionary<string, DictEntry>();
        foundEntries = new SortedList<string, DictEntry>();
        LoadDictionary();
        dictionary.SetActive(false);
        gameManager = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.LH_Grip && visible != VisibleState.LEFT) {
            // Left grip pressed and dictionary invisible -> Put dictionary in left hand.
            Open(0, VisibleState.LEFT);
        }
        else if(gameManager.LH_Grip && visible == VisibleState.LEFT) {
            // Left grip pressed and dictionary in left hand -> Make dictionary invisible
            Close();
        }
        
        if(gameManager.RH_Grip && visible != VisibleState.RIGHT) {
            // Right grip pressed and dictionary invisible -> Put dictionary in rightt hand.
            Open(0, VisibleState.RIGHT);
        }
        else if(gameManager.RH_Grip && visible == VisibleState.RIGHT) {
            // Right grip pressed and dictionary in right hand -> Make dictionary invisible
            Close();
        }
        
        if(visible == VisibleState.NONE && dictionary.activeSelf){
            // If dictionary is visible but should not be, make it invisible.
            dictionary.transform.parent = this.transform;
            dictionary.transform.localPosition = Vector3.zero;
            dictionary.transform.localEulerAngles = Vector3.zero;
            dictionary.SetActive(false);
        }
    }

    public void AddFound(string latinWord) {
        // Adds the dictionary entry of latinWord to the list of found entries
        foundEntries.Add(latinWord, dict[latinWord]);
        //Debug.Log("AddFound() not implemented yet.");
    }
    public void OpenTo(string latinWord) {
        // Opens the dictionary to the page containing latinWord.
        // For use of the Dialogue System's important words
        int i = 0;
        foreach (var word in foundEntries) {
            if(word.Key == latinWord) {
                break;
            }
            i++;
        }
        Open(i, dominantHand);
        //Debug.Log("OpenTo() not implemented yet.");
    }
    public void Close() {
        // Close the dictionary
        pgNum = -1;
        visible = VisibleState.NONE;
    }
    private void Open(int index, VisibleState pos) {
        // Opens the dictionary to the page number index
        dictionary.transform.parent = ((pos == VisibleState.LEFT)?(LeftHandDictionarySlot):(RightHandDictionarySlot)).transform; // Sets the transform to be the correct transform according to pos
        dictionary.transform.localPosition = Vector3.zero;
        dictionary.transform.localEulerAngles = Vector3.zero;
        pgNum = index;
        SetEntry(index);
        dictionary.SetActive(true);
        visible = pos;
        audioSource.Play();
    }
    private void SetEntry(int index) {
        // Set the values of the text field according to the values of 
        // the entry at foundEntries[index]
        foreach(var entry in foundEntries) {
            if(index == 0) {
                latinText.text = entry.Value.latin;
                englishText.text = entry.Value.english;
                audioSource.clip = entry.Value.clip;
                break;
            }
            else{
                index--;
            }
        }
        //Debug.Log("SetEntry() not implemented yet.");
    }
    private void LoadDictionary() {
        // Loads a CSV file with entries formatted as such
        // latin, english, audioFilePath
        // into dict
        string fileData = dictionaryFile.text;
        string[] lines = fileData.Split("\n"[0]);
        foreach(var line in lines) {
            string[] lineData = (line.Trim()).Split(","[0]);
            dict.Add(lineData[0].ToLower(), new DictEntry(lineData[0], lineData[1],AudioClip.Create(lineData[2], 44100, 2, 44100, false)));
        }
    }
}
