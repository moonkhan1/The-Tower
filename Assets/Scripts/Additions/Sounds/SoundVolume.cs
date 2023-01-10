using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundVolume : MonoBehaviour
{
    [SerializeField] Slider _musicVolume;
    [SerializeField] Slider _soundsVolume;
    [SerializeField] AudioSource _mainTheme;
    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            LoadMusic();
        }
        else
        {
            LoadMusic();
        }
        if (!PlayerPrefs.HasKey("soundsVolume"))
        {
            PlayerPrefs.SetFloat("soundsVolume", 1);
            LoadSounds();
        }
        else
        {
            LoadSounds();
        }
    }
    private void Update() {
        // Debug.Log(_mainTheme.volume);
        // Debug.Log(_volume.value);
    }


    public void ChangeVolumeofSounds()
    {
        AudioListener.volume = _soundsVolume.value;
        SaveSounds();
    }
    public void ChangeVolumeofMusic()
    {
        _mainTheme.volume = _musicVolume.value;
        SaveMusic();
    }

    private void LoadSounds()
    {
        _soundsVolume.value = PlayerPrefs.GetFloat("soundsVolume");
    }
    private void LoadMusic()
    {
        _musicVolume.value = PlayerPrefs.GetFloat("musicVolume");
    }
    private void SaveSounds()
    {
        PlayerPrefs.SetFloat("soundsVolume", _soundsVolume.value);
    }
    private void SaveMusic()
    {
        PlayerPrefs.SetFloat("musicVolume", _musicVolume.value);
    }
}
