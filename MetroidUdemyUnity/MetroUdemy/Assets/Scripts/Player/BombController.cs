using UnityEngine;

public class BombController : MonoBehaviour
{

    [Header("Bomb Settings:")]

    [SerializeField] private float _timeToExplode;
    [SerializeField] private float _explosionRange;
    [SerializeField] private GameObject _prefabExplosionFX;
    [SerializeField] private LayerMask _destructibleLayer;

    void Start() {

        //SFX
        AudioManagerController.instance.PlaySFX(7);
    }

    void Update()
    {
        _timeToExplode -= Time.deltaTime;

        if(_prefabExplosionFX != null)
        {
            if (_timeToExplode <= 0)
            {
                Instantiate(_prefabExplosionFX, transform.position, Quaternion.identity);
                Destroy(gameObject);

                //SFX
                AudioManagerController.instance.PlaySFX(13);

                // EXPLOSION RANGE

                Collider2D[] objetcsToRemove = Physics2D.OverlapCircleAll(transform.position, _explosionRange, _destructibleLayer);

                if(objetcsToRemove.Length > 0)
                {
                    foreach(Collider2D col in objetcsToRemove)
                    {
                        Destroy(col.gameObject);
                    }
                }
            }
        } else {
            Debug.Log("PREFAB EXPLOSION LOST");
        }

        
    }
}
