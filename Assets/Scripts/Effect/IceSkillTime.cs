using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSkillTime : MonoBehaviour
{
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += 1 * Time.deltaTime;
        if (time >= 0.2f)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
