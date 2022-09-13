using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackController : MonoBehaviour
{
    public GameObject bulletImpactFX;
    public int damageMelee;

    private EnemyHealthController _enemyHP;
    private BossHealthController _bossHP;

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.CompareTag("Enemy"))
        {
            _enemyHP = collision.GetComponent<EnemyHealthController>();
            _enemyHP.AddDamage(damageMelee);

            Instantiate(bulletImpactFX, transform.position, Quaternion.identity);

            //SFX
            AudioManagerController.instance.PlaySFXPitch(12);
        }


        if (collision.CompareTag("Boss"))
        {
            _bossHP = collision.GetComponent<BossHealthController>();
            _bossHP.AddDamage(damageMelee);


            Instantiate(bulletImpactFX, transform.position, Quaternion.identity);

            //SFX
            AudioManagerController.instance.PlaySFXPitch(12);
        }


    }


}



