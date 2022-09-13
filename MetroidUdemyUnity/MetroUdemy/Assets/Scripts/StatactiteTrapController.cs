using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatactiteTrapController : MonoBehaviour
{
    private Rigidbody2D _rigidBody2D;
    private GameObject _player;

    public float gravityForce = 10;
    public float distanceLimit = 5f;
    public Transform point;

    private void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _rigidBody2D.gravityScale = 0;

        _player = GameObject.Find("Player");
    }

    private void Update()
    {
        if(_player != null)
        {
            float distance = Vector3.Distance(_player.transform.position, point.transform.position);

            if(distance < distanceLimit)
            {
                _rigidBody2D.gravityScale = gravityForce;

                Destroy(gameObject, 2f);
            }
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerHealthController.instance.AddDamage(100);
            Destroy(gameObject);
        }
    }

}
