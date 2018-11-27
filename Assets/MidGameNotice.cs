using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;



public class MidGameNotice : MonoBehaviour 
{

    public void setInGameYes()
    {
        PlayerPrefs.SetString("MidGame", "Yes");
    }
    public void setInGameNo()
    {
        PlayerPrefs.SetString("MidGame", "No");
    }
    public void setComingFromSounds()
    {
        PlayerPrefs.SetString("From", "Sounds");
    }
    public void setComingFromHelp()
    {
        PlayerPrefs.SetString("From", "Help");
    }

}
