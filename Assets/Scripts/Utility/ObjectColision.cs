using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectColision : MonoBehaviour
{
    public GameObject Effect = null;

    void OnCollisionEnter2D(Collision2D coll)
    {
        // If the Collider2D component is enabled on the collided object
        if (coll.transform.tag == "Player" && this.transform.tag == "IcePowerUp" ||
            coll.transform.tag == "Player" && this.transform.tag == "EmpujePowerUp" ||
             coll.transform.tag == "EmpujePowerUp" && this.transform.tag == "IcePowerUp")
        {

            this.gameObject.SetActive(false);
        }
        else if (coll.transform.tag == "Floor" && this.transform.tag == "IcePowerUp")
        {
            StartCoroutine("DestroyPotion");
        }
  
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.transform.tag == "LavaCollector")
        {
            this.transform.position = new Vector3(10.0f, 120.0f, 1.0f);
            TimeManager.Clock = 30.0f;
            TimeManager.lavaOn = false;
        }
        if (coll.transform.tag == "CoinCollector" && this.transform.tag == "EnemyObject" || coll.transform.tag == "LavaCollector")
        {

            this.gameObject.SetActive(false);
         
        }
        if(coll.transform.tag == "Player" && this.transform.tag == "EnemyObject")
        {
           Instantiate(Effect, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
           this.gameObject.SetActive(false);
        }

    }

    IEnumerator DestroyPotion()
    {
        yield return new WaitForSeconds(1.0f);
        this.gameObject.SetActive(false);
    }
}
