using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static short LivePlayers;
    void Start()
    {
        LivePlayers = 0;
    }

    public static short GetLivePlayers()
    {
        return LivePlayers;
    }
    public static void SetLivePlayers(short CantPlayers)
    {
        LivePlayers = CantPlayers;
    }
    public static void SubsLP(short cant)
    {
        LivePlayers -= cant;
    }
}
