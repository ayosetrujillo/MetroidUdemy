using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleGhostPro : MonoBehaviour
{
    public GameObject mainCam;
    public GameObject bossCam;
    public float camSpeed;

    public Transform mainCamPos;
    public Transform bossCamPos;


    void Start()
    {
        mainCam.SetActive(false);
        bossCam.SetActive(true);
    }

    void Update()
    {
       // bossCam.transform.position = Vector3.MoveTowards(mainCamPos.position, bossCamPos.position, camSpeed * Time.deltaTime);
    }
}
