using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class MidGameNotice : MonoBehaviour 
{
  
    public void Update()
    {

    }
    public void leavingGame()
    {

        if (PlayerPrefs.GetString("From").Equals("NewGame") == false)
        {
            saveData();
        }
    }
    public void saveData()
    {
        GameManager.instance.MultiToList();
        SavedData s = GameManager.instance.saver;
        try
        {
            using (Stream stream = File.Open("data.bin", FileMode.Create))
            {
                BinaryFormatter bin = new BinaryFormatter();
                bin.Serialize(stream, s);
            }
        }
        catch (IOException){}
    }

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
