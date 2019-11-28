using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Inicio : MonoBehaviour {
    void PlayMenu () {
        SceneManager.LoadScene ("Menu");
    }

    void Update()
    {
        if (Input.GetKeyDown("escape") || Input.GetKeyDown("joystick 1 button 7") || Input.GetKeyDown("joystick 2 button 7"))
            SceneManager.LoadScene("Menu");
    }
}