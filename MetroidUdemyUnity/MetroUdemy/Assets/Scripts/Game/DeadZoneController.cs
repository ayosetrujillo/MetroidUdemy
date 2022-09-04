using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //collision.GetComponentInParent<PlayerHealthController>().AddDamage(1000);

            PlayerHealthController.instance.AddDamage(1000);
            PlayerHealthController.instance.playerIsDead = true;
        }
    }
}
