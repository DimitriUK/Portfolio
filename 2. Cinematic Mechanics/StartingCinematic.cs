using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class StartingCinematic : MonoBehaviour
{

    public Text mainText;
    public Text subText;
    public Image blackBg;

    public AudioMixerGroup effects;
    public AudioMixerGroup music;

    public bool diagnosticsDone;
    public bool gameStarting;
    public bool newGame;

    public GameObject[] disable;
    public GameObject[] enable;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(FirstCinematic());
        StartCoroutine(TheDiagnostics());
        subText.CrossFadeAlpha(0, 0, true);
        AudioListener.volume = 0;
        effects.audioMixer.SetFloat("Effects", -5);

        EnviroSky.instance.AudioSourceAmbient.audiosrc.outputAudioMixerGroup = effects;
        EnviroSky.instance.AudioSourceAmbient2.audiosrc.outputAudioMixerGroup = effects;
        EnviroSky.instance.AudioSourceWeather.audiosrc.outputAudioMixerGroup = effects;
        EnviroSky.instance.AudioSourceWeather2.audiosrc.outputAudioMixerGroup = effects;
        EnviroSky.instance.AudioSourceZone.audiosrc.outputAudioMixerGroup = effects;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Time.time);

        if (newGame)
        {
            AudioListener.volume += 0.009f;
        }

        if (AudioListener.volume < 1)
        {
            AudioListener.volume += 0.005f;
        }
        Debug.Log(AudioListener.volume);

        if (AudioListener.volume > 1)
        {
            AudioListener.volume = 1;
        }

        if (Time.time > 30.9f)
        {
            blackBg.CrossFadeAlpha(2, 2, true);

            if (!gameStarting)
            {
                StartCoroutine(BeginGame());
                gameStarting = true;
            }
        }

        if (Time.time > 19 && Time.time < 20)
        {
            blackBg.CrossFadeAlpha(0, 0, true);
        }

        if (gameStarting)
        {
            AudioListener.volume -= 0.009f;
        }
    }

    public IEnumerator FirstCinematic()
    {
        blackBg.CrossFadeAlpha(0, 10, true);
        yield return new WaitForSeconds(19);
        effects.audioMixer.SetFloat("Effects", -10);

        if (!diagnosticsDone)
        {
            mainText.text = "running  diagnostics...";
            yield return new WaitForSeconds(0.2f);
            mainText.text = "running  diagnostics.";
            yield return new WaitForSeconds(0.2f);
            mainText.text = "running  diagnostics..";
            yield return new WaitForSeconds(0.2f);
            mainText.text = "running  diagnostics...";
            yield return new WaitForSeconds(0.2f);
            mainText.text = "running  diagnostics.";
            yield return new WaitForSeconds(0.2f);
            mainText.text = "running  diagnostics..";
            yield return new WaitForSeconds(0.2f);
            mainText.text = "running  diagnostics...";
            yield return new WaitForSeconds(0.2f);
            mainText.text = "running  diagnostics.";
            yield return new WaitForSeconds(0.2f);
            mainText.text = "running  diagnostics..";
            yield return new WaitForSeconds(0.2f);
            mainText.text = "running  diagnostics...";
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(FirstCinematic());
        }
        else
        {
            mainText.text = "Modules:  0/10  Repaired";
            subText.text = "Life  Support";
            subText.CrossFadeAlpha(1, 0.1f, true);
            yield return new WaitForSeconds(0.5f);
            mainText.text = "Modules:  1/10  Repaired";
            subText.text = "Life  Support  Initialized";
            yield return new WaitForSeconds(1);
            mainText.text = "Modules:  1/10  Repaired";
            subText.text = "Electrical  Systems";
            subText.CrossFadeAlpha(1, 0.1f, true);
            yield return new WaitForSeconds(0.5f);
            mainText.text = "Modules:  2/10  Repaired";
            subText.text = "Electrical  Systems  Initialized";
            yield return new WaitForSeconds(1);
            mainText.text = "Modules:  2/10  Repaired";
            subText.text = "Fuel  Systems";
            subText.CrossFadeAlpha(1, 0.1f, true);
            yield return new WaitForSeconds(0.5f);
            mainText.text = "Modules:  3/10  Repaired";
            subText.text = "Fuel  Systems  Initialized";
        }
    }

    public IEnumerator TheDiagnostics()
    {
        yield return new WaitForSeconds(21);
        diagnosticsDone = true;
        StartCoroutine(FirstCinematic());
    }

    public IEnumerator BeginGame()
    {
        yield return new WaitForSeconds(3.1f);

        foreach (GameObject obj in enable)
        {
            blackBg.CrossFadeAlpha(0, 5, true);
            yield return new WaitForSeconds(1);
            newGame = true;
            blackBg.CrossFadeAlpha(0, 5, true);
            yield return new WaitForSeconds(2);
            blackBg.CrossFadeAlpha(0, 5, true);
            SceneManager.LoadScene("OpenWorldMainTPC");
        }
    }
}
