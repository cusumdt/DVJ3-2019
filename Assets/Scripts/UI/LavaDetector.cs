using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LavaDetector : MonoBehaviour
{
    public Transform lavaPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Camera.main.WorldToScreenPoint(lavaPoint.position);
        direction.Normalize();
        float rot_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
        
    }
}
