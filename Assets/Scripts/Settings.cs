using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    [Header("UI")]
    public Slider musicSlider;
    public Slider volumeSlider;
    public Slider sensitivitySlider;

    [Header("Audio Sources")]
    public AudioSource[] audioSources; 
    public AudioSource musicAudioSource;


    [Header("Settings")]
    private const string VolumeKey = "Volume";
    private const string MusicKey = "Music";
    private const string SensitivityKey = "MouseSensitivity";
    private const int SensitivityInteger = 7;

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 1f);
        ApplyGeneralVolume(savedVolume);

        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }

        float savedMusic = PlayerPrefs.GetFloat(MusicKey, 1f);
        ApplyMusicVolume(savedMusic);

        if (musicSlider != null)
        {
            musicSlider.value = savedMusic;
            musicSlider.onValueChanged.AddListener(OnMusicChanged);
        }

        float savedSensitivity = PlayerPrefs.GetFloat(SensitivityKey, 2f * SensitivityInteger);
        if (sensitivitySlider != null)
        {
            sensitivitySlider.value = savedSensitivity / SensitivityInteger;
            sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
        }
    }

    void OnVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat(VolumeKey, value);
        PlayerPrefs.Save();
        ApplyGeneralVolume(value);
    }

    void OnMusicChanged(float value)
    {
        PlayerPrefs.SetFloat(MusicKey, value);
        PlayerPrefs.Save();
        ApplyMusicVolume(value);
    }

    void ApplyGeneralVolume(float volume)
    {
        foreach (AudioSource source in audioSources)
        {
            if (source != null && source != musicAudioSource)
            {
                source.volume = volume;
            }
        }
    }

    void ApplyMusicVolume(float volume)
    {
        if (musicAudioSource != null)
        {
            musicAudioSource.volume = volume;
        }
    }


    void OnSensitivityChanged(float value)
    {
        PlayerPrefs.SetFloat(SensitivityKey, value * SensitivityInteger);
        PlayerPrefs.Save();
    }

    public void Back()
    {
        SceneManager.LoadScene("Main Menu");
    }
}




/*using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.LowLevel;

public class Settings : MonoBehaviour
{
    //volume
    public Slider volumeSlider;
    public AudioSource audioSource;
    private const string VolumeKey = "Volume"; // slot to save volume settings

    //mouse sensivity 
    public Slider sensitivitySlider;
    private const string SensitivityKey = "MouseSensitivity"; // slot to save sensivity settings
    private int sensivityInteger = 7; // integer to connect slider and sensivity values more smooth between each other

    void Start() //show accurate slider values on screen when settings scene is open
    {
        //volume
        if (PlayerPrefs.HasKey(VolumeKey)) // if there is a saved volume value
        {
            float savedVolume = PlayerPrefs.GetFloat(VolumeKey); //get the value from save
            audioSource.volume = savedVolume; //change volume
            volumeSlider.value = savedVolume; //change slider as well
        }
        else // if there is no save
        {
            volumeSlider.value = audioSource.volume; // set slider volume value the same as current volume value 
        }
        volumeSlider.onValueChanged.AddListener(ChangeVolume); 


        //mouse sensivity
        if (PlayerPrefs.HasKey(SensitivityKey)) //get the value from save
        {
            float savedSensitivity = PlayerPrefs.GetFloat(SensitivityKey); //get saved sensivity value
            sensitivitySlider.value = savedSensitivity/sensivityInteger; //change slider based on save
        }
        else // if there is no save
        {
            sensitivitySlider.value = 2f; //set slider value
        }
        sensitivitySlider.onValueChanged.AddListener(ChangeSensitivity);
    }

    void ChangeVolume(float value)
    {
        //set value
        audioSource.volume = value;
        Debug.Log(audioSource.volume); //check if it works

        //save value
        PlayerPrefs.SetFloat(VolumeKey, value); 
        PlayerPrefs.Save();
    }

    void ChangeSensitivity(float value)
    {
        //save value
        PlayerPrefs.SetFloat(SensitivityKey, value*sensivityInteger);
        PlayerPrefs.Save();
    }



    public void Back() // this is set on BACK button
    {
        SceneManager.LoadScene("Main Menu"); //go back to main menu
    }





 
}
*/