using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    public BoxCollider2D boundBox;

    private float _halfHeight;
    private float _halfWidth;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerController>();
    }

    void Start() {

        _halfHeight = Camera.main.orthographicSize;
        _halfWidth  = _halfHeight * Camera.main.aspect;
    }

    void Update()
    {
        if(_player != null)
        {
            transform.position = new Vector3(
                    Mathf.Clamp(_player.transform.position.x, boundBox.bounds.min.x + _halfWidth,  boundBox.bounds.max.x - _halfWidth),
                    Mathf.Clamp(_player.transform.position.y, boundBox.bounds.min.y + _halfHeight, boundBox.bounds.max.y - _halfHeight),
                    transform.position.z);
        }
        
    }
}
