using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Loading : MonoBehaviour
{
    public delegate void SetScene(float enrageVal, string _scene);
    public static event SetScene Scene;

    [HideInInspector]
    public float carga;
    [SerializeField]
    private float velCarga;
    private bool CargaOn;
    public Text text;
    public GameObject image;
    // Start is called before the first frame update
    void Start()
    {
        CargaOn = false;
    }

    // Update is called once per frame
    void Update()
    {
            if (carga >= 100)
            {

                carga = 100;
                 if (Input.GetKeyDown("joystick button 0")  || Input.GetKeyDown("space"))
                 {
                    carga = 0;
                    ActualScene("Game");
                    SceneManager.LoadScene("Game");
                }
                text.text = "To start";
                image.SetActive(true);
            }
            else
            {
                carga = carga + (Time.deltaTime * velCarga);
                text.text = ((int)carga).ToString() + "%"; 
            }
    }
    void ActualScene(string _scene)
    {
        if (Scene != null)
        {
            float amount = 1;
            Scene(amount, _scene);
        }
    }
}
