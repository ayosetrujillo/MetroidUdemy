using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyController : MonoBehaviour
{
    [Header ("Enemy Settings")]
    [SerializeField] private float _chasingArea;
    public float moveSpeed;
    public float turnSpeed;
    public bool isChasing;
    public bool isShooter;
    public GameObject bulletEnemy;
    public float coolDown;


    private Animator _animator;
    private Transform _player;
    private float _counterShoot;

    

    void Start()
    {
        _player = PlayerHealthController.instance.transform;
        _animator = GetComponentInChildren<Animator>();
        _counterShoot = coolDown;
    }

    void Update()
    {
        if(!isChasing)
        {
            if(Vector3.Distance(transform.position, _player.position) < _chasingArea)
            {
                isChasing = true;
            } 

        } else {
            if(_player.gameObject.activeSelf)
            {
                // rotation to the player
                Vector3 direction = transform.position - _player.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion targetRot = Quaternion.AngleAxis(angle, Vector3.forward);

                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, turnSpeed * Time.deltaTime);

                // move to the player
                //transform.position = Vector3.MoveTowards(transform.position, _player.position, moveSpeed * Time.deltaTime);
                transform.position += -transform.right * moveSpeed * Time.deltaTime;
            }
        }

        //Set Animation
        _animator.SetBool("Chasing", isChasing);

        if (isShooter)
        {
            _counterShoot -= Time.deltaTime;

            if (_counterShoot <= 0)
            {
                Instantiate(bulletEnemy, transform.position, Quaternion.identity);

                //SFX
                AudioManagerController.instance.PlaySFX(11);
                _counterShoot = coolDown;
            }
        }

    }
}
