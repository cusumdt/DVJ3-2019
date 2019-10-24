using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{

    static public float Clock;
   public float veamosClock;
    static public bool lavaOn;
    [SerializeField]
    private float TimeMax;
    public Image ClockImage;
    public GameObject Lava;
    // Start is called before the first frame update
    void Start()
    {
        
        Clock = TimeMax;
        lavaOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Clock > 0)
        {
            ClockImage.fillAmount = Clock / TimeMax;
            Clock -= 1 * Time.deltaTime;
        }
        else
        {
            if (!lavaOn)
            {
                Lava.SetActive(true);
                Lava.transform.position = new Vector3(10.0f, 120.0f, 1.0f);
                lavaOn = true;
            }
        }
        veamosClock = Clock;
    }
}
