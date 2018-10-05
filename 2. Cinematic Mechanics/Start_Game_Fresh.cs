using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Invector;
using UnityEngine.Audio;

public class Start_Game_Fresh : MonoBehaviour
{

    public Image blackBg;
    public Text mainText;
    public bool isOpen;

    public AudioMixerGroup effects;
    public AudioMixerGroup music;

    public Invector.vShooter.vShooterMeleeInput controls;
    public Invector.vCamera.vThirdPersonCamera camSettings;

    public FogVolume fv;

    // Use this for initialization
    void Start()
    {
        AudioListener.volume = 0;        
        mainText.CrossFadeAlpha(0, 0, true);
        mainText.CrossFadeAlpha(1, 3, true);
        StartCoroutine(FirstPart());

        effects.audioMixer.SetFloat("Effects", -5);

        EnviroSky.instance.AudioSourceAmbient.audiosrc.outputAudioMixerGroup = effects;
        EnviroSky.instance.AudioSourceAmbient2.audiosrc.outputAudioMixerGroup = effects;
        EnviroSky.instance.AudioSourceWeather.audiosrc.outputAudioMixerGroup = effects;
        EnviroSky.instance.AudioSourceWeather2.audiosrc.outputAudioMixerGroup = effects;
        EnviroSky.instance.AudioSourceZone.audiosrc.outputAudioMixerGroup = effects;
        fv.enabled = false;
        fv.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        AudioListener.volume += 0.005f;

        if (AudioListener.volume > 1.1f)
        {
            AudioListener.volume = 1;
        }
    }
    
    public IEnumerator FirstPart()
    {
        fv.enabled = false;
        fv.enabled = true;
        yield return new WaitForSeconds(1);
        fv.enabled = false;
        fv.enabled = true;
        yield return new WaitForSeconds(2);
        blackBg.CrossFadeAlpha(0, 5, true);
        yield return new WaitForSeconds(8);
        mainText.CrossFadeAlpha(0, 1, true);        
    }
}
