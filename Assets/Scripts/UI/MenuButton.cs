using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuButton : MonoBehaviour {
    [SerializeField]
    Button button;
    [SerializeField]
    string scene;

    void Start () {
        if (button == null) {
            button = GetComponent<Button> ();
        }
        button.onClick.AddListener (OnClick);
    }
    private void OnClick () {
        GameManager.Get ().RemovePlayers ();
        SceneManager.LoadScene (scene);
    }
}