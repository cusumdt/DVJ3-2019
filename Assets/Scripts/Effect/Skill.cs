using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    private float time = 0.0f;
    public float LimitTime;

    virtual public void Update()
    {
        time += 1 * Time.deltaTime;
        if (time >= LimitTime)
        {
            Destroy(this.gameObject);
        }
    }
}
