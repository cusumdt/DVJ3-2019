using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    void Start()
    {
        //Set Cursor to not be visible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
