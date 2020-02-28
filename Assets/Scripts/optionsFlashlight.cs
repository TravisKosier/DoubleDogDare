using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class optionsFlashlight : MonoBehaviour
{

   

    public Light flashlight;

    


    void Start()
    {
      

        flashlight.intensity =  PlayerPrefs.GetFloat("sliderBrightness");

    }

    // Update is called once per frame
    void Update()
    {
        
           
        
    }
}
