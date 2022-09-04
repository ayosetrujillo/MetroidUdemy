using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;

    //GAME STATES
    public bool newGame;
    public bool continueGame;

    [Header("Bosses")]
    public bool boss001Defeat;
    public string boss001Ref;


    private void Awake()
    {
        player = GameObject.Find("Player");

        instance = this;

       /* if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }*/
    }


    void Start()
    {    
        /*
        if (newGame == true)
        {
            Debug.Log("NEW GAME");
            player.GetComponent<PlayerAbilityManager>().doubleJump    = false;
            player.GetComponent<PlayerAbilityManager>().dash          = false;
            player.GetComponent<PlayerAbilityManager>().morphBall     = false;
            player.GetComponent<PlayerAbilityManager>().dropBombs     = false;
        }

        if (continueGame == true)
        {
            Debug.Log("CONTINUE");

            player.GetComponent<PlayerAbilityManager>().doubleJump = false;
            player.GetComponent<PlayerAbilityManager>().dash = false;
            player.GetComponent<PlayerAbilityManager>().morphBall = true;
            player.GetComponent<PlayerAbilityManager>().dropBombs = false;

        }

        */


    }

    // Update is called once per frame
    void Update()
    {


        
    }
}
