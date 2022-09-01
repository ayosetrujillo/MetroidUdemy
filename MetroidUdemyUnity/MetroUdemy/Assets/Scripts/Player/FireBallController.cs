using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallController : MonoBehaviour
{
    public static FireBallController instance;

    public int damageBullet = 1; 
    public float bulletLifeTime;
    public Vector2 bulletDir;
    public GameObject bulletImpactFX;

    private PlayerHealthController _playerHP;

    public float bulletSpeed;

    private Rigidbody2D _bulletRigid2D;

    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        _bulletRigid2D = GetComponent<Rigidbody2D>();
        StartCoroutine("DestroyBullet");

        // rotation to the player
        Vector3 direction = transform.position - PlayerHealthController.instance.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }

    // Update is called once per frame
    void Update()
    {
        _bulletRigid2D.velocity = -transform.right * bulletSpeed;  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);

        Instantiate(bulletImpactFX, transform.position, Quaternion.identity);

        if(collision.CompareTag("Player"))
        {
            _playerHP = collision.GetComponentInParent<PlayerHealthController>();
            Debug.Log(collision.tag + "");
            _playerHP.AddDamage(damageBullet);
        }

        
    } 


    IEnumerator DestroyBullet()
    {
        //Debug.Log("SHOOT");
        yield return new WaitForSeconds(bulletLifeTime);
        Destroy(gameObject);
    }
}
