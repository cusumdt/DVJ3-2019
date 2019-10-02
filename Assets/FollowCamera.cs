using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject player;
    public GameObject player2;
    public Camera CameraObj;

    
    void Start()
    {
      
        CameraObj = gameObject.GetComponent<Camera>();
    }
    void Update()
    {

        float positionX = player.transform.position.x;
        float positionY = player.transform.position.y;
        float positionP2X = player2.transform.position.x;
        float positionP2Y = player2.transform.position.y;
        float CameraSize = CameraObj.orthographicSize;
        
        if (positionX >= transform.position.x + 11 || positionX <= transform.position.x - 11 || positionY >= transform.position.y + 4 ||
            positionP2X >= transform.position.x + 11 || positionP2X <= transform.position.x - 11 || positionP2Y >= transform.position.y + 4)
        {
            if (CameraSize < 8.0f)
                CameraObj.orthographicSize += (2 * Time.deltaTime);
           
        }
        else if(positionX >= transform.position.x + 8 || positionX <= transform.position.x - 8 || positionY >= transform.position.y ||
            positionP2X >= transform.position.x + 8 || positionP2X <= transform.position.x - 8 || positionP2Y >= transform.position.y)
        {
            if (CameraSize < 6.0f)
                CameraObj.orthographicSize += (2 * Time.deltaTime);
            if(CameraSize >7.0f)
                CameraObj.orthographicSize -= (2 * Time.deltaTime);
        }
        else
        {
            if (CameraSize > 5.0f)
                CameraObj.orthographicSize -= (2 * Time.deltaTime);
        }
    }
}
