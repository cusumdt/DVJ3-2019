using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager> {
    #region Events
    public delegate void PlayerState (float enrageVal);
    public static event PlayerState OnPause;
    #endregion

    #region List
    public List<GameObject> pjs;
    #endregion

    #region Floats
    public float velLoad;
    float Load;
    #endregion

    string playerLoser = null;
    public string scene;
    public override void Awake () {

        base.Awake ();
        Load = 0;
        scene = "Game";
    }

    void Start () {
        PauseManager.Scene += ActualScene;
        Loading.Scene += ActualScene;
        Player.WhoDefeat += PlayerLose;
    }

    void Update () {
        if (playerLoser != null) {
            Time.timeScale = 0.5f;
            if (Load >= 100) {
                Load = 0;
                Time.timeScale = 1.0f;
                RestartPause ();
                scene = "GameOver";
                SceneManager.LoadScene ("GameOver");
            } else {
                Load = Load + (Time.deltaTime * velLoad);
            }
        }
    }

    public GameObject GetWinner () {
        for (int i = 0; i < 2; i++) {
            if (pjs[i].name != playerLoser) {
                return pjs[i];
            }
        }
        return null;
    }
    public GameObject GetLose () {
        for (int i = 0; i < 2; i++) {
            if (pjs[i].name == playerLoser) {
                return pjs[i];
            }
        }
        return null;
    }
    public void RemovePlayers () {
        playerLoser=null;
    }
    void RestartPause () {
        if (OnPause != null) {
            float amount = 1;
            OnPause (amount);
        }
    }
    public void RestartPlayer()
    {
        playerLoser = null;
    }
    void PlayerLose (float amount, string player) {
        if(playerLoser == null)
            playerLoser = player;
    }
    void ActualScene (float v, string _scene) {

        scene = _scene;

    }
}