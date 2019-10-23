using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameOverStage : MonoBehaviour
{
    // Start is called before the first frame update
    public Image winner;
    public Image seccond;
    public SpriteRenderer thisWinner;
    public SpriteRenderer thisLose;
    bool init;
    void Start()
    {
        init = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(!init)
        {
            GameObject asd = GameManager.instance.GetLose();
            GameObject ase = GameManager.instance.GetWinner();
            thisLose = asd.GetComponent<SpriteRenderer>();
            thisWinner = ase.GetComponent<SpriteRenderer>();
            seccond.sprite = thisLose.sprite;
            winner.sprite = thisWinner.sprite;
            init = true;
        }
    }
}
