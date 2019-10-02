using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionLife : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("DestroyTime");
    }
    IEnumerator DestroyTime()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
