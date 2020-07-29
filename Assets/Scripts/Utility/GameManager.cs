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

    public int characterP1;
    public int characterP2;
    #endregion

    #region Floats
    public float velLoad;
    float Load;
    #endregion

    string playerLoser = null;
    string playerWinner = null;
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
        CharacterSelect.PlayerSelection += PlayerSelected;
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

    public string GetWinner () {
         return playerWinner;
    }
    public string GetLose () {
         return playerLoser;
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
        playerWinner = null;
    }
    void PlayerLose (float amount, string player, string winner) {
        if(playerLoser == null)
            playerLoser = player;
        if (playerWinner == null)
            playerWinner = winner;
    }

    void PlayerSelected(float amount, int _p1, int _p2)
    {
        characterP1 = _p1;
        characterP2 = _p2;
    }


    void ActualScene (float v, string _scene) {

        scene = _scene;

    }
}