using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : Singleton<CharacterManager>
{
    public GameObject[] characterP1;
    public GameObject[] characterP2;
    public GameObject[] iconP1;
    public GameObject[] iconP2;
    public int p1;
    public int p2;
    // Start is called before the first frame update
    void Start()
    {
        p1 = GameManager.Get().characterP1;
        p2 = GameManager.Get().characterP2;
        characterP1[p1].SetActive(true);
        characterP2[p2].SetActive(true);
        iconP1[p1].SetActive(true);
        iconP2[p2].SetActive(true);
    }


   
}
