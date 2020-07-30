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
    public Animator LooserAnim;
    public Text winnerText;
    public Text seccondText;

    void Start () {
        string Loser = GameManager.Get().GetLose();
        string Winner = GameManager.Get().GetWinner();
        GameManager.Get().RestartPlayer();

        if (Winner == "player2_rony" || Winner == "player_rony")
            WinnerAnim.SetInteger("Player", 1);
        else if (Winner == "player2_wambo" || Winner == "player_wambo")
            WinnerAnim.SetInteger("Player", 2);
        else
            WinnerAnim.SetInteger("Player", 3);
        if (Loser == "player2_rony" || Loser == "player_rony")
            LooserAnim.SetInteger("Player", 1);
        else if (Loser == "player2_wambo" || Loser == "player_wambo")
            LooserAnim.SetInteger("Player", 2);
        else
            LooserAnim.SetInteger("Player", 3);

        if (Winner == "player_rony" || Winner == "player_wambo" || Winner == "player_googli") {
            winnerText.text = "Player1"; ;
            seccondText.text = "Player2";
        }
        else {
            winnerText.text = "Player2"; ;
            seccondText.text = "Player1";
        }
        seccond.sprite = thisLose.sprite;
        winner.sprite = thisWinner.sprite;
    }
}