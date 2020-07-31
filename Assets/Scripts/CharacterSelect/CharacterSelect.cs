using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CharacterSelect : MonoBehaviour
{
    public delegate void PlayerCharacter(float enrageVal, int p1, int p2);
    public static event PlayerCharacter PlayerSelection;

    public enum Player
    {
        P1,
        P2
    }
    public enum Characters
    {
        Wambo,
        Rony,
        Googli,
        Mubble,
        Chunky
    }
    // Start is called before the first frame update
    public Characters actualP1;
    public Characters actualP2;
    public GameObject[] p1;
    public GameObject[] p2;

    // Update is called once per frame
    void Update()
    {
        Refresh(actualP1, p1);
        Refresh(actualP2, p2);
    }

    public void NextCharacter(int p)
    {
        switch ((Player)p)
        {
            case Player.P1:
                actualP1++;
                if (actualP1 > Characters.Chunky)
                    actualP1 = Characters.Wambo;
                break;
            case Player.P2:
                actualP2++;
                if (actualP2 > Characters.Chunky)
                    actualP2 = Characters.Wambo;
                break;
        }
    

    }
    public void PreviousCharacter(int p)
    {
        switch ((Player)p)
        {
            case Player.P1:
                actualP1--;
                if (actualP1 < Characters.Wambo)
                    actualP1 = Characters.Chunky;
                break;
            case Player.P2:
                actualP2--;
                if (actualP2 < Characters.Wambo)
                    actualP2 = Characters.Chunky;
                break;
        }
    }
    void Refresh(Characters actualP, GameObject[] p)
    {
        switch (actualP)
        {
            case Characters.Wambo:
                p[(int)Characters.Wambo].SetActive(true);
                p[(int)Characters.Rony].SetActive(false);
                p[(int)Characters.Googli].SetActive(false);
                p[(int)Characters.Mubble].SetActive(false);
                p[(int)Characters.Chunky].SetActive(false);
                break;
            case Characters.Rony:
                p[(int)Characters.Wambo].SetActive(false);
                p[(int)Characters.Rony].SetActive(true);
                p[(int)Characters.Googli].SetActive(false);
                p[(int)Characters.Mubble].SetActive(false);
                p[(int)Characters.Chunky].SetActive(false);
                break;
            case Characters.Googli:
                p[(int)Characters.Wambo].SetActive(false);
                p[(int)Characters.Rony].SetActive(false);
                p[(int)Characters.Googli].SetActive(true);
                p[(int)Characters.Mubble].SetActive(false);
                p[(int)Characters.Chunky].SetActive(false);
                break;
            case Characters.Mubble:
                p[(int)Characters.Wambo].SetActive(false);
                p[(int)Characters.Rony].SetActive(false);
                p[(int)Characters.Googli].SetActive(false);
                p[(int)Characters.Mubble].SetActive(true);
                p[(int)Characters.Chunky].SetActive(false);
                break;
            case Characters.Chunky:
                p[(int)Characters.Wambo].SetActive(false);
                p[(int)Characters.Rony].SetActive(false);
                p[(int)Characters.Googli].SetActive(false);
                p[(int)Characters.Mubble].SetActive(false);
                p[(int)Characters.Chunky].SetActive(true);
                break;
            default:
                break;
        }
    }

    public void StartGame()
    {
        PlayerSelected((int)actualP1, (int)actualP2);
        SceneManager.LoadScene("Loading");
    }
    void PlayerSelected(int p1, int p2)
    {
        if (PlayerSelection != null)
        {
            float amount = 1;
            PlayerSelection(amount, p1, p2);
        }
    }

}
