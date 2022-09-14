using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int damage = 1;
    public bool kamikaze;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerHealthController.instance.AddDamage(damage);
            if(kamikaze) gameObject.GetComponent<EnemyHealthController>().AutoKill();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerHealthController.instance.AddDamage(damage);
            if(kamikaze) gameObject.GetComponent<EnemyHealthController>().AutoKill();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerHealthController.instance.AddDamage(damage);
            if(kamikaze) gameObject.GetComponent<EnemyHealthController>().AutoKill();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealthController.instance.AddDamage(damage);
            if(kamikaze) gameObject.GetComponent<EnemyHealthController>().AutoKill();
        }
    }




}
