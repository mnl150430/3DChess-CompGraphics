using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputInterface : MonoBehaviour 
{
    public InputField firstInput;
    public InputField secondInput;


    void Update()
    {
        if (firstInput.isFocused)
        {
            clearPlaceHolder(firstInput); 
        }
        else if (secondInput.isFocused)
        {
            clearPlaceHolder(secondInput);
        }
    }

    public void clearPlaceHolder(InputField x)
    {
        if (x.isFocused && x.text.Equals(""))
        {
            x.placeholder.GetComponent<Text>().text = "";

            x.image.color = new Color(0.91f, 0.91f, 0.91f, 1.0f);
        }
    }
}
