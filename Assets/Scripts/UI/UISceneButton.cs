using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UISceneButton : MonoBehaviour
{
    [SerializeField]
    private Button button;
    
    [SerializeField]
    private string scene;

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
        SceneManager.LoadScene(scene);
        Time.timeScale = 1.0f;
    }
}
