using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{
    public static GameManager instance = null;
    public List<GameObject> players;
    public List<GameObject> pjs;
    float carga;
    public float velCarga;
    void Awake()
    {
          if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);    
            }
            DontDestroyOnLoad(this);
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
}
