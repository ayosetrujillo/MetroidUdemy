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

        StartCoroutine("DamageFX");

        if (totalHP <= 0)
        {
            //SFX
            AudioManagerController.instance.PlaySFX(13);

            if (deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, transform.rotation);
            }

            Destroy(gameObject);
        }
    }



    public void AutoKill()
    {
        StartCoroutine("DamageFX");

        //SFX
        AudioManagerController.instance.PlaySFX(13);

        Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }


    IEnumerator DamageFX()
    {
        SpriteRenderer enemySprite;
        enemySprite = GetComponentInChildren<SpriteRenderer>();
        enemySprite.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        enemySprite.color = Color.white;

    }
}
