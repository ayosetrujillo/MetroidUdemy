using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    // Main ref
    private Rigidbody2D _playerRigid2D;
    private Animator _playerAnimator;
    private Animator _playerBallAnimator;
    private PlayerAbilityManager _playerAbility;

    [Space(10)]
    [Header("// PLAYER MOVEMENT //")]

    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _playerJump;
    [SerializeField] private float _coyoteTime = 0.16f;
    [SerializeField] private Transform _playerGroundCheck;
    [SerializeField] private LayerMask _groundLayer;

    private float _timeInAir; //coyote time counter

    [Space(10)]
    [Header("// MAIN WEAPON //")]

    // Weapons var
    public BulletController bulletPrefab;
    public Transform shootPoint;

    [Space(10)]
    [Header("// DASH //")]

    // Dash var
    public float dashTime;
    public float dashSpeed = 20;
    private float _dashCounter;

    // Dash Cooldown var
    public float dashCoolDown = 1f;
    private float _timerCoolDown;

    // Dash effect
    public SpriteRenderer playerSprite;
    public SpriteRenderer afterImage;
    public Color afterImageColor;
    public float afterImageLifetime;
    public float timeBetweenCopies;

    private float _afterImageCounter;


    [Space(10)]
    [Header("// BALL MODE //")]

    // Ball Mode
    public GameObject playerStandObject;
    public GameObject playerBallObject;
    public BombController bombPrefab;

    [Space(5)]

    //Ball Weapon
    [Space(10)]
    [Header("// PLAYER STATES //")]

    [Space(10)]
    // Player STATES var
    public bool playerCanMove;
    public bool playerIsGrounded;
    public bool playerIsMoving;
    public bool playerIsJumping;
    public bool playerIsShooting;
    public bool playerIsDashing;
    public bool playerIsBall;
    public bool playerCanDoubleJump;
    public bool lookR = true;

    private void Awake()
    {
        _playerRigid2D = GetComponent<Rigidbody2D>();
        _playerAnimator = playerStandObject.GetComponentInChildren<Animator>();
        _playerBallAnimator = playerBallObject.GetComponent<Animator>();
        _playerAbility = GetComponent<PlayerAbilityManager>();

        instance = this;
    }

    void Start() { lookR = true; playerIsShooting = false; playerCanMove = true; }

    void Update()
    {

        // PLAYER STATES:

        // CAN'T MOVE PLAYER
        if (!playerCanMove) {
            //playerIsMoving = false;
            _playerRigid2D.velocity = Vector2.zero;
        }


        // CAN MOVE
        if (playerCanMove) {

            // Player Dash
            if (_timerCoolDown == 0) {
                if (Input.GetButtonDown("Fire2") && !playerIsBall && _playerAbility.dash) { _dashCounter = dashTime; DashEffect(); }
            }
        
            if((_dashCounter > 0 ))
            {
                playerIsDashing = true;
                _dashCounter = _dashCounter - Time.deltaTime;
                _playerRigid2D.velocity = new Vector2(dashSpeed * transform.localScale.x, _playerRigid2D.velocity.y);
                _timerCoolDown = dashCoolDown;

                //Dash Effect
                _afterImageCounter -= Time.deltaTime;
                if(_afterImageCounter <= 0)
                {
                    DashEffect();
                }

            } else {
                playerIsDashing = false;
                _dashCounter = 0;
            }

            // Cooldowm Timer
            if(_timerCoolDown > 0) {
                _timerCoolDown = _timerCoolDown - Time.deltaTime;
            } else {
                _timerCoolDown = 0;
            }

            // Player horizontal movement
            if (!playerIsDashing)
            {
                _playerRigid2D.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * _playerSpeed, _playerRigid2D.velocity.y);

                // Is Moving with flip
                if (_playerRigid2D.velocity.x < 0) { playerIsMoving = true; transform.localScale = new Vector3(-1f, 1f, 1f); lookR = false; }
                if (_playerRigid2D.velocity.x > 0) { playerIsMoving = true; transform.localScale = new Vector3(1f, 1f, 1f);  lookR = true; }

                if (_playerRigid2D.velocity.x == 0) { playerIsMoving = false; }

            }

            // Is Grounded
            playerIsGrounded = Physics2D.OverlapCircle(_playerGroundCheck.position, 0.2f, _groundLayer);
            playerIsJumping = !playerIsGrounded;

            if (playerIsGrounded)
            {
                _timeInAir = 0;
            } else {
                _timeInAir += Time.deltaTime;
            }


            // Is Jumping
            if (Input.GetButtonDown("Jump") && (playerIsGrounded || playerCanDoubleJump || _timeInAir < _coyoteTime))
            {
                if(_playerAbility.doubleJump) // double jump ability check
                {
                    if (playerIsGrounded)
                    {
                        playerCanDoubleJump = true;
                    }
                    else
                    {
                        playerCanDoubleJump = false;
                        // DOUBLE JUMP ANIMATION
                        _playerAnimator.SetTrigger("isDoubleJump");
                    }
                }
                _playerRigid2D.velocity = new Vector2(_playerRigid2D.velocity.x, _playerJump);
                playerIsJumping = true;
            }

            // Is Shooting
            if (Input.GetButtonDown("Fire1") && (!playerIsBall))
            {
                Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation).bulletDir = new Vector2(transform.localScale.x, 0f);
                playerIsShooting = true;

                // SHOOT ANIMATION
                _playerAnimator.SetTrigger("isShoot");

                // SOUND
                AudioManagerController.instance.PlaySFX(8);
            }

            if(Input.GetButtonUp("Fire1"))
            {
                playerIsShooting = false;
            }

            // Ball Mode

            if(_playerAbility.morphBall && (playerBallObject != null) && (playerBallObject != null))
            {
                if(!playerBallObject.activeSelf)
                {

                    if(Input.GetAxisRaw("Vertical") < -0.9)
                    {
                        playerStandObject.SetActive(false);
                        playerBallObject.SetActive(true);
                        playerIsBall = true;
                    } 
                } else {

                    if (Input.GetAxisRaw("Vertical") > 0.9)
                    {
                        playerStandObject.SetActive(true);
                        playerBallObject.SetActive(false);
                        playerIsBall = false;
                    }

                }



                // Is Shooting BOMB!
                if (Input.GetButtonDown("Fire1") && playerIsBall && _playerAbility.morphBall && _playerAbility.dropBombs)
                {
                    Instantiate(bombPrefab, playerBallObject.transform.position, playerBallObject.transform.rotation);
                    playerIsShooting = true;

                }

                if (Input.GetButtonUp("Fire1"))
                {
                    playerIsShooting = false;
                }
            }
        }

        // BASIC PLAYER ANIMATIONS /////

        if (!playerIsBall) {      
            _playerAnimator.SetBool("isGrounded",   playerIsGrounded);
            _playerAnimator.SetBool("isMoving",     playerIsMoving);
        }


        if (playerIsBall)
        {
            _playerBallAnimator.SetBool("isBallMoving", playerIsMoving);
        }

    }


    public void DashEffect()
    {

        AudioManagerController.instance.PlaySFX(1);

        SpriteRenderer image = Instantiate(afterImage, transform.position, transform.rotation);
        image.sprite = playerSprite.sprite;
        image.transform.localScale = transform.localScale;
        image.color = afterImageColor;

        Destroy(image.gameObject, afterImageLifetime);

        _afterImageCounter = timeBetweenCopies;
    }
    
}