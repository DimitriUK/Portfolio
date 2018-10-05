using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainApp : MonoBehaviour
{
    [Header("UI Elements")]
    private Image _bgImg;
    public Sprite[] sprites;
    private Text soundText;
    private Text musicText;
    private Image soundIcon;
    private Image musicIcon;

    [Header("Bools")]
    private bool isSoundOn;
    private bool isMusicOn;

    [Header("Audio Components")]
    public AudioMixerGroup masterAudio;
    private AudioSource effectsAudio;
    private AudioSource musicAudio;

    [Header("Audio Sounds")]
    public AudioClip honkSound;

    void Start()
    {
        _bgImg = GameObject.Find("Canvas/_FadeBackgroundIMG").GetComponent<Image>();
        soundIcon = GameObject.Find("Canvas/_SoundButton").GetComponent<Image>();
        musicIcon = GameObject.Find("Canvas/_MusicButton").GetComponent<Image>();
        soundText = GameObject.Find("Canvas/_SoundButton/Text").GetComponent<Text>();
        musicText = GameObject.Find("Canvas/_MusicButton/Text").GetComponent<Text>();

        _bgImg.CrossFadeAlpha(0, 5, true);
        effectsAudio = GetComponent<AudioSource>();
        musicAudio = transform.GetChild(0).GetComponent<AudioSource>();

        if (PlayerPrefs.HasKey("effects"))
        {
            int effectsVol = PlayerPrefs.GetInt("effects");
            int musicVol = PlayerPrefs.GetInt("music");

            if (effectsVol == 0)
            {
                isSoundOn = false;
            }
            else if (effectsVol == 1)
            {
                isSoundOn = true;
            }
            if (musicVol == 0)
            {
                isMusicOn = false;
            }
            else if (musicVol == 1)
            {
                isMusicOn = true;
            }
        }

        else
        {
            PlayerPrefs.SetInt("music", 1);
            isMusicOn = true;
            isSoundOn = true;
            masterAudio.audioMixer.SetFloat("Effects", 0);
            masterAudio.audioMixer.SetFloat("Music", 0);
            PlayerPrefs.SetInt("effects", 1);
        }

        CheckPlayerSettings();
    }

    public void PlayHonkSound()
    {
        effectsAudio.PlayOneShot(honkSound);
    }

    public void ToggleEffects()
    {
        isSoundOn = !isSoundOn;
    }

    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
    }

    public void CheckPlayerSettings()
    {
        if (isSoundOn)
        {
            masterAudio.audioMixer.SetFloat("Effects", 0);
            PlayerPrefs.SetInt("effects", 1);
            soundText.text = "Sound On";
            soundIcon.sprite = sprites[0];

        }
        else
        {
            masterAudio.audioMixer.SetFloat("Effects", -80);
            PlayerPrefs.SetInt("effects", -80);
            soundText.text = "Sound Off";
            soundIcon.sprite = sprites[1];
        }

        if (isMusicOn)
        {
            masterAudio.audioMixer.SetFloat("Music", 0);
            PlayerPrefs.SetInt("music", 1);
            musicText.text = "Music On";
            musicIcon.sprite = sprites[2];
        }
        else
        {
            masterAudio.audioMixer.SetFloat("Music", -80);
            PlayerPrefs.SetInt("music", -80);
            musicText.text = "Music Off";
            musicIcon.sprite = sprites[3];
        }
    }
}
