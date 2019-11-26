using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{
    public delegate void PlayerState(float enrageVal);
    public static event PlayerState OnPause;

    public List<GameObject> players;
    public List<GameObject> pjs;
    float carga;
    public float velCarga;

    public override void Awake()
    {
        base.Awake();
        carga = 0;
    }

   void Update()
   {
     if(players.Count == 1 && SceneManager.GetActiveScene().name == "Game")
     {
            Time.timeScale = 0.5f;
            if (carga >= 100)
            {
                carga = 0;
                Time.timeScale = 1.0f;
                RestartPause();
                SceneManager.LoadScene("GameOver");
            }
            else
            {
                carga = carga + (Time.deltaTime * velCarga);
            }

    
     }
   }
   
   public void SetPlayers(GameObject player)
   {
       players.Add(player);
   }
    public GameObject GetWinner()
    {
        for (int j = 0; j < 1; j++)
        {
            for (int i = 0; i < 2; i++)
            {
                if(pjs[i].name!=players[j].name)
                {
                    return pjs[i];
                }
            }
        }
        return new GameObject();
        
    }
    public GameObject GetLose()
    {
        return players[0];
    }
    public void RemovePlayers()
    {
        players.Clear();
    }
    void RestartPause()
    {
        if (OnPause != null)
        {
            float amount = 1;
            OnPause(amount);
        }
    }
}
