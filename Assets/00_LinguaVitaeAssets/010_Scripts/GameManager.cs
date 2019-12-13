using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn;

public class GameManager : YarnObserver
{
    public ItemHolder playerItemHolder;

    public override void Observe(string var_name, Yarn.Value value)
    {
        // Set food quest item
        if(var_name == "$food_quest" && value.AsNumber != 0)
        {
            if (value.AsNumber == 1)
            {
                playerItemHolder.HoldItem("bread");
            }
            else if (value.AsNumber == 2)
            {
                playerItemHolder.HoldItem("grapes");
            }
            else if (value.AsNumber == 3)
            {
                playerItemHolder.HoldItem("jar");
            }
            else if(value.AsNumber == -1)
            {
                //Quest complete
                SceneManager.LoadScene("Finish");
            }

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
