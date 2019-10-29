using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public GameObject Empuje;
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
