using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeManager : Singleton<TimeManager> {

    public float Clock;
    public float actualClock;
    public bool lavaOn;
    [SerializeField]
    private float TimeMax;
    public Image ClockImage;
    public GameObject Arrow;

    void Start () {

        Clock = TimeMax;
        lavaOn = false;

    }

    void Update () {
        if (Clock > 0) {
            ClockImage.fillAmount = Clock / TimeMax;
            Clock -= 1 * Time.deltaTime;
        } else {
            if (!lavaOn) {
                AkSoundEngine.PostEvent ("amb_lava_warning", gameObject);
                Arrow.SetActive (true);
                lavaOn = true;
            }
        }
        actualClock = Clock;
    }

}