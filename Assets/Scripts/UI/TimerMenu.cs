using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerMenu : MonoBehaviour
{
    float carga;
    public float velCarga;
    public GameObject menu;


    // Update is called once per frame
    void Update()
    {
        if (carga >= 100)
        {

            carga = 0;
            menu.SetActive(true);
     
        }
        else
        {
            carga = carga + (Time.deltaTime * velCarga);

        }
    }
}
