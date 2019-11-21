using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEmoji : MonoBehaviour
{
    void Update()
    {
        if (transform.rotation.y != 0)
        {
            transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);

        }
    }
    public void DeleteEmoji()
    {
        Destroy(this.gameObject);
    }
}
