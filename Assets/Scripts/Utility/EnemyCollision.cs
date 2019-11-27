using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public GameObject Effect = null;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.transform.tag == "LavaCollector")
        {
            AkSoundEngine.PostEvent("amb_lava_ends", gameObject);
            Vector3 ResetPosition = new Vector3(10.0f, 120.0f, 1.0f);
            this.transform.position = ResetPosition;
            TimeManager.Clock = 30.0f;
            TimeManager.lavaOn = false;
        }
        if (coll.transform.tag == "EnemyCollector" && this.transform.tag == "EnemyObject" || coll.transform.tag == "LavaCollector")
        {

            this.gameObject.SetActive(false);
         
        }
        if(coll.transform.tag == "Player" && this.transform.tag == "EnemyObject")
        {
           AkSoundEngine.PostEvent("amb_stalactite_breaks", gameObject);
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
