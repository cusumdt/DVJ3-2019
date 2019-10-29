using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDestroy : MonoBehaviour
{
    private float time;
    public float totalTime;
    // Start is called before the first frame update
    void Start()
    {
        time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += 1 * Time.deltaTime;
        if (time >= totalTime)
        {
            Destroy(this.gameObject);
        }
    }
}
