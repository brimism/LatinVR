﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gotTheBread : MonoBehaviour
{
    public SceneTransition sm;
    public GameObject player;
    [Yarn.Unity.YarnCommand("gotThePanis")]
    public void GotThePanis()
    {
        // Goal complete
        player.GetComponent<getBread>().GetPanis("false");
        sm.EndGame();
    }
}
