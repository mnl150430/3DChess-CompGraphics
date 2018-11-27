using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerNames : MonoBehaviour
{


    public Text player1_name;
    public Text player2_name;

    public Text player1_view;
    public Text player2_view;
    public Text neutral_view;
    public Text topplayer1_view;
    public Text topplayer2_view;
    public Text topneutral_view;

    public InputField firstInput;
    public InputField secondInput;




    // Use this for initialization
    void Start()
    {
        try
        {
            player1_name.text = PlayerPrefs.GetString("Player1");
            player2_name.text = PlayerPrefs.GetString("Player2");
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            Console.WriteLine();
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }

    }
 
    public void setPlayer(string player)
    {
        if (player.Equals("Player1"))
        {
            PlayerPrefs.SetString("Player1", firstInput.text);
            firstInput.image.color = Color.white;

        }

        else if (player.Equals("Player2"))
        {
            PlayerPrefs.SetString("Player2", secondInput.text);
            secondInput.image.color = Color.white;

        }
    }

    public void ChangeColour(Text t)
    {
        player1_view.color = Color.white;
        player2_view.color = Color.white;
        neutral_view.color = Color.white;
        topplayer1_view.color = Color.white;
        topplayer2_view.color = Color.white;
        topneutral_view.color = Color.white;

        t.color = Color.red;


    }

}

