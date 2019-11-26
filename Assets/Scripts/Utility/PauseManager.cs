using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseManager : MonoBehaviour
{
    public delegate void Pause (float enrageVal, bool state);
    public static event Pause OnPause;


    public GameObject Panel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKeyDown("escape") || Input.GetKeyDown("joystick 1 button 7") || Input.GetKeyDown("joystick 2 button 7") )
         {
             if(Time.timeScale != 0f)
             {
                SetPause(true);
                 Panel.SetActive(true);
                Time.timeScale = 0f;
             }
             else
             {
                 SetPause(false);
                 Panel.SetActive(false);
                 Time.timeScale = 1f;
             }
         }
    }
    public void ReturnGame()
    {
        SetPause(false);
        Panel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Menu()
    {
        SetPause(false);
        Panel.SetActive(false);
        Time.timeScale =1f;
        SceneManager.LoadScene("Menu");
    }
    public void SoundOff()
    {

    }
    void SetPause(bool state)
    {
        if (OnPause != null)
        {
            float amount = 1;
            OnPause(amount, state);
        }
    }
}
