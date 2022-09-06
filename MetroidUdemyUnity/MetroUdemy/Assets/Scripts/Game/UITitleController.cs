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
    public Animator ship;

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
        PlayerPrefs.DeleteAll();
        StartCoroutine("StartTheGame");

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


    private IEnumerator StartTheGame()
    {
        ship.SetTrigger("StartEngine");

        yield return new WaitForSeconds(1f);

        ship.SetTrigger("GoGame");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(startGameScene);

    }

}
