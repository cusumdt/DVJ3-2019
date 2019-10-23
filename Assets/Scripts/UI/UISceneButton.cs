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
    Animator m_Animator;
    void Awake()
    {
        if(button == null)
        {
            button = GetComponent<Button>();
        }    
        button.onClick.AddListener(OnClick);
        m_Animator = gameObject.GetComponent<Animator>();
    }
    private void OnClick()
    {
       m_Animator.SetBool("On",true);
    }
    

}

