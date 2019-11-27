using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDestroy : MonoBehaviour {
    private float time;
    public float totalTime;
    
    void Start () {
        time = 0.0f;
    }
    
    void Update () {
        time += 1 * Time.deltaTime;
        if (time >= totalTime) {
            Destroy (this.gameObject);
        }
    }
}