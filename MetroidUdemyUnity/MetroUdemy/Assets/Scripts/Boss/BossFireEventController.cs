using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireEventController : MonoBehaviour
{
    public GameObject bossBullet;

    public void BossFiring()
    {
        Instantiate(bossBullet, transform.position, Quaternion.identity);

        //SFX
        AudioManagerController.instance.PlaySFX(11);
    }
}
