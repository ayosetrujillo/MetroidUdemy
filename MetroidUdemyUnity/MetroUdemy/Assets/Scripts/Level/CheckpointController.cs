using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointController : MonoBehaviour
{
    private Animator _animator;
    private string _currentScene;
    private GameObject _player;
    public bool isActive;
    public PlayerAbilityManager playerAbility;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _currentScene = SceneManager.GetActiveScene().name.ToString();
        isActive = false;

        _player = GameObject.Find("Player");
        playerAbility = _player.GetComponent<PlayerAbilityManager>();

        if (PlayerPrefs.HasKey("LastScene"))
        {
           // StartCoroutine("LoadProgress", _player.GetComponentInChildren<Collider2D>());
        }

    }

    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponentInParent<PlayerHealthController>().TotalCureHP();

            isActive = true;
            RespawnController.instance.SetNewRespawm(collision.transform.position);
            //PlayerHealthController.instance.RefillHP();

            _animator.SetBool("Check", true);

            //SFX
            AudioManagerController.instance.PlaySFX(16);

            // Saving current progress //
            StartCoroutine("SaveProgress", _player.GetComponentInChildren<Collider2D>());
            
        }
    }

    public IEnumerator SaveProgress(Collider2D _collision)
    {
        Debug.Log("Name Scene: " + _currentScene);

        //Player Position
        PlayerPrefs.SetFloat("PosX", _collision.transform.position.x);
        PlayerPrefs.SetFloat("PosY", _collision.transform.position.y);
        PlayerPrefs.SetFloat("PosZ", _collision.transform.position.z);
        PlayerPrefs.SetString("LastScene", _currentScene);

        /*//Player Abilities
        if (playerAbility.doubleJump)   { PlayerPrefs.SetInt("DoubleJump", 1); } else   { PlayerPrefs.SetInt("DoubleJump", 0); }
        if (playerAbility.dash)         { PlayerPrefs.SetInt("Dash", 1); } else         { PlayerPrefs.SetInt("Dash", 0); }
        if (playerAbility.morphBall)    { PlayerPrefs.SetInt("BallMorph", 1); } else    { PlayerPrefs.SetInt("BallMorph", 0); }
        if (playerAbility.dropBombs)    { PlayerPrefs.SetInt("DropMorph", 1); } else    { PlayerPrefs.SetInt("DropMorph", 0); }*/

        if (playerAbility.morphBall == true) {
            PlayerPrefs.SetInt("BallMorph", 1);
        } else {
            PlayerPrefs.SetInt("BallMorph", 0);
        }

        yield return new WaitForSeconds(0.2f);

        Debug.Log("" + PlayerPrefs.GetFloat("BallMorph"));
        Debug.Log("SAVE Completed");

    }

    public IEnumerator LoadProgress(Collider2D _collision)
    {
        Debug.Log("Name Scene: " + _currentScene);

        //Player Position
        PlayerPrefs.GetFloat("PosX", _collision.transform.position.x);
        PlayerPrefs.GetFloat("PosY", _collision.transform.position.y);
        PlayerPrefs.GetFloat("PosZ", _collision.transform.position.z);

        //Player Abilities
        if (PlayerPrefs.HasKey("BallMorph")) {

            if(PlayerPrefs.GetInt("BallMorph") == 1) { Debug.Log("BALL TRUE"); } else { Debug.Log("BALL FALSE"); }
        }
      
        yield return new WaitForSeconds(0.2f);
        Debug.Log("LOAD Completed");
    }

}
