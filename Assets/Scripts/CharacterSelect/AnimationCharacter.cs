using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCharacter : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator m_anim;
    public int character;
    void Start()
    {
        m_anim.SetInteger("Player", character);
    }
    private void Update()
    {
        m_anim.SetInteger("Player", character);
    }
}
