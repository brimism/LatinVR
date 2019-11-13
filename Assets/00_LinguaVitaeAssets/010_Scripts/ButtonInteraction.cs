using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInteraction : MonoBehaviour
{
    public Image im;
    Color idle_color;
    Sprite idle_sprite;
    public Color hover_color;
    public Color clicked_color;
    public Sprite hover_sprite;
    public Sprite clicked_sprite;
    enum Button_state
    {
        NONE, HOVER, CLICKED
    };
    Button_state bs = Button_state.NONE;
    // Start is called before the first frame update
    void Start()
    {

        im = GetComponent<Image>();
        //idle_color = im.color;
        idle_sprite = im.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        switch (bs)
        {
            case Button_state.CLICKED:
                //im.color = clicked_color;
                im.sprite = clicked_sprite;
                break;
            case Button_state.HOVER:
                //im.color = hover_color;
                im.sprite = hover_sprite;
                break;
            default:
                //im.color = idle_color;
                im.sprite = idle_sprite;
                break;
        }
    }

    public void onHover()
    {
        bs = Button_state.HOVER;
    }

    public void onClick()
    {
        bs = Button_state.CLICKED;
    }

    public void onRelease()
    {
        bs = Button_state.NONE;
    }

}
