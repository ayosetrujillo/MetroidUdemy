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

    [SerializeField] private GameObject _continueBTN;
    private GameObject _player;

    private void Awake()
    {
        _player = GameObject.Find("Player");
    }

    private void Start()
    {
        FadeOut();
        AudioManagerController.instance.PlayTitleTheme();

        if(PlayerPrefs.HasKey("LastScene"))
        {
            _continueBTN.SetActive(true);

            Debug.Log("BALL MORHP= " + PlayerPrefs.GetInt("BallMorph"));
        }
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

    public void NewGame()
    {
        //New game
        SceneManager.LoadScene(startGameScene);
        PlayerPrefs.DeleteAll();

    }


    public void ContinueGame()
    {
        //New game
        SceneManager.LoadScene(PlayerPrefs.GetString("LastScene"));
    }

    public void QuitGame()
    {

        Application.Quit();
        Debug.Log("QUIT GAME");
    }


}
