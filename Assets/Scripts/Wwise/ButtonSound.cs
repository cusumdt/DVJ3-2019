using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonSound : MonoBehaviour
{

    public void PressedButton()
    {
        AkSoundEngine.PostEvent("ui_menu_select", gameObject);
    }
    public void PressedStart()
    {
        AkSoundEngine.PostEvent("ui_menu_play_start", gameObject);
    }
    public void Explosion()
    {
        AkSoundEngine.PostEvent("ui_menu_play_stop", gameObject);
    }
}
