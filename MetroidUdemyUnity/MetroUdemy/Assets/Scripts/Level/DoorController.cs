using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    [SerializeField] private float _distanceToOpen;
    [SerializeField] private bool _playerExiting;
    private PlayerController _player;
    private Animator _doorAnimator;
   

    // Door 
    public bool doorOpen;
    public Transform exitPoint;
    public float moveTowardSpeed;
    public bool needKey;

    public string nextScene;

    void Start()
    {
        _player = PlayerHealthController.instance.gameObject.GetComponent<PlayerController>();
        _doorAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if(needKey)
        {

            if (PlayerPrefs.HasKey("HasBossKey") && UIController.instance.hasBossKey) {


                // DON'T NEED KEY, THE DOOR IS OPEN AUTOMATICALLY
                if (Vector3.Distance(transform.position, _player.transform.position) <= _distanceToOpen)
                {
                    doorOpen = true;
                }
                else
                {
                    doorOpen = false;
                }

                if (_playerExiting)
                {
                    _player.transform.position = Vector2.MoveTowards(_player.transform.position, exitPoint.position, moveTowardSpeed * Time.deltaTime);
                }

                //Animation
                _doorAnimator.SetBool("isOpen", doorOpen);


            } else  { // THE PLAYER DON'T HAVE THE KEY

                doorOpen = false;

                //Animation
                _doorAnimator.SetBool("isOpen", doorOpen);

            }



        } else { // DON'T NEED KEY, THE DOOR IS OPEN AUTOMATICALLY


            if (Vector3.Distance(transform.position, _player.transform.position) <= _distanceToOpen)
            {
                doorOpen = true;
            }
            else
            {
                doorOpen = false;
            }

            if (_playerExiting)
            {
                _player.transform.position = Vector2.MoveTowards(_player.transform.position, exitPoint.position, moveTowardSpeed * Time.deltaTime);
            }

            //Animation
            _doorAnimator.SetBool("isOpen", doorOpen);

        }
        



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if(needKey)
        {

            if (UIController.instance.hasBossKey)
            {
                if (collision.CompareTag("Player"))
                {
                    if (!_playerExiting)
                    {
                        _player.playerCanMove = false;

                        StartCoroutine("UseDoor");
                    }
                }
            }


        } else {

            if (collision.CompareTag("Player"))
            {
                if (!_playerExiting)
                {
                    _player.playerCanMove = false;

                    StartCoroutine("UseDoor");
                }
            }

        }







    }


    IEnumerator UseDoor()
    {
        _playerExiting = true;
        UIController.instance.FadeIn();

        yield return new WaitForSeconds(1f);

        UIController.instance.FadeOut();
        RespawnController.instance.SetNewRespawm(exitPoint.position);
        _playerExiting = false;
        _player.playerCanMove = true;

        SceneManager.LoadScene(nextScene);
    }
}
