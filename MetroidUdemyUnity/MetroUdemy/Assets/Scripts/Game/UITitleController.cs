using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UITitleController : MonoBehaviour
{
    public GameObject fadeFX;
    public string startGameScene;

    public Animator _animatorFadeFX;


    private void Start()
    {
        FadeOut();
        AudioManagerController.instance.PlayTitleTheme();
    }


    void Update() { }

    public void FadeIn()
    {
        _animatorFadeFX.SetTrigger("FadeIn");
    }

    public void FadeOut()
    {
        _animatorFadeFX.SetTrigger("FadeOut");
    }

    // BTN ACTIONS

    public void StartGame()
    {
        SceneManager.LoadScene(startGameScene);
    }

    public void QuitGame()
    {

        Application.Quit();

        Debug.Log("QUIT GAME");
    }


}
