using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectColision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D coll)
    {
        // If the Collider2D component is enabled on the collided object
        if (coll.transform.tag == "Player" && this.transform.tag == "EnemyObject" || coll.transform.tag == "Player" && this.transform.tag == "Coin" || coll.transform.tag == "Floor" && this.transform.tag == "Coin" )
        {
          this.gameObject.SetActive(false);
        }
  
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.transform.tag == "CoinCollector" && this.transform.tag == "EnemyObject")
        {
          this.gameObject.SetActive(false);
        }
    }
}
