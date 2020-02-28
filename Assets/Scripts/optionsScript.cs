using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class optionsScript : MonoBehaviour
{
    public Light Brightness;

    public AudioMixer audioMixer;

   

    public Slider brightSlider;

    public Slider volumeSlider;

    public float sliderbrightnessnumber;



    private void Start()
    {
        Brightness = GetComponent<Light>();

        

        if (PlayerPrefs.GetFloat("sliderBrightness")==0f)
        {
            PlayerPrefs.SetFloat("sliderBrightness", Brightness.intensity);
        }
        brightSlider.value = PlayerPrefs.GetFloat("sliderBrightness");

        volumeSlider.value= PlayerPrefs.GetFloat("volume");
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("volume", volume);
        PlayerPrefs.Save();
    }

    public void SetBrightness()
    {
        sliderbrightnessnumber= PlayerPrefs.GetFloat("sliderBrightness");
        Brightness.intensity = (brightSlider.value) ;
        PlayerPrefs.SetFloat("sliderBrightness", Brightness.intensity);
        PlayerPrefs.Save();
        

    }

    public void OnDestroy()
    {
        
        PlayerPrefs.SetFloat("sliderBrightness", Brightness.intensity);
        PlayerPrefs.Save();
    }



}
