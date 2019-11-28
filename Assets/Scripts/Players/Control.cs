using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour {
    public enum PlayerType {
        player1,
        player2,
        player3,
        player4
    }
    PlayerType player;

    public bool Jump () {
        switch (player) {
            case PlayerType.player1:
                if (Input.GetKeyDown ("space") || Input.GetKeyDown ("joystick 1 button 0")) {
                    return true;
                }
                break;
            case PlayerType.player2:
                if (Input.GetKeyDown ("[0]") || Input.GetKeyDown ("joystick 2 button 0")) {
                    return true;
                }
                break;
            case PlayerType.player3:
                break;
            case PlayerType.player4:
                break;
            default:
                break;
        }
        return false;
    }
    public bool Right () {
        switch (player) {
            case PlayerType.player1:
                if (Input.GetKey ("d") || Input.GetAxis ("HorizontalJoystick1") >= 1) {
                    return true;
                }
                break;
            case PlayerType.player2:
                if (Input.GetKey ("right") || Input.GetAxis ("HorizontalJoystick2") >= 1) {
                    return true;
                }
                break;
            case PlayerType.player3:
                break;
            case PlayerType.player4:
                break;
            default:
                break;
        }
        return false;
    }
    public bool Left () {
        switch (player) {
            case PlayerType.player1:
                if (Input.GetKey ("a") || Input.GetAxis ("HorizontalJoystick1") <= -1) {
                    return true;
                }
                break;
            case PlayerType.player2:
                if (Input.GetKey ("left") || Input.GetAxis ("HorizontalJoystick2") <= -1) {
                    return true;
                }
                break;
            case PlayerType.player3:
                break;
            case PlayerType.player4:
                break;
            default:
                break;
        }
        return false;
    }
    public bool Skill () {
        switch (player) {
            case PlayerType.player1:
                if (Input.GetKey ("f") || Input.GetKey ("joystick 1 button 2")) {
                    return true;
                }
                break;
            case PlayerType.player2:
                if (Input.GetKey ("[5]") || Input.GetKey ("joystick 2 button 2")) {
                    return true;
                }
                break;
            case PlayerType.player3:
                break;
            case PlayerType.player4:
                break;
            default:
                break;
        }
        return false;
    }
    public bool Dash () {
        switch (player) {
            case PlayerType.player1:
                if (Input.GetKeyDown ("left shift") || Input.GetKeyDown ("joystick 1 button 1")) {
                    return true;
                }
                break;
            case PlayerType.player2:
                if (Input.GetKeyDown ("[.]") || Input.GetKeyDown ("joystick 2 button 1")) {
                    return true;
                }
                break;
            case PlayerType.player3:
                break;
            case PlayerType.player4:
                break;
            default:
                break;
        }
        return false;
    }
    
    public void SetPlayer (PlayerType _player) {
        player = _player;
    }
}