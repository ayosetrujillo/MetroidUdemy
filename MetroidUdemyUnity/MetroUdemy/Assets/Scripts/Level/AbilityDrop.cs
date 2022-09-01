using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityDrop : MonoBehaviour
{
    [Header("Ability to upgrade")]

    public bool doubleJump;
    public bool dash;
    public bool morphBall;
    public bool dropBombs;

    [Header("Message")]
    public float messageTime;
    public GameObject messageObject;

    public string message;

    [SerializeField] private GameObject _prefabDropFX;

    private PlayerAbilityManager _playerAbilityManager;
    private Collider2D _collider2D;
    private SpriteRenderer _spriteRenderer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            _playerAbilityManager = collision.GetComponentInParent<PlayerAbilityManager>();
            //SFX
            AudioManagerController.instance.PlaySFXPitch(14);

            if (doubleJump) { _playerAbilityManager.doubleJump  = true; Instantiate(_prefabDropFX, transform.position, Quaternion.identity); StartCoroutine("PrompMessageAndDestroy"); }
            if (dash)       { _playerAbilityManager.dash        = true; Instantiate(_prefabDropFX, transform.position, Quaternion.identity); StartCoroutine("PrompMessageAndDestroy"); }
            if (morphBall)  { _playerAbilityManager.morphBall   = true; Instantiate(_prefabDropFX, transform.position, Quaternion.identity); StartCoroutine("PrompMessageAndDestroy"); }
            if (dropBombs)  { _playerAbilityManager.dropBombs   = true; Instantiate(_prefabDropFX, transform.position, Quaternion.identity); StartCoroutine("PrompMessageAndDestroy"); }
        }
    }


    IEnumerator PrompMessageAndDestroy()
    {
        Collider2D _collider2D = GetComponent<Collider2D>();
        SpriteRenderer _spriteRenderer = GetComponent<SpriteRenderer>();
        TMP_Text messageTM_Pro = messageObject.GetComponent<TextMeshProUGUI>();

        _collider2D.enabled = false;
        _spriteRenderer.enabled = false;
        messageTM_Pro.text = message;

        messageObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        messageObject.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        messageObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        messageObject.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        messageObject.SetActive(true);
        yield return new WaitForSeconds(messageTime);
        Destroy(gameObject);
    }

}
