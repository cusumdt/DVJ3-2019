using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{

    public Animator m_Animator;


    // Update is called once per frame
    void ShakeCamera()
    {
        m_Animator.SetBool("On", true);
    }
}
