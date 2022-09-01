using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTrigger : MonoBehaviour
{

    public GameObject battleObject;

    void Awake()
    {
        if(battleObject != null)
        {
            battleObject.SetActive(false);
        }
        else
        {
            Debug.Log("Battle Object No está asignado");
        }

    }

    void Update()
    {
        
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
