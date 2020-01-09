using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hand_scr : MonoBehaviour
{
    public bool pressed;
    public Vector3 pos;
    public Quaternion rot;

    public GameObject hand_obj;

    bool clicked;
    RaycastHit hit;
    ButtonInteraction active_button;
    public GameObject pointer_sphere;

    void Update()
    {
        if (Physics.Raycast(hand_obj.transform.position, hand_obj.transform.forward, out hit, 2000.0F))
        {
            pointer_sphere.transform.position = hit.point;
            if (hit.transform.gameObject.tag == "Button")
            {
                //Debug.Log(hit.transform.gameObject.name);
                if (active_button == null)
                {
                    ActivateButton();
                } else
                {
                    DeactivateButton();
                    ActivateButton();
                }
                
            }
            else
            {
                if (active_button != null)
                {
                    DeactivateButton();
                }
            }

            if (pressed && !clicked)
            {
                Debug.Log("pressed");
                clicked = true;


                Debug.Log(hit.transform.gameObject.name);
                if (hit.transform.gameObject.tag == "Button")
                {
                    ClickButton();

                }


            }
            
        } else if (active_button != null)
        {
            DeactivateButton();
        }
        else
        {
            pointer_sphere.transform.position = new Vector3(0, 0, 10000);
        }
        hand_obj.transform.position = pos;
        hand_obj.transform.rotation = rot;

        if (!pressed)
        {
            clicked = false;
        }


    }

    void ClickButton()
    {
        active_button.onClick();
        hit.transform.gameObject.GetComponent<Button>().onClick.Invoke();
    }

    void ActivateButton()
    {

        active_button = hit.transform.gameObject.GetComponent<ButtonInteraction>();
        active_button.onHover();
    }

    void DeactivateButton()
    {
        active_button.onRelease();
        active_button = null;
    }
}
