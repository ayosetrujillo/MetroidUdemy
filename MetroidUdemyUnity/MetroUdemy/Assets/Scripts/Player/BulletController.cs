using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public int damageBullet = 1; 
    public float bulletLifeTime;
    public Vector2 bulletDir;
    public GameObject bulletImpactFX;

    private EnemyHealthController _enemyHP;
    private BossHealthController _bossHP;

    [SerializeField] private float _bulletSpeed;

    private Rigidbody2D _bulletRigid2D;
    

    void Start()
    {
        _bulletRigid2D = GetComponent<Rigidbody2D>();
        StartCoroutine("DestroyBullet");
    }

    // Update is called once per frame
    void Update()
    {
        _bulletRigid2D.velocity = _bulletSpeed * bulletDir;  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        Instantiate(bulletImpactFX, transform.position, Quaternion.identity);

        //SFX
        AudioManagerController.instance.PlaySFXPitch(12);

        if (collision.CompareTag("Enemy"))
        {
            _enemyHP = collision.GetComponent<EnemyHealthController>();
            _enemyHP.AddDamage(damageBullet);
        }


        if (collision.CompareTag("Boss"))
        {
            _bossHP = collision.GetComponent<BossHealthController>();
            _bossHP.AddDamage(damageBullet);
        }


    } 


    IEnumerator DestroyBullet()
    {
        //Debug.Log("SHOOT");
        yield return new WaitForSeconds(bulletLifeTime);
        Destroy(gameObject);
    }
}
