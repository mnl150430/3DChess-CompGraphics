using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class SoundSettings : MonoBehaviour
{

    public AudioSource soundEffect;

    float mainVolume;
    float SoundVolume;
    // Update is called once per frame


    void Update()
    {
        try
        {
            soundEffect.volume = (((1.0f + PlayerPrefs.GetFloat("SoundsVolume")) + (1.0f + PlayerPrefs.GetFloat("MainVolume"))) / 2.0f) - 1.0f;

            if ((Mathf.Approximately(PlayerPrefs.GetFloat("MainVolume"), 0.0f)) || (Mathf.Approximately(PlayerPrefs.GetFloat("SoundsVolume"), 0.0f)))
                soundEffect.volume = 0.0f;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            Console.WriteLine();
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
            soundEffect.volume = 1.0f;
            PlayerPrefs.SetFloat("MainVolume", 1.0f);
            PlayerPrefs.SetFloat("MusicVolume", 1.0f);
            PlayerPrefs.SetFloat("SoundsVolume", 1.0f);

        }
    }

}