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
    private GameObject _player;
    private PlayerController _playerController;

    //Key
    public bool hasBossKey;
    public GameObject bossKey;

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

    void Start()
    {
        FadeOut();
        pausePanel.SetActive(false);
        AudioManagerController.instance.PlayMainTheme();
        _player = GameObject.Find("Player");
        _playerController = _player.GetComponent<PlayerController>();

        //Spawn with Continue Button
        if (PlayerPrefs.HasKey("LastScene"))
        {
            _player.transform.position = new Vector3(PlayerPrefs.GetFloat("PosX"), PlayerPrefs.GetFloat("PosY"), PlayerPrefs.GetFloat("PosZ"));
        }

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
    
        _playerController.playerCanMove = false;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;

        _playerController.playerCanMove = true;
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
