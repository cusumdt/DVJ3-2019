using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsSpawner : MonoBehaviour {
    public GameObject[] GoodDrop;
    public GameObject[] BadDrop;
    [SerializeField]
    private int amountGoodObject;
    [SerializeField]
    private int amountBadObject;
    [SerializeField]
    private GameObject[] DropItem;
    private const int amountDrop = 50;
    private const int maxTime = 20;
    private const int minTime = 10;
    [SerializeField]
    private bool ActiveObjects;
    [SerializeField]
    int rand;
    int randTime;
    private float time;

    void Start () {
        ActiveObjects = true;
        DropItem = new GameObject[amountDrop];
        InitDrop ();
    }

    void Update () {

        if (ActiveObjects) {
            randTime = Random.Range (minTime, maxTime);
            ActiveObjects = false;
        }
        time += 1 * Time.deltaTime;
        if (time >= 5f) {
            OnObj ();
            time = 0f;
            ActiveObjects = true;
        }
    }

    void OnObj () {

        rand = Random.Range (1, 11);
        for (int i = 0; i < rand; i++) {
            int randomItem = Random.Range (0, (amountBadObject + amountGoodObject));
            if (!DropItem[randomItem].activeSelf) {
                DropItem[randomItem].SetActive (true);
                DropItem[randomItem].transform.position = new Vector3 (Random.Range (-10.0f, 10.0f), Random.Range (13.0f, 18.0f), 0);
            } else {
                rand++;
                if (rand > amountDrop) {
                    rand = amountDrop;
                }
            }
        }

        // rand = 0;

    }
    void InitDrop () {
        for (int i = 0; i < amountDrop; i++) {
            if (i < amountBadObject) {
                float Scale = Random.Range (0.8f, 1.6f);
                BadDrop[i].transform.localScale = new Vector3 (Scale, Scale, 1.0f);
                DropItem[i] = Instantiate (BadDrop[i], new Vector3 (Random.Range (-10.0f, 10.0f), Random.Range (13.0f, 18.0f), 0), Quaternion.identity);
                DropItem[i].SetActive (false);
            } else if (i >= (amountBadObject) && i < (amountBadObject + amountGoodObject)) {
                DropItem[i] = Instantiate (GoodDrop[(i - amountBadObject)], new Vector3 (Random.Range (-10.0f, 10.0f), Random.Range (13.0f, 18.0f), 0), Quaternion.identity);
                DropItem[i].SetActive (false);
            }
        }
    }

}