using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTrigger : MonoBehaviour
{

    public GameObject battleObject;
    public string bossRef;

    void Awake()
    {
        if(battleObject != null)
        {
            battleObject.SetActive(false);

            // IF BOSS HAS BEEN DEFEATED
            if (PlayerPrefs.HasKey(bossRef))
            {
                if (PlayerPrefs.GetInt(bossRef) == 1)
                {
                    gameObject.GetComponent<Collider2D>().enabled = false;
                }
            }

        }
        else
        {
            Debug.Log("Battle Object No está asignado");
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            battleObject.SetActive(true);
            gameObject.SetActive(false);

            Debug.Log("BOSS BATTLE STARTED");
        }
    }
}
