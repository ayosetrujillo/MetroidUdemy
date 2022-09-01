using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    [Header("Player HP Setting")]
    public int maxHP;
    public int currentHP;
    public bool inmunity;
    public float timeInmunity;
    public GameObject deathFX;
    public GameObject damageFX;

    public bool playerIsDead = false;

    private SpriteRenderer _playerSprite;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            _playerSprite = GetComponentInChildren<SpriteRenderer>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            //currentHP = maxHP;
            UIController.instance.UpdateHP(currentHP, maxHP);
        }
            
    }

    void Start()
    {

    }

    void Update()
    {
        UIController.instance.UpdateHP(currentHP, maxHP);

        if (Input.GetKeyDown(KeyCode.P)) { AddDamage(1);  }
    }

    public void AddDamage(int damage)
    {
        if (!inmunity)
        {
            inmunity = true;

            Debug.Log("INMUNITY");

            currentHP -= damage;

            if(currentHP > 0)
            {
                Instantiate(damageFX, transform.position, transform.rotation);
                StartCoroutine("HurtFX");
            }

            if (currentHP <= 0)
            {
                if (deathFX != null)
                {
                    Instantiate(deathFX, transform.position, transform.rotation);
                    UIController.instance.FadeIn();
                }

                playerIsDead = true;

                Debug.Log("RESPAWN");
                RespawnController.instance.Respawn();
                inmunity = false;
            }

           UIController.instance.UpdateHP(currentHP, maxHP);
        }
    }


    IEnumerator HurtFX()
    {
        _playerSprite.color = Color.red;
        yield return new WaitForSeconds(timeInmunity);
        _playerSprite.color = Color.white;
        inmunity = false;
       
    }

    public void RefillHP()
    {
        if(playerIsDead) { currentHP = maxHP; } 
        UIController.instance.UpdateHP(currentHP, maxHP);
    }

    public void HealPlayer(int amountHP)
    {
        currentHP += amountHP;

        if(currentHP > maxHP)
        {
            currentHP = maxHP;
        }

        UIController.instance.UpdateHP(currentHP, maxHP);
    }

}
