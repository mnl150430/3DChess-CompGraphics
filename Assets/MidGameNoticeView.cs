using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidGameNoticeView : MonoBehaviour 
{

    public GameObject MainPanel;
    public GameObject SettingsPanel;
    public GameObject BackGround;
    public GameObject OldBackground;
    public GameObject HelpPanel;
    public GameObject SBackButton;
    public GameObject SReturnButton;
    public GameObject HBackButton;
    public GameObject HReturnButton;
    void Start () 
    {

            if (PlayerPrefs.GetString("MidGame").Equals("Yes"))
            {
                MainPanel.SetActive(false);
                if (PlayerPrefs.GetString("From").Equals("Sounds"))
                {
                    OldBackground.SetActive(false);
                    BackGround.SetActive(true);
                    SettingsPanel.SetActive(true);
                    HelpPanel.SetActive(false);
                    SBackButton.SetActive(false);
                    SReturnButton.SetActive(true);
                }
                 else if (PlayerPrefs.GetString("From").Equals("Help"))
                 {
                        OldBackground.SetActive(false);
                        BackGround.SetActive(true);
                        SettingsPanel.SetActive(false);
                        HelpPanel.SetActive(true);
                        HBackButton.SetActive(false);
                        HReturnButton.SetActive(true);
                  }
            PlayerPrefs.SetString("MidGame", "No");

        }
     }
       

	
	
}
