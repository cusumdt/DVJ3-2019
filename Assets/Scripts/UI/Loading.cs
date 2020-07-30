using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Loading : MonoBehaviour {
    public delegate void SetScene (float enrageVal, string _scene);
    public static event SetScene Scene;

    [HideInInspector]
    public float Load;
    [SerializeField]
    private float velLoad;
    private bool LoadOn;
    public Text text;
    public GameObject image;
    bool SoundFullLoad = false;

    void Start () {
        LoadOn = false;
        SoundFullLoad = true;
    }

    void Update () {
        if (Load >= 100) {
            if (SoundFullLoad) {
                SoundFullLoad = false;
                AkSoundEngine.PostEvent ("ui_tutorial_loading_full", gameObject);
            }
            Load = 100;
            if (Input.GetKeyDown ("joystick button 0") || Input.GetKeyDown ("space")) {
                AkSoundEngine.PostEvent ("ui_tutorial_startgame", gameObject);
                Load = 0;
                ActualScene ("Game");
                SceneManager.LoadScene ("Game");
            }
            text.text = "Iniciar";
            image.SetActive (true);
        } else {
            Load = Load + (Time.deltaTime * velLoad);
            text.text = ((int) Load).ToString () + "%";
        }
    }
    void ActualScene (string _scene) {
        if (Scene != null) {
            float amount = 1;
            Scene (amount, _scene);
        }
    }
}