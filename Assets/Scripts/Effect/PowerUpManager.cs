using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public GameObject Empuje;
    public float time;

    void Update()
    {
        if (!Empuje.activeSelf)
        {
            time += 1 * Time.deltaTime;
            if (time >= 10f)
            {
                time = 0.0f;
                Empuje.SetActive(true);
            }
        }

    }
}
