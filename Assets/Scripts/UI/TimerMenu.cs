using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerMenu : MonoBehaviour {
    float Load;
    public float velLoad;
    public GameObject menu;

    // Update is called once per frame
    void Update () {
        if (Load >= 100) {
            Load = 0;
            menu.SetActive (true);

        } else {
            Load = Load + (Time.deltaTime * velLoad);
        }
    }
}