using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIMainMenu : MonoBehaviour
{
    [SerializeField]
    private Button button;
    [SerializeField]
    private Button quit;
    Animator m_Animator;
    void Awake()
    {
        if(button == null)
        {
            button = GetComponent<Button>();
        }    
        button.onClick.AddListener(OnClick);
        m_Animator = button.gameObject.GetComponent<Animator>();
    }
    private void OnClick()
    {
        if (button.transform.name == "Play")
        {
            m_Animator.SetBool("On", true);
        }
        else
        {
            Application.Quit();
        }
    }
    

}

