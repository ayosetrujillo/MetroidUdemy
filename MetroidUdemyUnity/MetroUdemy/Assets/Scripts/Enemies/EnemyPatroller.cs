using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatroller : MonoBehaviour
{
    public Transform[] patrolPoints;
    private int _currentPoint;

    public float moveSpeed;
    public float jumpForce;

    public float waitAPoints;

    private float _waitCounters;


    private Rigidbody2D _enemyRigidbody;


    private void Awake()
    {
        _enemyRigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _waitCounters = waitAPoints;


        //Desheredamos los patrolPoints del Crab para que no se muevan durante el patrolling.
        foreach(Transform wayPoints in patrolPoints)
        {
            wayPoints.SetParent(null);
        }
    }

    void Update()
    {
        // si la distancia entre el enemigo y el punto al que tiene que ir es mayor a 0.2
        if(Mathf.Abs(transform.position.x - patrolPoints[_currentPoint].position.x) > 0.2f)
        {
            //según donde esté el punto se mueve a izquierda o a derecha.
            if(transform.position.x < patrolPoints[_currentPoint].position.x)
            {
                _enemyRigidbody.velocity = new Vector2(moveSpeed, _enemyRigidbody.velocity.y);
                transform.localScale = new Vector3(-1f, 1f, 1f);
            } else {
                _enemyRigidbody.velocity = new Vector2(-moveSpeed, _enemyRigidbody.velocity.y);
                transform.localScale = new Vector3(1f, 1f, 1f);
            }

            //Si el siguiente waypoint está más alto saltará
            if( (transform.position.y < (patrolPoints[_currentPoint].position.y -0.5f)) && (_enemyRigidbody.velocity.y < 0.1f) )
            {
                _enemyRigidbody.velocity = new Vector2(_enemyRigidbody.velocity.x, jumpForce);
            }


        } else {

            _enemyRigidbody.velocity = new Vector2(0f, _enemyRigidbody.velocity.y);

            _waitCounters -= Time.deltaTime;

            if(_waitCounters <= 0)
            {
                _waitCounters = waitAPoints;

                _currentPoint++;

                if(_currentPoint >= patrolPoints.Length)
                {
                    _currentPoint = 0;
                }
                
            }

        }
        
    }
}
