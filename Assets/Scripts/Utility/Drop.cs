using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public GameObject[] GoodDrop;
    public GameObject[] BadDrop;
    [SerializeField]
    private int cantGoodObject;
    [SerializeField]
    private int cantBadObject;

    [SerializeField]
    private GameObject[] DropItem;
    private const int CantDrop = 50;
     [SerializeField]
    private bool ActiveObjects;
    [SerializeField]
    int rand;
    // Start is called before the first frame update
    void Start()
    {
        ActiveObjects=true;
       //cantGoodObject = sizeof(GoodDrop) / sizeof(GameObject);
       //cantBadObject = sizeof(BadDrop) / sizeof(GameObject);
       DropItem =  new GameObject [CantDrop];
       InitDrop();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(ActiveObjects)
        {
           StartCoroutine(ActiveObj(Random.Range(1,5)));
           
        }
    }

    IEnumerator ActiveObj(float time)
    {
        OnObj();
        ActiveObjects = false;
        yield return new WaitForSeconds(time);
        Debug.Log(time);
        ActiveObjects = true;
    }
    void OnObj()
    {
        
        rand = Random.Range(1,11);
        for (int i = 0; i < rand ; i++)
        {
            int randomItem = Random.Range(0,(cantBadObject +cantGoodObject) );
            if(!DropItem[randomItem].activeSelf)
            {
                DropItem[randomItem].SetActive(true);
                DropItem[randomItem].transform.position = new Vector3(Random.Range(-10.0f,10.0f), Random.Range(13.0f,18.0f), 0);
            }
            else
            {
                rand++;
                if(rand > CantDrop)
                {
                   rand = CantDrop;
                }
            }
        }
       
       // rand = 0;
      
    }
    void InitDrop()
    {
        for (int i = 0; i < CantDrop; i++)
        {
            if(i < cantBadObject)
            {
                float Scale = Random.Range(0.8f, 1.6f);
                BadDrop[i].transform.localScale = new Vector3(Scale, Scale, 1.0f); 
                DropItem[i] = Instantiate(BadDrop[i], new Vector3(Random.Range(-10.0f,10.0f), Random.Range(13.0f,18.0f), 0), Quaternion.identity);
                DropItem[i].SetActive(false);
            }
            else if (i>= (cantBadObject) && i< (cantBadObject + cantGoodObject))
            {
                DropItem[i] = Instantiate(GoodDrop[(i-cantBadObject)], new Vector3(Random.Range(-10.0f,10.0f), Random.Range(13.0f,18.0f), 0), Quaternion.identity);
                DropItem[i].SetActive(false);
            }
        }
    }

}
