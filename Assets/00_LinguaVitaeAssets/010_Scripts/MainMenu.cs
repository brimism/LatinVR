using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public GameObject[] Menu_Panels = new GameObject[2];
    public GameObject[] Stage_Cards = new GameObject[8];
    public GameObject[] Nov_Panels = new GameObject[2];
    public bool nov;
    public AudioSource nov_audio;
	// Use this for initialization
	void Start()
    {
        if (!nov) {
            setActivePanel("Title Panel");
        }
        else
        {
            setActivePanel("Nov Title Panel");
        }
    }
    
	// Update is called once per frame
	void Update () {
		
	}

    public void setActivePanel(string str)
    {
        switch (str)
        {
            case "Title Panel":
                activatePanel(0);
                break;
            case "Stage Select Panel":
                activatePanel(1);
                break;
            case "Nov Title Panel":
                novActivatePanel(0);
                break;
            case "Nov Stage Panel":
                novActivatePanel(1);
                playSound();
                break;
            // Options?
            default:
                print("error: unreachable panel");
                break;
        }
    }

    public void playSound()
    {
        nov_audio.Play();
    }

    void novActivatePanel(int index)
    {
        foreach (GameObject g in Nov_Panels)
        {
            g.SetActive(false);
        }
        Nov_Panels[index].SetActive(true);
    }

    void activatePanel(int index)
    {
        foreach(GameObject g in Menu_Panels)
        {
            g.SetActive(false);
        }
        Menu_Panels[index].SetActive(true);
    }

    public void setActiveCard(int index)
    {
        activateCard(index - 1);
    }

    void activateCard(int index)
    {
        foreach (GameObject g in Stage_Cards)
        {
            g.SetActive(false);
        }
        Stage_Cards[index].SetActive(true);
    }

    public void disableCards()
    {
        foreach (GameObject g in Stage_Cards)
        {
            g.SetActive(false);
        }
    }

    public void loadLevel(string level)
    {
        print("loading level: " + level);
        SceneManager.LoadScene(level);
    }

    public void quitLevel()
    {
        print("quitting...");
        Application.Quit();
    }
}
