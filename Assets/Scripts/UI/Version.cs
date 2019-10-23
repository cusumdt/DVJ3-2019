using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Version : MonoBehaviour
{
    public Text textoDeVersion;

    void Start()
    {
        textoDeVersion.text = "v" + Application.version;
	}

}
