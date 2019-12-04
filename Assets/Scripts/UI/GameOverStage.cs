using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameOverStage : MonoBehaviour {
    public Image winner;
    public Image seccond;
    public SpriteRenderer thisWinner;
    public SpriteRenderer thisLose;
    public Animator WinnerAnim;

    void Start () {
        GameObject Loser = GameManager.Get().GetLose();
        GameObject Winner = GameManager.Get().GetWinner();
        thisLose = Loser.GetComponent<SpriteRenderer>();
        thisWinner = Winner.GetComponent<SpriteRenderer>();
        WinnerAnim.SetInteger("Player", Winner.name == "player" ? 1 : 2);
        seccond.sprite = thisLose.sprite;
        winner.sprite = thisWinner.sprite;
    }
}