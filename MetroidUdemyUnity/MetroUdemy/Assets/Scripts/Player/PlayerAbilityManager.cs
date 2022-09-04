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
    }


    private void Update()
    {

    }
}
