using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour {
    public string name;
    void Start () {
        AkSoundEngine.PostEvent (name, gameObject);
    }
}