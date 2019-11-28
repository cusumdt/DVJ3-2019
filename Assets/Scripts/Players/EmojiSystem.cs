using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiSystem : MonoBehaviour
{
    public List<GameObject> Emojis;
    public bool emojiOn = false;
    float timeEmoji = 0;

    public string JoystickEmojiString;
    public string JoystickEmojiString2;
    public string InputEmojiString;
    public string InputEmoji2String;
    public string InputEmoji3String;
    public string InputEmoji4String;
    


    void Update()
    {
        if (!emojiOn)
        {
            if (Input.GetKeyDown(InputEmojiString) || Input.GetAxis(JoystickEmojiString) <= -1)
            {
                EmojiInvoque(Emojis[0]);
            }
            else if (Input.GetKeyDown(InputEmoji2String) || Input.GetAxis(JoystickEmojiString) >= 1)
            {
                EmojiInvoque(Emojis[1]);
            }
            else if (Input.GetKeyDown(InputEmoji3String) || Input.GetAxis(JoystickEmojiString2) <= -1)
            {
                EmojiInvoque(Emojis[2]);
            }
            else if (Input.GetKeyDown(InputEmoji4String) || Input.GetAxis(JoystickEmojiString2) >= 1)
            {
                EmojiInvoque(Emojis[3]);
            }
        }
        else
        {
            timeEmoji += 1 * Time.deltaTime;
            if (timeEmoji >= 2.0f)
            {
                timeEmoji = 0;
                emojiOn = false;
            }
        }
    }

    void EmojiInvoque(GameObject Emoji)
    {
        Instantiate(Emoji, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity, this.transform);
        emojiOn = true;
    }
}
