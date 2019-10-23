using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnim : MonoBehaviour
{
     public Animator est_Animator;
    public Animator m_Animator;

   
    void EstON()
    {
        m_Animator.SetBool("On",false);
        est_Animator.SetBool("On",true);
    }
}
