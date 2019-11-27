using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCollision : MonoBehaviour
{
   void OnCollisionEnter2D(Collision2D coll)
    {
        // If the Collider2D component is enabled on the collided object
        if ((coll.transform.tag == "Player" && this.transform.tag == "IcePowerUp") ||
            (coll.transform.tag == "Player" && this.transform.tag == "EmpujePowerUp") ||
             (coll.transform.tag == "EmpujePowerUp" && this.transform.tag == "IcePowerUp"))
        {

            this.gameObject.SetActive(false);
        }
        else if (coll.transform.tag == "Floor" && this.transform.tag == "IcePowerUp")
        {
            StartCoroutine("DestroyPotion");
        }
  
    }
    
    IEnumerator DestroyPotion()
    {
        yield return new WaitForSeconds(1.0f);
        this.gameObject.SetActive(false);
    }
}
