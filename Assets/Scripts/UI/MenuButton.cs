using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuButton : MonoBehaviour
{
    [SerializeField]
    private Button button;
    
    [SerializeField]
    private string scene;
    // Start is called before the first frame update
    void Start()
    {
         if(button == null)
        {
            button = GetComponent<Button>();
        }    
        button.onClick.AddListener(OnClick);
    }
    private void OnClick()
    {
        GameManager.Get().RemovePlayers();
        SceneManager.LoadScene(scene);
    }
}
