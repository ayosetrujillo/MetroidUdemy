using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int amountHP;
    public GameObject healFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealthController.instance.HealPlayer(amountHP);
            //SFX
            AudioManagerController.instance.PlaySFXPitch(15);

            if (healFX != null)
            {
                Instantiate(healFX, transform.position, Quaternion.identity);
            }
            
            Destroy(gameObject, 0.1f);

        }
    }
}
