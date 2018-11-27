using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ChangeVolume : MonoBehaviour 
{

    public Slider MainSlider;
    public Slider MusicSlider;
    public Slider mySlider;

    float mainVolume;
    float musicVolume;
    float soundsVolume;

    private void Update()
    {
        try
        {
            mainVolume = PlayerPrefs.GetFloat("MainVolume");
            musicVolume = PlayerPrefs.GetFloat("MusicVolume");
            MainSlider.value = mainVolume;
            MusicSlider.value = musicVolume;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            Console.WriteLine();
            Console.WriteLine("Problem in Starting ChangingVolume");
            Console.ReadLine();
        }

    }
    public void setMainVolume()
    {
        mainVolume = MainSlider.value;
        PlayerPrefs.SetFloat("MainVolume", mainVolume);
        setMusicVolume();
    }
    public void setMusicVolume()
    {
        musicVolume = MusicSlider.value;
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
    }
    public void setSoundsVolume()
    {
        soundsVolume = (mySlider.value * PlayerPrefs.GetFloat("MainVolume"));
        PlayerPrefs.SetFloat("SoundsVolume", soundsVolume);
    }
}
