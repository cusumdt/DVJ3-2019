using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseManager : MonoBehaviour {
    public delegate void Pause (float enrageVal, bool state);
    public static event Pause OnPause;

    public delegate void SetScene (float enrageVal, string _scene);
    public static event SetScene Scene;

    public GameObject Panel;

    void Update () {
        if (Input.GetKeyDown ("escape") || Input.GetKeyDown ("joystick 1 button 7") || Input.GetKeyDown ("joystick 2 button 7")) {
            if (Time.timeScale != 0f) {
                AkSoundEngine.PostEvent ("ui_pause_on", gameObject);
                SetPause (true);
                Panel.SetActive (true);
                Time.timeScale = 0f;
            } else {
                AkSoundEngine.PostEvent ("ui_pause_off", gameObject);
                SetPause (false);
                Panel.SetActive (false);
                Time.timeScale = 1f;
            }
        }
    }
    public void ReturnGame () {
        SetPause (false);
        Panel.SetActive (false);
        Time.timeScale = 1f;
    }
    public void Menu () {
        SetPause (false);
        Panel.SetActive (false);
        Time.timeScale = 1f;
        ActualScene ("Menu");
        SceneManager.LoadScene ("Menu");
    }

    public void SoundOff () {

    }
    
    void SetPause (bool state) {
        if (OnPause != null) {
            float amount = 1;
            OnPause (amount, state);
        }
    }
    void ActualScene (string _scene) {
        if (Scene != null) {
            float amount = 1;
            Scene (amount, _scene);
        }
    }
}