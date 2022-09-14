using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorEvents : MonoBehaviour
{
    public void AttackStart()
    {
        PlayerController.instance.playerIsAttacking = true;
        PlayerHealthController.instance.inmunity = true;
    }


    public void AttackEnd()
    {
        PlayerController.instance.playerIsAttacking = false;
        PlayerHealthController.instance.inmunity = false;
    }
}
