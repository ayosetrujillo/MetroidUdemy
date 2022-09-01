using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{

    public int totalHP;
    public GameObject deathEffect;

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.O)) { AddDamage(1);  }
    }

    public void AddDamage(int damage)
    {
        totalHP -= damage;

        if(totalHP <= 0)
        {
            if(deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, transform.rotation);
            }

            Destroy(gameObject);
        }
    }
}
