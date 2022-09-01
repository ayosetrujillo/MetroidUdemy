using UnityEngine;

public class BombBossController : MonoBehaviour
{

    [Header("Bomb Settings:")]

    [SerializeField] private float _timeToExplode;
    [SerializeField] private float _explosionRange;
    [SerializeField] private GameObject _prefabExplosionFX;
    [SerializeField] private LayerMask _destructibleLayer;
    [SerializeField] private PlayerHealthController _playerHP;


    void Start() {   }

    void Update()
    {
        _timeToExplode -= Time.deltaTime;

        if(_prefabExplosionFX != null)
        {
            if (_timeToExplode <= 0)
            {
                Instantiate(_prefabExplosionFX, transform.position, Quaternion.identity);
                Destroy(gameObject);

                // EXPLOSION RANGE

                Collider2D[] playerContact = Physics2D.OverlapCircleAll(transform.position, _explosionRange, _destructibleLayer);

                if(playerContact.Length > 0)
                {
                    foreach(Collider2D col in playerContact)
                    {
                        if(col.CompareTag("Player"))
                        {
                            _playerHP = col.GetComponentInParent<PlayerHealthController>();
                            _playerHP.AddDamage(2);
                        }
                    }
                }
            }
        } else {
            Debug.Log("PREFAB EXPLOSION LOST");
        }

        
    }
}
