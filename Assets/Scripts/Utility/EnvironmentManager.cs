using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public List<GameObject> Environment;
    public int EnviroAmount;
    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range (0 , EnviroAmount);
        Environment[rand].SetActive(true);         

    }

}
