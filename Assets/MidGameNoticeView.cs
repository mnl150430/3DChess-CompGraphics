using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidGameNoticeView : MonoBehaviour 
{

    public GameObject MainPanel;
    public GameObject SettingsPanel;
    //public GameObject HelpPanel;
    public GameObject BackButton;
    public GameObject ReturnButton;
    void Start () 
    {

            if (PlayerPrefs.GetString("MidGame").Equals("Yes"))
            {
                MainPanel.SetActive(false);
                if (PlayerPrefs.GetString("From").Equals("Sounds"))
                {
                    SettingsPanel.SetActive(true);
                    //HelpPanel.SetActive(false);
                    BackButton.SetActive(false);
                    ReturnButton.SetActive(true);
                }
            PlayerPrefs.SetString("MidGame", "No");
            }
     }
       

	
	
}
