using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleGhost : MonoBehaviour
{
    // BATTLE
    [Header("Battle")]

    public string bossRef; // Boss Reference to save progress
    public static BossBattleGhost instance;
    public GameObject boss;
    public Transform _bossTransform;
    public GameObject doorBoss;
    public GameObject invisibleWall;
    public GameObject musicBoss;
    public bool battleActive = false;
    public bool isVanish;
    public bool bossShot;
   


    private Animator _bossAnimator;

    // CAM LOCK VAR
    [SerializeField] private CameraController _cam;
    [SerializeField] private float _camSpeed;

    [SerializeField] private Transform _camPosition;


    // BATTLE CONFIG
    
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _distance;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] public Transform nextPoint;
    [SerializeField] private float _activeTime;
    [SerializeField] private float _vanishTime;
    [Space(10)]
    [SerializeField] private float _counterActive;
    [SerializeField] private float _counterVanish;
    [Space(10)]
    [SerializeField] private GameObject _bombBoss;

    public int bossPhase;


    // BATTLE Reward

    public GameObject abilityPickUp;


    private void Awake()
    {
        instance = this;
        bossPhase = 0;

        _counterActive = _activeTime;
        _counterVanish = _vanishTime;

    }

    void Start()
    {
        battleActive = true;

        AudioManagerController.instance.PlayBossTheme();

        doorBoss.GetComponent<DoorController>().enabled = false;
        invisibleWall.SetActive(true);

        _cam = FindObjectOfType<CameraController>();
        _cam.enabled = false;

        _bossAnimator = boss.GetComponentInChildren<Animator>();
        _bossTransform = boss.GetComponent<Transform>();

        boss.transform.position = _spawnPoints[0].transform.position;
        nextPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];

    }


    void Update()
    {
        _cam.transform.position = Vector3.MoveTowards(_cam.transform.position, _camPosition.position, _camSpeed * Time.deltaTime);

        if (battleActive)
        {
            // BATTLE BOSS

            if (boss.activeSelf && bossPhase == 0)
            {
                Debug.Log("FASE 1");
                bossPhase = 1;
            }

            if ((bossPhase == 1) && (BossHealthController.instance.currentHP <= (BossHealthController.instance.maxHP / 3) * 2) && (BossHealthController.instance.currentHP >= BossHealthController.instance.maxHP / 2))
            {
                Debug.Log("FASE 2");
                bossPhase = 2;
            }

            if ((bossPhase == 2) && (BossHealthController.instance.currentHP <= (BossHealthController.instance.maxHP / 3)))
            {
                Debug.Log("FASE 3");
                bossPhase = 3;
            }


            if (bossPhase == 1)
            {
                BossVanishPhase1();
            }

            if (bossPhase == 2)
            {
                BossVanishPhase2();
            }

            if (bossPhase == 3)
            {
                BossVanishPhase3();
            }


        }
    }


    // PHASE 1 ////////////////////////////////////////////////

    public void BossVanishPhase1()
    {

        //Inital Settings

        _moveSpeed = 6f;
        _activeTime = 2.5f;
        _vanishTime = Random.Range(0.8f, 3f);

        //Timer
        _counterActive -= Time.deltaTime;

        //Shoot
        if (_counterActive <= _activeTime / 2)
        {
            if (bossShot == false)
            {
                //Boss Shooting
                _bossAnimator.SetTrigger("Fire");
                bossShot = true;
            }
        }


        //Disappear
        if (_counterActive <= 0)
        {
            isVanish = true;

            boss.GetComponent<Collider2D>().enabled = false;

            _bossAnimator.SetBool("Vanish", isVanish);

            _counterVanish -= Time.deltaTime;

            //Appear
            if (_counterVanish <= 0)
            {

                isVanish = false;
                boss.GetComponent<Collider2D>().enabled = true;

                nextPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];

                _bossTransform.position = nextPoint.position;

                _bossAnimator.SetBool("Vanish", isVanish);
                _counterActive = _activeTime;
                _counterVanish = _vanishTime;
                bossShot = false;
            }
        }
    }


    // PHASE 2 ////////////////////////////////////////////////

    public void BossVanishPhase2()
    {

        //Inital Settings

        _moveSpeed = 12f;
        _activeTime = 2f;
        _vanishTime = Random.Range(0.8f, 1.5f);

        //Timer
        _counterActive -= Time.deltaTime;

        _bossTransform.position = Vector3.MoveTowards(_bossTransform.position, nextPoint.position, _moveSpeed * Time.deltaTime);
        _distance = Vector3.Distance(_bossTransform.position, nextPoint.position);

        //Shoot
        if (bossShot == false && _distance <= 0.1f)
        {
            //Boss Shooting
            _bossAnimator.SetTrigger("DoubleFire");
            bossShot = true;
        }

        //Disappear
        if (_counterActive <= 0)
        {   
            isVanish = true;

            boss.GetComponent<Collider2D>().enabled = false;
            _bossAnimator.SetBool("Vanish", isVanish);

            _counterVanish -= Time.deltaTime;

            //Appear
            if (_counterVanish <= 0)
            {
                isVanish = false;
                boss.GetComponent<Collider2D>().enabled = true;

                _bossTransform.position = Vector3.MoveTowards(_bossTransform.position, nextPoint.position, _moveSpeed * Time.deltaTime);
                _distance = Vector3.Distance(_bossTransform.position, nextPoint.position);

                _bossAnimator.SetBool("Vanish", isVanish);
                _counterActive = _activeTime;
                _counterVanish = _vanishTime;
                bossShot = false;

                if (_distance <= 0.01f && isVanish == false)
                {
                    nextPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
                }
            }
        }
    }

    // PHASE 3 ////////////////////////////////////////////////

    public void BossVanishPhase3()
    {

        //Inital Settings
        _moveSpeed = 20f;
        _activeTime = 1f;
        _vanishTime = Random.Range(0.4f, 1.2f);


        //Timer
        _counterActive -= Time.deltaTime;

        _bossTransform.position = Vector3.MoveTowards(_bossTransform.position, nextPoint.position, _moveSpeed * Time.deltaTime);
        _distance = Vector3.Distance(_bossTransform.position, nextPoint.position);

        //Shoot
        if (bossShot == false && _distance <= 0.1f)
        {
            //Boss Shooting
            _bossAnimator.SetTrigger("DoubleFire");
            bossShot = true;
            Instantiate(_bombBoss, _bossTransform.position, Quaternion.identity);
        }


        //Disappear
        if (_counterActive <= 0)
        {
            isVanish = true;

            boss.GetComponent<Collider2D>().enabled = false;
            _bossAnimator.SetBool("Vanish", isVanish);

            _counterVanish -= Time.deltaTime;

            //Appear
            if (_counterVanish <= 0)
            {
                isVanish = false;
                boss.GetComponent<Collider2D>().enabled = true;

                _bossTransform.position = Vector3.MoveTowards(_bossTransform.position, nextPoint.position, _moveSpeed * Time.deltaTime);
                _distance = Vector3.Distance(_bossTransform.position, nextPoint.position);

                _bossAnimator.SetBool("Vanish", isVanish);
                _counterActive = _activeTime;
                _counterVanish = _vanishTime;
                bossShot = false;

                if (_distance <= 0.01f && isVanish == false)
                {
                    nextPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
                }
            }
        }
    }

    public void EndBattle()
    {
        battleActive = false;

        //SAVE Boss STATE
        PlayerPrefs.SetInt(bossRef, 1);

        StartCoroutine("EndingBattle");

        Debug.Log("END BATTLE");
    }


    public IEnumerator EndingBattle()
    {
        _bossAnimator.SetTrigger("Death");
        AudioManagerController.instance.StopAllMusic();

        yield return new WaitForSeconds(2);

        Instantiate(abilityPickUp, _spawnPoints[0].transform.position, Quaternion.identity);

        invisibleWall.SetActive(false);
        doorBoss.GetComponent<DoorController>().enabled = true;

        AudioManagerController.instance.PlayMainTheme();

        Destroy(gameObject);

    }


}
