using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGame : MonoBehaviour {

    public GameObject GameManager;
    public GameObject Board;
    public GameObject Music;
    public void setGame()
    {
        PlayerPrefs.SetString("From", "NewGame");
    
    }
}
