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
        if (positionX >= transform.position.x + 16 || positionX <= transform.position.x - 16 || positionY >= transform.position.y + 10 ||
            positionP2X >= transform.position.x + 16 || positionP2X <= transform.position.x - 16 || positionP2Y >= transform.position.y + 10)
        {
            if (CameraSize < 13.0f)
                CameraObj.orthographicSize += (2 * Time.deltaTime);
        }
        else if (positionX >= transform.position.x + 14 || positionX <= transform.position.x - 14 || positionY >= transform.position.y + 7 ||
            positionP2X >= transform.position.x + 14 || positionP2X <= transform.position.x - 14 || positionP2Y >= transform.position.y + 7)
        {
            if (CameraSize < 11.0f)
                CameraObj.orthographicSize += (2 * Time.deltaTime);
                if(CameraSize > 12.0f)
                CameraObj.orthographicSize -= (2 * Time.deltaTime);
           
        }
        else if (positionX >= transform.position.x + 11 || positionX <= transform.position.x - 11 || positionY >= transform.position.y + 4 ||
            positionP2X >= transform.position.x + 11 || positionP2X <= transform.position.x - 11 || positionP2Y >= transform.position.y + 4)
        {
            if (CameraSize < 9.0f)
                CameraObj.orthographicSize += (2 * Time.deltaTime);
            if(CameraSize > 10.0f)
                CameraObj.orthographicSize -= (2 * Time.deltaTime);
           
        }
        else if(positionX >= transform.position.x + 8 || positionX <= transform.position.x - 8 || positionY >= transform.position.y ||
            positionP2X >= transform.position.x + 8 || positionP2X <= transform.position.x - 8 || positionP2Y >= transform.position.y)
        {
            if (CameraSize < 7.0f)
                CameraObj.orthographicSize += (2 * Time.deltaTime);
            if(CameraSize >8.0f)
                CameraObj.orthographicSize -= (2 * Time.deltaTime);
        }
        else
        {
            if (CameraSize > 5.0f)
                CameraObj.orthographicSize -= (2 * Time.deltaTime);
        }
        if(positionX > positionP2X)
        {
            transform.position = new Vector3 ((positionX + positionP2X) / 2, transform.position.y,transform.position.z);
        }
         if(positionX < positionP2X)
        {
            transform.position = new Vector3 ((positionP2X + positionX) / 2, transform.position.y,transform.position.z);
        }
            if(positionY > positionP2Y)
        {
            transform.position = new Vector3 (transform.position.x, (positionY + positionP2Y) /2 + 1,transform.position.z);
        }
            if(positionY < positionP2Y)
        {
            transform.position = new Vector3 (transform.position.x,(positionP2Y + positionY)/2 +1,transform.position.z);
        }
    }
}
