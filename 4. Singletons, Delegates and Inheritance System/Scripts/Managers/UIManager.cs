using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public void Awake()
    {
        instance = this;
    }

    public void OnNPCDeath()
    {
        Debug.Log("Display Death UI");
    }

    public void IncreaseExperienceUIWindow(int amount)
    {
        Debug.Log("Show EXP bar increasing");
        Debug.Log("Display amount of EXP gained");
    }
}
