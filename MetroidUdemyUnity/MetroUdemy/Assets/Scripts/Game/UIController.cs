using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public Slider hpBar;
    public GameObject fadeFX;   
    public GameObject pausePanel;
    public string titleScreenScene;

    private Animator _animatorFadeFX;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            _animatorFadeFX = fadeFX.GetComponent<Animator>();
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void Start()
    {
        FadeOut();
        pausePanel.SetActive(false);
    }


    void Update() {

        //Pause Game
        if(Input.GetKey(KeyCode.Escape)) { PauseGame(); }
    }

    public void UpdateHP(int currentHP, int maxHP)
    {
        hpBar.maxValue = maxHP;
        hpBar.value = currentHP;
    }


    public void FadeIn()
    {
        _animatorFadeFX.SetTrigger("FadeIn");
    }

    public void FadeOut()
    {
        _animatorFadeFX.SetTrigger("FadeOut");
    }




    // BTN ACTIONS

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        PlayerController.instance.playerCanMove = false;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        PlayerController.instance.playerCanMove = true;
    }

    public void TitleScreen()
    {
        Time.timeScale = 1f;
        UIController.instance.FadeIn();
        SceneManager.LoadScene(titleScreenScene);
    }

    public void QuitGame()
    {

        Application.Quit();

        Debug.Log("QUIT GAME");
    }


}
