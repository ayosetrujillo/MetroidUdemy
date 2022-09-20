using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    private void Start()
    {
        if(PlayerPrefs.HasKey("HasBossKey"))
        {
            Destroy(gameObject);
        }   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            UIController.instance.bossKey.SetActive(true);
            UIController.instance.hasBossKey = true;

            PlayerPrefs.SetInt("HasBossKey", 1);

            Destroy(gameObject);
        }
    }
}
