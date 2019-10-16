using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIQuitButton : MonoBehaviour
{
 [SerializeField]
    private Button button;

    void Awake()
    {
        if(button == null)
        {
            button = GetComponent<Button>();
        }    
        button.onClick.AddListener(OnClick);
    }
    private void OnClick()
    {
        Application.Quit();
    }
}
