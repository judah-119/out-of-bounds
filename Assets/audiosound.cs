using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider volumeSlider;

    void Start()
    {
        LoadVolume();
    }

    public void SetVolume(float sliderValue)
    {
        float volume = Mathf.Log10(sliderValue) * 20;
        mixer.SetFloat("MasterVolume", volume);

        PlayerPrefs.SetFloat("MasterVolume", sliderValue);
        PlayerPrefs.Save();
    }

    void LoadVolume()
    {
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);

        // Set slider without triggering duplicate logic issues
        volumeSlider.value = savedVolume;

        // Apply it to mixer
        float volume = Mathf.Log10(savedVolume) * 20;
        mixer.SetFloat("MasterVolume", volume);
    }
}