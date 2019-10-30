using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaPlay : MonoBehaviour
{
    public GameObject Obj;

    public void LavaOn()
    {
        Obj.SetActive(true);
        Obj.transform.position = new Vector3(15.0f, 200.0f, 1.0f);
        this.gameObject.SetActive(false);
    }
    public void PointerOn()
    {
        Obj.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
