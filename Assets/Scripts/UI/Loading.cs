using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Loading : MonoBehaviour
{

     public Image BarraDeCarga;
     [HideInInspector]
    public float carga;
    [SerializeField]
    private float velCarga;
    private bool CargaOn;
    // Start is called before the first frame update
    void Start()
    {
        CargaOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(CargaOn)
        {
          carga = carga + (Time.deltaTime* velCarga);
            BarraDeCarga.fillAmount = carga / 100;
            
            if (carga >= 100)
            {   
                carga = 0;
                SceneManager.LoadScene("Game");
            }
        }
    }
    void SetCargaOn()
    {
        CargaOn = true;
    }
}
