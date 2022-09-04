using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnController : MonoBehaviour
{
    public static RespawnController instance;

    public Vector3 respawnPoint;
    private GameObject _player;

    [SerializeField] private float _timeToRespawn;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //PlayerHealthController.instance.RefillHP();
            PlayerHealthController.instance.playerIsDead = false;

            Destroy(gameObject);
        }

    }

    private void Start()
    {
        _player = PlayerHealthController.instance.gameObject;
        respawnPoint = _player.transform.position;
    }

    public void Respawn()
    {
        StartCoroutine("RespawnRoutine");
    }

    public void SetNewRespawm(Vector3 checkpointPosition)
    {
        respawnPoint = checkpointPosition;
    }


    IEnumerator RespawnRoutine()
    {
        _player.SetActive(false);
        yield return new WaitForSeconds(_timeToRespawn);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        _player.transform.position = respawnPoint;
        _player.SetActive(true);
        UIController.instance.FadeOut();
    }
}
