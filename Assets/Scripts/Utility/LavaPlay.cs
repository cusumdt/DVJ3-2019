using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaPlay : MonoBehaviour
{
    public GameObject Lava;

    public void LavaOn()
    {
        Lava.SetActive(true);
        Lava.transform.position = new Vector3(10.0f, 120.0f, 1.0f);
        this.gameObject.SetActive(false);
    }
}
