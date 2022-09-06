using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleProps : MonoBehaviour
{
    public int totalHitsToBreak;
    public GameObject breakEffect;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BullePlayer")) {

            Debug.Log("" + collision.tag);

            AddDamage(totalHitsToBreak);
        }
    }




    public void AddDamage(int damage)
    {
        totalHitsToBreak -= damage;

        if (totalHitsToBreak <= 0)
        {
            //SFX
            //AudioManagerController.instance.PlaySFXPitch(13);

            if (breakEffect != null)
            {
                Instantiate(breakEffect, transform.position, transform.rotation);
            }

            Destroy(gameObject);
        }
    }
}


