using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthController : MonoBehaviour
{
    public static BossHealthController instance;

    public int maxHP;
    public int currentHP;
    public GameObject deathEffect;
    public GameObject damageFX;
    public SpriteRenderer _bossSprite;

    public Slider hpBar;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T)) { AddDamage(1);  }

        hpBar.maxValue = maxHP;
        hpBar.value = currentHP;
    }

    public void AddDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP > 0)
        {
            Instantiate(damageFX, transform.position, transform.rotation);
            StartCoroutine("HurtFX");
        }

        if (currentHP <= 0)
        {
            hpBar.value = 0;

            if (deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, transform.rotation);
            }
            BossBattleGhost.instance.EndBattle();
            Destroy(gameObject);
        }
    }

    IEnumerator HurtFX()
    {
        _bossSprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        _bossSprite.color = Color.white;
    }


}
