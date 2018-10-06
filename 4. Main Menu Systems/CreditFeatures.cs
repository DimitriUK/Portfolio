using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditFeatures : MonoBehaviour
{
    public CanvasGroup cg;
    public bool canSkip;
    public bool isSkipped;
    public bool isDone;
    public bool begin;

    //This script was created for the purpose of skipping the intro parts before having access to the main menu.
   
    void Start()
    {
        cg.alpha = 0;
        StartCoroutine(Begin());
    }
   
    void Update()
    {
        if (!isDone && begin)
        {
            if (cg.alpha < 1)
            {
                cg.alpha += 0.05f;
            }
        }
        if (isSkipped)
        {
            cg.alpha -= 0.05f;
        }
        if (canSkip)
        {
            if (Input.anyKeyDown)
            {
                StopAllCoroutines();
                isSkipped = true;
                StartCoroutine(Skipped());
                canSkip = false;
            }
        }
    }

    public IEnumerator Begin()
    {
        yield return new WaitForSeconds(1);
        begin = true;
        yield return new WaitForSeconds(2);
        isDone = true;
        canSkip = true;
        yield return new WaitForSeconds(5);
        isSkipped = true;
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MainMenu");
    }

    public IEnumerator Skipped()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MainMenu");
    }
}
