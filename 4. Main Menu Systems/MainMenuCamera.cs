using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuCamera : MonoBehaviour
{
    public Transform camDef;
    public Transform newGame;
    public Transform loadGame;

    public GameObject goBackNG;
    public GameObject goBackLG;

    public GameObject newGameButton;
    public GameObject startGameButton;

    public GameObject[] toDisableCamDef;

    public GameObject newGameTorch;
    public GameObject loadGameTorch;

    public SelectCharacter charScript;

    public bool isCamDef;
    public bool isNewGame;
    public bool isLoadGame;

    public bool enterPressed;
    public bool canInput;
    public bool isFading;

    public Image BlackBG;
    public Text GameTitle;
    public Canvas worldCanvas;
   
    void Start()
    {
        AudioListener.volume = 0;
        worldCanvas.enabled = false;
        StartCoroutine(MenuBegin());
    }
  
    void FixedUpdate()
    {
        if (AudioListener.volume < 0.9f && !isFading)
        {
            AudioListener.volume += 0.005f;
        }

        if (isFading)
        {
            AudioListener.volume -= 0.005f;
        }

        if (isCamDef)
        {
            isNewGame = false;
            isLoadGame = false;

            goBackNG.SetActive(false);
            goBackLG.SetActive(false);

            newGameButton.SetActive(true);
            startGameButton.SetActive(false);
            foreach (GameObject obj in toDisableCamDef)
            {
                obj.SetActive(false);
            }
            transform.position = Vector3.Lerp(transform.position, camDef.position, Time.deltaTime * 3);
            transform.rotation = Quaternion.Lerp(transform.rotation, camDef.rotation, Time.deltaTime * 3);
        }

        if (isNewGame)
        {
            isCamDef = false;
            isLoadGame = false;

            goBackNG.SetActive(true);
            goBackLG.SetActive(false);

            newGameButton.SetActive(false);
            startGameButton.SetActive(true);

            foreach (GameObject obj in toDisableCamDef)
            {
                obj.SetActive(true);
            }
            transform.position = Vector3.Lerp(transform.position, newGame.position, Time.deltaTime * 3);
            transform.rotation = Quaternion.Lerp(transform.rotation, newGame.rotation, Time.deltaTime * 3);

            newGameTorch.SetActive(true);
            loadGameTorch.SetActive(false);
        }

        if (isLoadGame)
        {
            isNewGame = false;
            isCamDef = false;

            goBackNG.SetActive(false);
            goBackLG.SetActive(true);

            transform.position = Vector3.Lerp(transform.position, loadGame.position, Time.deltaTime * 3);
            transform.rotation = Quaternion.Lerp(transform.rotation, loadGame.rotation, Time.deltaTime * 3);

            newGameTorch.SetActive(false);
            loadGameTorch.SetActive(true);
        }
    }

    public void NewGame()
    {
        isCamDef = false;
        isNewGame = true;
    }

    public void LoadGame()
    {
        //Application.LoadLevel("CombatTesting");
        isCamDef = false;
        isLoadGame = true;
    }

    public void CamDefault()
    {
        isCamDef = true;
        newGameTorch.SetActive(false);
        loadGameTorch.SetActive(false);
    }

    public void NewGameHover()
    {
        newGameTorch.SetActive(true);
    }
    public void NewGameHoverOff()
    {
        newGameTorch.SetActive(false);
    }

    public void LoadGameHover()
    {
        loadGameTorch.SetActive(true);
    }
    public void LoadGameHoverOff()
    {
        loadGameTorch.SetActive(false);
    }

    public void StartTheGame()
    {
        ES2.Save(charScript.modelNum, "ModelNum");
        ES2.Save(1, "FirstTime");
        StartCoroutine(Fade());
        isFading = true;
        worldCanvas.worldCamera = null;
    }

    public IEnumerator MenuBegin()
    {
        yield return new WaitForSeconds(1);
        BlackBG.CrossFadeAlpha(0, 5, false);
        yield return new WaitForSeconds(4);
        GameTitle.CrossFadeAlpha(1, 4, false);
        yield return new WaitForSeconds(4);
        GameTitle.CrossFadeAlpha(0, 2, false);
        yield return new WaitForSeconds(2);
        worldCanvas.enabled = true;
    }
    public IEnumerator Fade()
    {
        BlackBG.CrossFadeAlpha(1, 2, false);
        yield return new WaitForSeconds(4);
        GameObject music = GameObject.Find("_MUSIC");
        music.SetActive(false);
        SceneManager.LoadScene("Intro");
    }
}
