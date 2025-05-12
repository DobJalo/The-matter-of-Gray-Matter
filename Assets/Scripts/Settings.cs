using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.LowLevel;

public class Settings : MonoBehaviour
{
    //volume
    public Slider volumeSlider;
    public AudioSource audioSource;
    private const string VolumeKey = "Volume"; //save slot

    //mouse sensivity 
    public Slider sensitivitySlider;
    private const string SensitivityKey = "MouseSensitivity"; //save slot
    private int sensivityInteger = 7;

    void Start()
    {
        //volume
        if (PlayerPrefs.HasKey(VolumeKey))
        {
            float savedVolume = PlayerPrefs.GetFloat(VolumeKey);
            audioSource.volume = savedVolume;
            volumeSlider.value = savedVolume;
        }
        else
        {
            volumeSlider.value = audioSource.volume;
        }
        volumeSlider.onValueChanged.AddListener(ChangeVolume);


        //mouse sensivity
        if (PlayerPrefs.HasKey(SensitivityKey))
        {
            float savedSensitivity = PlayerPrefs.GetFloat(SensitivityKey);
            sensitivitySlider.value = savedSensitivity/sensivityInteger;
        }
        else
        {
            sensitivitySlider.value = 2f; 
        }
        sensitivitySlider.onValueChanged.AddListener(ChangeSensitivity);
    }

    void ChangeVolume(float value)
    {
        audioSource.volume = value;
        Debug.Log(audioSource.volume);
        PlayerPrefs.SetFloat(VolumeKey, value); 
        PlayerPrefs.Save();
    }

    void ChangeSensitivity(float value)
    {
        PlayerPrefs.SetFloat(SensitivityKey, value*sensivityInteger);
        PlayerPrefs.Save();
    }

    public void Back()
    {
        SceneManager.LoadScene("Main Menu");
    }





 
}
