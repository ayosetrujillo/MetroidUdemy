using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerController : MonoBehaviour
{
    public static AudioManagerController instance;
    public AudioSource titleMusic, mainMusic, bossMusic;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void PlayTitleTheme()
    {
        titleMusic.Play();
        mainMusic.Stop();
        bossMusic.Stop();

    }

    public void PlayMainTheme()
    {
        titleMusic.Stop();
        mainMusic.Play();
        bossMusic.Stop();

    }

    public void PlayBossTheme()
    {
        titleMusic.Stop();
        mainMusic.Stop();
        bossMusic.Play();

    }

}
