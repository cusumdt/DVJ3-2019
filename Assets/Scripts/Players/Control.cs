using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Control
{
    public string JoystickJumpString;
    public string JoystickDashString;
    public string JoystickMovementString;
    public string JoystickSkillString;

    public string InputJumpString;
    public string InputDashString;
    public string InputRightString;
    public string InputLeftString;
    public string InputSkillString;

    public bool Jump()
    {
        if (Input.GetKeyDown(InputJumpString) || Input.GetKeyDown(JoystickJumpString))
            return true;

        return false;
    }
    public bool Right()
    {
        if (Input.GetKey(InputRightString) || Input.GetAxis(JoystickMovementString) >= 1)
            return true;

        return false;
    }
    public bool Left()
    {
        if (Input.GetKey(InputLeftString) || Input.GetAxis(JoystickMovementString) <= -1)
            return true;

        return false;
    }
    public bool Skill()
    {
        if (Input.GetKey(InputSkillString) || Input.GetKey(JoystickSkillString))
            return true;

        return false;
    }
    public bool Dash()
    {
        if (Input.GetKeyDown(InputDashString) || Input.GetKeyDown(JoystickDashString))
            return true;

        return false;
    }
}