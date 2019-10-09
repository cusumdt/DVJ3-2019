using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectiles : MonoBehaviour
{

    public GameObject particle;
    public Vector3 mousePos;
    public Vector3 objectPos;
    public GameObject sword;
    public GameObject sword2;
    public GameObject sword3;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
    }

 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            /* Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

             if (Physics.Raycast(ray))
             {
                 Debug.Log(transform.position);
                 Instantiate(particle, transform.position, transform.rotation);
             }*/

            mousePos = Input.mousePosition;
            mousePos.z = 8;
            objectPos = Camera.main.ScreenToWorldPoint(mousePos);

            Instantiate(particle, objectPos, Quaternion.identity);
        }
        float step = speed * Time.deltaTime;

       sword.transform.position = Vector2.MoveTowards(sword.transform.position, objectPos, step);
        sword2.transform.position = Vector2.MoveTowards(sword2.transform.position, objectPos, step);
        sword3.transform.position = Vector2.MoveTowards(sword3.transform.position, objectPos, step);
     
    }

}