using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileButtons : MonoBehaviour
{
    static public bool mButtonHasBeenPressed = false;
    static public Direction.direction mButtonDirectionPressed = Direction.direction.None;

    // Start is called before the first frame update
    void Start()
    {
        if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    public void buttonDirectionPressed(int direction)
    {
        mButtonDirectionPressed = (Direction.direction)direction;
        mButtonHasBeenPressed = true;
    }

}
