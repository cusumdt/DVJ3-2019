using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour {
    public GameObject Effect = null;
    const float TotalClock = 30f;
    public Vector3 ResetPosition = new Vector3 (10, 120, 1);
    TimeManager timeManager;

    void Start()
    {
        timeManager = TimeManager.Get();
    }

    void OnTriggerEnter2D (Collider2D coll) {
        if (coll.transform.tag == "LavaCollector") {
            AkSoundEngine.PostEvent ("amb_lava_ends", gameObject);
            this.transform.position = ResetPosition;
            timeManager.Clock = TotalClock;
            timeManager.lavaOn = false;
        }
        if (coll.transform.tag == "EnemyCollector" && this.transform.tag == "EnemyObject" || coll.transform.tag == "LavaCollector") {

            this.gameObject.SetActive (false);
        }
        if (coll.transform.tag == "Player" && this.transform.tag == "EnemyObject") {
            AkSoundEngine.PostEvent ("amb_stalactite_breaks", gameObject);
            Instantiate (Effect, new Vector3 (transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            this.gameObject.SetActive (false);
        }
    }
}