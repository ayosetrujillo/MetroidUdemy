using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleProps : MonoBehaviour
{
    public int totalHitsToBreak;
    public GameObject breakEffect;
    //public GameObject breakEffect2;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BullePlayer")) {

            Debug.Log("" + collision.tag);

            AddDamage(1);
        }
    }




    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BullePlayer"))
        {
            AddDamage(1);
        }
    }



    public void AddDamage(int damage)
    {
        totalHitsToBreak -= damage;
        Instantiate(breakEffect, transform.position, transform.rotation);

        if (totalHitsToBreak <= 0)  
        {
            //SFX
            //AudioManagerController.instance.PlaySFXPitch(13);

            Instantiate(breakEffect, transform.position, transform.rotation);
           // Instantiate(breakEffect2, transform.position, transform.rotation);
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject);
        }
    }
}   


