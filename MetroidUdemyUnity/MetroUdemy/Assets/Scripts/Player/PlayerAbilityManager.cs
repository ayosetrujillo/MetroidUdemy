using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityManager : MonoBehaviour
{
    public static PlayerAbilityManager instance;

    [Space(10)]
    [Header("/// PLAYER ABILITIES ///")]

    public bool doubleJump;
    public bool dash;
    public bool morphBall;
    public bool dropBombs;

    private void Awake()
    {
        instance = this;

        Debug.Log("AWAKE BALL=" + PlayerPrefs.GetFloat("BallMorph"));

        if (PlayerPrefs.HasKey("LastScene"))
        {
            if(PlayerPrefs.GetFloat("BallMorph") == 1) { Debug.Log("BALL TRUE"); PlayerPrefs.SetInt("BallMorph", 1); } else { Debug.Log("BALL FALSE"); }
        }

    }


    private void Update()
    {

    }
}
